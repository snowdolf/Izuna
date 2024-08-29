using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    Dictionary<int, string[]> talkData;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }


    void GenerateData()
    {
        for (int i = 1; i < Managers.Data.dialogues.Count; i++)
        {
            talkData.Add(Managers.Data.dialogues[i].DialogID, new string[] { Managers.Data.dialogues[i].Content });
        }
    }
    public string GetTalk(int npcID, int talkIndex)
    {
        if (talkIndex == talkData[npcID].Length)
            return null;
        else
            return talkData[npcID][talkIndex];
    }
}
