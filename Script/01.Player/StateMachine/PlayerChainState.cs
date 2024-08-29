using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerChainState : PlayerBaseState
{
    private float maxChainDistance;
    private LayerMask targetLayer;
    private Vector2 mousePosition;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private Vector2 endExitPostion;

    private ParticleSystem chainParticleSystem;

    public PlayerChainState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        maxChainDistance = 4f;
        targetLayer = LayerMask.GetMask("Ground");
        chainParticleSystem = stateMachine.player.chainParticle.GetComponent<ParticleSystem>();
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.player.chainLine.enabled = true;
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.player.chainLine.enabled = false;
        stateMachine.player.transform.position = endExitPostion;

        stateMachine.player.chainParticle.transform.position = endPosition;
        Vector2 direction = (startPosition - endPosition).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        stateMachine.player.chainParticle.transform.rotation = Quaternion.Euler(0, 0, angle);
        chainParticleSystem.Play();
        SoundManager.instance.PlaySound(stateMachine.player.clipChain, 0.7f, 0f);
    }

    public override void Update()
    {
        base.Update();
        mousePosition = Mouse.current.position.ReadValue();
        ShootLaser();
    }
    void ShootLaser()
    {
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        startPosition = stateMachine.player.transform.position;
        Vector2 direction = (mouseWorldPosition - startPosition).normalized;
        RaycastHit2D hit = Physics2D.Raycast(startPosition, direction, maxChainDistance, targetLayer);

        if (hit.collider != null)
        {
            endPosition = hit.point;
            endExitPostion = endPosition - direction * stateMachine.player.boxCollider.bounds.extents.y;
        }
        else
        {
            endPosition = startPosition + direction * maxChainDistance;
            endExitPostion = endPosition;
        }

        DrawLaser(new Vector2(0f, 0f), endPosition - startPosition);
    }

    void DrawLaser(Vector2 start, Vector2 end)
    {
        Vector2  scaledEnd = new Vector2(end.x / stateMachine.player.transform.lossyScale.x, end.y / stateMachine.player.transform.lossyScale.y);
        stateMachine.player.chainLine.SetPosition(0, start);
        stateMachine.player.chainLine.SetPosition(1, scaledEnd);
    }
}
