using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanObj : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.scene.name == null || obj.scene.name == "DontDestroyOnLoad")
            {
                // 필요에 따라 로그 추가
                Debug.Log($"Destroying object: {obj.name}");
                if (obj.name != "EffectManager")
                {
                    Destroy(obj);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
