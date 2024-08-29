using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerSO data {  get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData animationData { get; private set; }

    public Animator animator { get; private set; }
    public PlayerController controller { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    public Rigidbody2D rigidBody { get; private set; }
    public BoxCollider2D boxCollider { get; private set; }
    public PlayerComponent playerComponent { get; private set; }

    [field: Header("ComboAttack Colliders")]
    [field: SerializeField] public List<BoxCollider2D> comboColliders { get; private set; }
    [field: SerializeField] public List<Vector2> weaponHitBoxRightPos { get; private set; }
    [field: SerializeField] public List<Vector2> weaponHitBoxLeftPos { get; private set; }
    [field: SerializeField] public List<PlayerWeapon> playerWeapons { get; private set; }

    [field: Header("ComboAttack Effects")]
    [field: SerializeField] public GameObject attackEffects { get; private set; }

    [field: Header("ChainEffect")]
    [field: SerializeField] public LineRenderer chainLine { get; private set; }
    [field: SerializeField] public GameObject chainParticle { get; private set; }

    [field: Header("MoveEffect")]
    [field: SerializeField] public ParticleSystem runDust { get; private set; }
    [field: SerializeField] public ParticleSystem flipDust { get; private set; }
    [field: SerializeField] public ParticleSystem jumpDust { get; private set; }
    [field: SerializeField] public ParticleSystem fallDust { get; private set; }

    [field: Header("SoundEffect")]
    [field: SerializeField] public AudioClip clipAttack { get; private set; }
    [field: SerializeField] public AudioClip clipJump { get; private set; }
    [field: SerializeField] public AudioClip clipLand { get; private set; }
    [field: SerializeField] public AudioClip clipChain { get; private set; }

    [field: SerializeField] public PlayerStateMachine stateMachine { get; private set; }

    private void Awake()
    {
        animationData.Initialize();
        animator = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        playerComponent = GetComponent<PlayerComponent>();

        stateMachine = new PlayerStateMachine(this);
        stateMachine.ChangeState(stateMachine.idleState);
    }

    void Start()
    {
        foreach (var collider in comboColliders)
        {
            Vector2 colliderPos = collider.transform.localPosition;
            weaponHitBoxRightPos.Add(colliderPos);
            weaponHitBoxLeftPos.Add(new Vector2(-colliderPos.x, colliderPos.y));
        }
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
}
