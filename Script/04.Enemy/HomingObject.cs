using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingObject : EnemyObject
{
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player");
        mainCamera = Camera.main;
        Rotate();
    }

    void Update()
    {
        if (isRotate)
            transform.Rotate(Vector3.forward * 10);
        Vector3 viewPos = mainCamera.WorldToViewportPoint(transform.position);
        // 시야 영역을 벗어나면 제거
        if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
        {
            Destroy(gameObject);
        }
    }
    void Rotate()
    {
        Vector2 direction = (playerPos.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }
}