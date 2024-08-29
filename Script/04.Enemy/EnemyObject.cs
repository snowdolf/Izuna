using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    protected Camera mainCamera;
    protected GameObject playerPos;
    protected bool isRotate; // 체크하면 오브젝트가 빙글빙글 돌아갑니다

    public int damage;
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player");
        mainCamera = Camera.main;
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
    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Destroy(gameObject);
    //}
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy (gameObject);
    }
}
