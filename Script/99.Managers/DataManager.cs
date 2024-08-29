using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}
public class DataManager
{
    public Dictionary<int, PlayerData> playerStats { get; private set; } = new Dictionary<int, PlayerData>();
    public Dictionary<int, DialogueData> dialogues { get; private set; } = new Dictionary<int, DialogueData>();

    public Dictionary<string, EnemyData> enemyStats { get; private set; } = new Dictionary<string, EnemyData>();

    public void Init()
    {
        playerStats = LoadJson<PlayerDataLoader, int, PlayerData>("PlayerData").MakeDict();
        dialogues = LoadJson<DialogueDataLoader, int, DialogueData>("Dialogues").MakeDict();
        enemyStats = LoadJson<EnemyDataLoader, string, EnemyData>("EnemyData").MakeDict();

        Debug.Log("Enemy Data Loaded:");
        foreach (var kvp in enemyStats)
        {
            Debug.Log($"rcode: {kvp.Key}, Name: {kvp.Value.name}");
        }
    }

    public bool Loaded()
    {
        return true;
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"02.Data/01.Json/{path}");
        Debug.Log($"Loaded JSON from path: Data/Json/{path}");
        Debug.Log($"JSON Content: {textAsset.text}");
        return JsonConvert.DeserializeObject<Loader>(textAsset.text);
    }


}