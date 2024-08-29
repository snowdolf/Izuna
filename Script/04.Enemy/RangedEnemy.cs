using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyComponent
{
    public GameObject EnemyAttackA;
    public float curDelay;
    public string rangedType;

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
        curDelay = 2;

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

        if (rangedType == "A")
        {
            GameObject attack = Instantiate(EnemyAttackA, transform.position, transform.rotation);
            Rigidbody2D rigid = attack.GetComponent<Rigidbody2D>();
            Vector2 dirVec = playerPos.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * 15, ForceMode2D.Impulse);
        }
        else if (rangedType == "B")
        {
            GameObject attackR = Instantiate(EnemyAttackA);
            attackR.transform.position = transform.position + Vector3.right * 0.2f;
            GameObject attackL = Instantiate(EnemyAttackA);
            attackL.transform.position = transform.position + Vector3.left * 0.2f;

            Rigidbody2D rigidR = attackR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = attackL.GetComponent<Rigidbody2D>();

            Vector2 dirVec = playerPos.transform.position - transform.position;

            rigidR.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
            rigidL.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
        }
        else if (rangedType == "C")
        {
            GameObject attack = Instantiate(EnemyAttackA, transform.position, transform.rotation);
            Rigidbody2D rigid = attack.GetComponent<Rigidbody2D>();
            Vector2 dirVec = playerPos.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);
        }

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