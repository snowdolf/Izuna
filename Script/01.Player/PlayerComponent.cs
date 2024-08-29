using System.Collections;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    public int health;
    public int damage;
    public int speed;
    public int attackDelay;

    public bool isDead => health == 0;

    public Animator anim;
    public HealthDisplay healthDisplay;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Player player;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponent<Player>();
    }

    public void Initialize(PlayerData data)
    {
        health = data.health;
        damage = data.damage;
        speed = data.speed;
        attackDelay = data.attackDelay;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("EnemyObject"))
        {
            EnemyObject objectDamage = collider.gameObject.GetComponent<EnemyObject>();
            OnHit(objectDamage.damage);
            OnDamaged(collider.transform.position);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyComponent enemy = collision.gameObject.GetComponent<EnemyComponent>();
            OnHit(enemy.damage);
            OnDamaged(collision.transform.position);
        }
        if (collision.gameObject.tag == "EnemyObject")
        {
            EnemyObject objectDamage = collision.gameObject.GetComponent<EnemyObject>();
            OnHit(objectDamage.damage);
            OnDamaged(collision.transform.position);
        }

    }

    void OnHit(int damage)
    {
        if (isDead) return;

        health = Mathf.Max(health - damage, 0);

        if (healthDisplay != null)
        {
            healthDisplay.SetHealth(health);
        }

        if (isDead)
        {
            player.stateMachine.ChangeState(player.stateMachine.deathState);
        }
    }

    void OnDamaged(Vector2 targetPos)
    {
        if (isDead) return;

        gameObject.layer = 12;
        StartCoroutine(FlashCoroutine());

        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigidBody.AddForce(new Vector2(dirc, 1) * 2, ForceMode2D.Impulse);
        //anim.SetTrigger("OnHit");
        Invoke("OffDamaged", 1.5f);
    }

    IEnumerator FlashCoroutine()
    {
        float flashDuration = 0.3f;
        float flashInterval = 0.15f;

        float timer = 0f;
        bool isFlashing = true;

        while (timer < flashDuration)
        {
            spriteRenderer.color = isFlashing ? new Color(1, 0, 0, 1f) : new Color(1, 1, 1, 1f);
            isFlashing = !isFlashing;

            yield return new WaitForSeconds(flashInterval);

            timer += flashInterval;
        }

        spriteRenderer.color = new Color(1, 1, 1, 0.5f);
    }

    void OffDamaged()
    {
        gameObject.layer = 11;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void PlayAttackSound()
    {
        SoundManager.instance.PlaySound(player.clipAttack, 0.15f, 0.05f);
    }
}
