using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private string targetTag = "Enemy";

    [SerializeField] private Player player;
    [SerializeField] private int comboIndex;

    [SerializeField] GameObject attackEffect;
    [SerializeField] private float effectDuration;

    private int damage;

    private Vector2 startPosition;
    private Vector2 endPosition;
    private Vector2 direction;
    private float angle;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void Start()
    {
        damage = player.data.attackData.GetAttackInfoData(comboIndex).damage;
        attackEffect.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            if (collision.TryGetComponent<EnemyComponent>(out EnemyComponent enemy))
            {
                enemy.OnHit(damage);
            }

            startPosition = player.transform.position;
            endPosition = collision.transform.position;
            direction = (endPosition - startPosition).normalized;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            attackEffect.transform.rotation = Quaternion.Euler(0, 0, angle);

            StartCoroutine(AttackEffectFlash());
        }
    }

    IEnumerator AttackEffectFlash()
    {
        attackEffect.SetActive(true);
        yield return new WaitForSeconds(effectDuration);
        attackEffect.SetActive(false);
    }
}
