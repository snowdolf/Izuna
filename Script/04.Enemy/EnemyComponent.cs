using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComponent : MonoBehaviour
{
    public Animator anim;
    protected GameObject playerPos;
    protected Rigidbody2D rigid;
    protected SpriteRenderer spriteRenderer;
    public ParticleSystem dieParticle;


    [Header("Enemy Stats")]
    public int maxHP;
    public int health;
    public int damage;
    public float speed;
    public float attackDelay;
    public float attackRange;
    public float detectionRange;

    public void Initialize(EnemyData data)
    {
        health = data.health;
        maxHP = health;
        damage = data.damage;
        speed = data.speed;
        attackDelay = data.attackDelay;
        attackRange = data.attackRange;
        detectionRange = data.detectionRange;
    }
    public void OnHit(int damage)
    {
        health -= damage;
        anim.SetTrigger("OnHit");
        if (health <= 0)
        {
            Die();
        }
    }
    protected virtual void Die()  // �� ����� ��ƼŬ ȿ�� �߰��� ���� �ۼ�
    {
        if (dieParticle != null)
        {
            ParticleSystem particle = Instantiate(dieParticle, transform.position, Quaternion.identity);
            particle.Play();
            Destroy(particle.gameObject, particle.main.duration + particle.main.startLifetime.constantMax);
        }
        Destroy(gameObject);
    }
}
