using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public class EnemyManager : MonoBehaviour
{
    private DataManager dataManager;

    void Start()
    {
        dataManager = new DataManager();
        dataManager.Init();

        ApplyPlayerData();
        ApplyEnemyData();
    }

    void ApplyPlayerData()
    {
        foreach (var playerData in dataManager.playerStats.Values)
        {
            GameObject playerObject = GameObject.Find(playerData.name);
            if (playerObject != null)
            {
                PlayerComponent playerComponent = playerObject.GetComponent<PlayerComponent>();
                if (playerComponent != null)
                {
                    playerComponent.Initialize(playerData);
                }
            }
        }
    }

    void ApplyEnemyData()
    {
        foreach (var enemyData in dataManager.enemyStats.Values)
        {
            GameObject enemyObject = GameObject.Find(enemyData.rcode);
            if (enemyObject != null)
            {
                EnemyComponent enemyComponent = enemyObject.GetComponent<EnemyComponent>();
                if (enemyComponent != null)
                {
                    enemyComponent.Initialize(enemyData);
                }
            }
        }
    }
}