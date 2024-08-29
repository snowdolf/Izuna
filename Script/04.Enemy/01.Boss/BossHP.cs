using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHP : MonoBehaviour
{
    public EnemyComponent enemy;
    private Slider sliderHP;


    private void Awake()
    {
        sliderHP = GetComponent<Slider>();
    }
    private void Update()
    {
        sliderHP.value = (float)enemy.health / (float)enemy.maxHP;
    }
    //private void Update()
    //{
    //    float healthFloat = (float)enemy.health / (float)enemy.maxHP;
    //    sliderHP.value = Mathf.Clamp01(healthFloat);
    //}
}