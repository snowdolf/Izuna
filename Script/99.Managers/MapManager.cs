using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] mapPrefabs; // 맵 프리팹 리스트
    public Transform player; // 플레이어 트랜스폼
    public ScreenFader screenFader; // ScreenFader 참조
    public float fadeDuration = 1f; // 페이드 인/아웃 시간

    private int currentMapIndex = 0;
    private GameObject currentMap;

// TODO: 
/*     void Start()
    {
        LoadMap(currentMapIndex);
    }

    void LoadMap(int mapIndex)
    {
        if (currentMap != null)
        {
            Destroy(currentMap);
        }

        currentMap = Instantiate(mapPrefabs[mapIndex]);
    } */

    public void TransitionToMap(int mapIndex, Vector3 targetPosition)
    {
        StartCoroutine(Transition(mapIndex, targetPosition));
    }

    IEnumerator Transition(int mapIndex, Vector3 targetPosition)
    {
        yield return StartCoroutine(screenFader.FadeOut(fadeDuration));
// TODO: 
        // LoadMap(mapIndex);
        player.position = targetPosition;

        yield return StartCoroutine(screenFader.FadeIn(fadeDuration));
    }
}
