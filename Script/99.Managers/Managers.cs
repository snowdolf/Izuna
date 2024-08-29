using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Managers : MonoBehaviour
{
    // singleton
    static Managers s_instance = null;
    static Managers Instance { get { Init(); return s_instance; } }

    DataManager _data = new DataManager();
    GameManager _game = new GameManager();



    public static DataManager Data { get { return Instance?._data; } }


    public static GameManager Game { get { return Instance?._game; } }



    void Start()
    {
        Init();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();


            s_instance._data.Init();
            s_instance._game.Init();

            // Application.targetFrameRate = 60;

            
        }
    }

    public static void Clear()
    {



    }
}