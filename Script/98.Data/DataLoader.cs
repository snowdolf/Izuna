using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.EventSystems;

[System.Serializable]
public class DialogueData
{
    public int DialogID;
    public string NPCID;
    public string Content;
    public int NextNPCID;
}



[Serializable]
public class PlayerData
{
    public int idx;
    public string name;
    public int health;
    public int damage;
    public int speed;
    public int attackDelay;
}
[Serializable]
public class EnemyData
{
    public int idx;
    public string rcode;
    public string name;
    public int health;
    public int damage;
    public float speed;
    public float attackDelay;
    public float attackRange;
    public float detectionRange;
}


[Serializable]
public class PlayerDataLoader : ILoader<int, PlayerData>
{
    public List<PlayerData> playerStats = new List<PlayerData>();

    public Dictionary<int, PlayerData> MakeDict()
    {
        Dictionary<int, PlayerData> dic = new Dictionary<int, PlayerData>();

        foreach (PlayerData playerStat in playerStats)
            dic.Add(playerStat.idx, playerStat);

        return dic;
    }
}

[Serializable]
public class EnemyDataLoader : ILoader<string, EnemyData>
{
    public List<EnemyData> enemyStats = new List<EnemyData>();

    public Dictionary<string, EnemyData> MakeDict()
    {
        Dictionary<string, EnemyData> dic = new Dictionary<string, EnemyData>();

        foreach (EnemyData enemyStat in enemyStats)
            dic.Add(enemyStat.rcode, enemyStat);

        return dic;
    }
}

[System.Serializable]
public class DialogueDataLoader : ILoader<int, DialogueData>
{
    public List<DialogueData> dialogues = new List<DialogueData>();

    public Dictionary<int, DialogueData> MakeDict()
    {
        Dictionary<int, DialogueData> dic = new Dictionary<int, DialogueData>();

        foreach (DialogueData dialog in dialogues)
            dic.Add(dialog.DialogID, dialog);

        return dic;
    }

}
