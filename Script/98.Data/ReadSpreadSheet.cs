using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using UnityEditor;


public class ReadSpreadSheet : MonoBehaviour
{
    public readonly string ADDRESS = "https://docs.google.com/spreadsheets/d/1W4PTXRTMdVBRirbbNGHYouh16OlhMjGLfBc5kKoYSdg";
    public readonly string[] RANGE = { "A2:D", "A2:I", "A2:F" };
    public readonly long[] SHEET_ID = { 1528743577, 1126790782, 1195838914 };
    private string[] SHEET_NAME = { "Dialogues", "EnemyData", "PlayerData" };

    //[MenuItem("Json/ParseGoogleSheetLoad")]
    public static void ParseGoogleSheetLoad()
    {
        // 새로운 숨겨진 게임 오브젝트를 생성하고 ReadSpreadSheet 컴포넌트를 추가합니다.
        GameObject go = new GameObject("ReadSpreadSheetLoader");
        go.hideFlags = HideFlags.HideAndDontSave; // 에디터 씬에서 숨기고 저장하지 않음
        ReadSpreadSheet instance = go.AddComponent<ReadSpreadSheet>();
        instance.StartCoroutine(instance.LoadDataCoroutine());
    }

    private IEnumerator LoadDataCoroutine()
    {
        yield return LoadDataDialogues(dialogueDataList => SaveToDialoguesJson(dialogueDataList, SHEET_NAME[0]));

        yield return LoadDataEnemyStats(enemyDataList => SaveToEnemyJson(enemyDataList, SHEET_NAME[1]));

        yield return LoadDataPlayerStats(playerDataList => SaveToPlayerJson(playerDataList, SHEET_NAME[2]));
        // 작업이 끝나면 게임 오브젝트를 파괴합니다.
        DestroyImmediate(gameObject);
    }

    private IEnumerator LoadDataPlayerStats(Action<List<PlayerData>> onLoaded)
    {
        UnityWebRequest www = UnityWebRequest.Get(GetTSVAddress(ADDRESS, RANGE[2], SHEET_ID[2]));
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
            yield break;
        }

        string tsvData = www.downloadHandler.text;
        List<PlayerData> playerDataList = ParseTSVPlayerData(tsvData);
        Debug.Log($"Parsed {playerDataList.Count} rows of data");

