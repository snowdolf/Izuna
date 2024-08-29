using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public int targetMapIndex; // 이동할 맵의 인덱스
    public Vector3 targetPosition; // 이동할 위치 (월드 좌표)
    private bool playerInRange;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            MapManager mapManager = FindObjectOfType<MapManager>();
            if (mapManager != null)
            {
                mapManager.TransitionToMap(targetMapIndex, targetPosition);
            }
        }
    }
}
