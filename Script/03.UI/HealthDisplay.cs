using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.Mathematics;

public class HealthDisplay : MonoBehaviour
{
    public int maxHealth; // 최대 체력
    public int currentHealth; // 현재 체력
    public GameObject heartPrefab; // 하트 프리팹
    public Transform heartContainer; // 하트를 배치할 컨테이너
    private float heartSpacing = 40f; // 하트 간격

    public GameObject dieDisplay;

    private List<GameObject> hearts = new List<GameObject>();

    void Start()
    {
        maxHealth = Managers.Data.playerStats[0].health;
        currentHealth = maxHealth;
        UpdateHearts();
    }

    // 하트를 업데이트하는 메서드
    public void UpdateHearts()
    {
        // 기존 하트 제거
        foreach (Transform child in heartContainer)
        {
            Destroy(child.gameObject);
        }
        hearts.Clear();

        // 새로운 하트 생성
        for (int i = 0; i < maxHealth; i++)
        {
            Vector2 heartPos = new Vector2(heartContainer.transform.position.x + i * heartSpacing, heartContainer.transform.position.y);
            GameObject heart = Instantiate(heartPrefab, heartPos, Quaternion.identity, heartContainer);
            hearts.Add(heart);
        }

        // 현재 체력에 따라 하트 이미지 업데이트
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                hearts[i].GetComponent<Image>().enabled = false;
            }
        }
    }

    // 체력을 설정하는 메서드
    public void SetHealth(int health)
    {
        currentHealth = health;
        UpdateHearts();
        if (health <= 0)
        {
            Invoke("Die", 3f);
        }
    }

    public void Die()
    {
        dieDisplay.SetActive(true);
    }
}