        SaveToPlayerJson(playerDataList, SHEET_NAME[2]);
    }

    private IEnumerator LoadDataEnemyStats(Action<List<EnemyData>> onLoaded)
    {
        UnityWebRequest www = UnityWebRequest.Get(GetTSVAddress(ADDRESS, RANGE[1], SHEET_ID[1]));
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
            yield break;
        }

        string tsvData = www.downloadHandler.text;
        List<EnemyData> enemyDataList = ParseTSVEnemyData(tsvData);
        Debug.Log($"Parsed {enemyDataList.Count} rows of data");

        SaveToEnemyJson(enemyDataList, SHEET_NAME[1]);
    }

    private IEnumerator LoadDataDialogues(Action<List<DialogueData>> onLoaded)
    {
        UnityWebRequest www = UnityWebRequest.Get(GetTSVAddress(ADDRESS, RANGE[0], SHEET_ID[0]));
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
            yield break;
        }

        string tsvData = www.downloadHandler.text;
        List<DialogueData> dialogueDataList = ParseTSVDialogData(tsvData);
        Debug.Log($"Parsed {dialogueDataList.Count} rows of data");

        SaveToDialoguesJson(dialogueDataList, SHEET_NAME[0]);
    }


    public static string GetTSVAddress(string address, string range, long sheetID)
    {
        return $"{address}/export?format=tsv&range={range}&gid={sheetID}";
    }

    private List<DialogueData> ParseTSVDialogData(string tsvData)
    {
        List<DialogueData> dialogueDataList = new List<DialogueData>();
        string[] lines = tsvData.Split('\n');
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] fields = line.Split('\t');
            if (fields.Length < 4)
                continue;

            DialogueData dialogueData = new DialogueData
            {
                DialogID = int.Parse(fields[0]),
                NPCID = fields[1],
                Content = fields[2],
                NextNPCID = int.Parse(fields[3])
            };

            dialogueDataList.Add(dialogueData);
        }

        return dialogueDataList;
    }
    private List<EnemyData> ParseTSVEnemyData(string tsvData)
    {
        List<EnemyData> enemyDataList = new List<EnemyData>();
        string[] lines = tsvData.Split('\n');
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] fields = line.Split('\t');
            if (fields.Length < 9)
                continue;

            EnemyData enemyData = new EnemyData
            {
                idx = int.Parse(fields[0]),
                rcode = fields[1],
                name = fields[2],
                health = int.Parse(fields[3]),
                damage = int.Parse(fields[4]),
                speed = int.Parse(fields[5]),
                attackDelay = int.Parse(fields[6]),
                attackRange = int.Parse(fields[7]),
                detectionRange = int.Parse(fields[8])
            };

            enemyDataList.Add(enemyData);
        }

        return enemyDataList;
    }
    private List<PlayerData> ParseTSVPlayerData(string tsvData)
    {
        List<PlayerData> playerDataList = new List<PlayerData>();
        string[] lines = tsvData.Split('\n');
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] fields = line.Split('\t');
            if (fields.Length < 6)
                continue;

            PlayerData playerdata = new PlayerData
            {
                idx = int.Parse(fields[0]),
                name = fields[1],
                health = int.Parse(fields[2]),
                damage = int.Parse(fields[3]),
                speed = int.Parse(fields[4]),
                attackDelay = int.Parse(fields[5])
            };

            playerDataList.Add(playerdata);
        }

        return playerDataList;
    }

    private void SaveToDialoguesJson(List<DialogueData> dialogueDataList, string sheetName)
    {
        // DialogueData 리스트를 DialogueDataLoader 객체로 감쌉니다.
        DialogueDataLoader dataLoader = new DialogueDataLoader();
        dataLoader.dialogues = dialogueDataList;

        // dataLoader 객체를 JSON으로 직렬화합니다.
        string jsonString = JsonUtility.ToJson(dataLoader, true);
        string jsonFilePath = Path.Combine(Application.dataPath, "Resources/02.Data/01.Json", $"{sheetName}.json");

        // 경로가 존재하지 않으면 생성합니다.
        string directoryPath = Path.GetDirectoryName(jsonFilePath);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        Debug.Log(jsonFilePath);

        File.WriteAllText(jsonFilePath, jsonString);
        Debug.Log($"데이터가 {jsonFilePath} 파일에 저장되었습니다.");
    }
    private void SaveToEnemyJson(List<EnemyData> enemyDataList, string sheetName)
    {
        // DialogueData 리스트를 DialogueDataLoader 객체로 감쌉니다.
        EnemyDataLoader dataLoader = new EnemyDataLoader();
        dataLoader.enemyStats = enemyDataList;

        // dataLoader 객체를 JSON으로 직렬화합니다.
        string jsonString = JsonUtility.ToJson(dataLoader, true);
        string jsonFilePath = Path.Combine(Application.dataPath, "Resources/02.Data/01.Json", $"{sheetName}.json");

        // 경로가 존재하지 않으면 생성합니다.
        string directoryPath = Path.GetDirectoryName(jsonFilePath);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        Debug.Log(jsonFilePath);

        File.WriteAllText(jsonFilePath, jsonString);
        Debug.Log($"데이터가 {jsonFilePath} 파일에 저장되었습니다.");
    }
    private void SaveToPlayerJson(List<PlayerData> playerDataList, string sheetName)
    {
        // DialogueData 리스트를 DialogueDataLoader 객체로 감쌉니다.
        PlayerDataLoader dataLoader = new PlayerDataLoader();
        dataLoader.playerStats = playerDataList;

        // dataLoader 객체를 JSON으로 직렬화합니다.
        string jsonString = JsonUtility.ToJson(dataLoader, true);
        string jsonFilePath = Path.Combine(Application.dataPath, "Resources/02.Data/01.Json", $"{sheetName}.json");

        // 경로가 존재하지 않으면 생성합니다.
        string directoryPath = Path.GetDirectoryName(jsonFilePath);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        Debug.Log(jsonFilePath);

        File.WriteAllText(jsonFilePath, jsonString);
        Debug.Log($"데이터가 {jsonFilePath} 파일에 저장되었습니다.");
    }
}



