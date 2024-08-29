using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyComponent
{
    public float curDelay;
    enum State
    {
        Idle,
        Run,
        Attack,
        OnHit
    }
    State state;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        state = State.Idle;
        playerPos = GameObject.FindWithTag("Player");

    }


    void Update()
    {
        if (state == State.Idle)
        {
            UpdateIdle();
        }
        else if (state == State.Run)
        {
            UpdateRun();
        }
        else if (state == State.Attack)
        {
            UpdateAttack();
            Reload();
        }
        //else if (state == State.Hit)
        //{
        //    UpdateHit();
        //}
    }
    void UpdateIdle()
    {
        rigid.velocity = Vector2.zero;
        if (playerPos != null && Vector2.Distance(transform.position, playerPos.transform.position) <= detectionRange)
        {
            state = State.Run;
            anim.SetTrigger("Run");
        }
    }


    void UpdateRun()
    {
        float distance = Vector2.Distance(transform.position, playerPos.transform.position);
        if (distance <= attackRange) // 플레이어가 가까워지면 공격
        {
            state = State.Attack;
            anim.SetBool("isAttacking", true);
            anim.ResetTrigger("Run");
            rigid.velocity = Vector2.zero;
            return;
        }
        Vector2 direction = (playerPos.transform.position - transform.position).normalized;
        rigid.velocity = direction * speed;
        if (direction.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(direction.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    void UpdateAttack()
    {
        if (curDelay < attackDelay)
            return;


            state = State.Run;
            anim.SetBool("isAttacking", false);
            anim.SetTrigger("Run");
            curDelay = 0;
        

    }
    void Reload()
    {
        curDelay += Time.deltaTime;
    }




}