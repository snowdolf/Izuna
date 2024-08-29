using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    protected virtual void AddInputActionsCallbacks()
    {
        PlayerController controller = stateMachine.player.controller;
        controller.playerActions.Move.canceled += OnMovementCanceled;

        controller.playerActions.Jump.started += OnJumpStarted;

        controller.playerActions.Attack.started += OnAttackStarted;
        controller.playerActions.Attack.canceled += OnAttackCanceled;

        controller.playerActions.Chain.started += OnChainStarted;
        controller.playerActions.Chain.canceled += OnChainCanceled;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerController controller = stateMachine.player.controller;
        controller.playerActions.Move.canceled -= OnMovementCanceled;

        controller.playerActions.Jump.started -= OnJumpStarted;

        controller.playerActions.Attack.started -= OnAttackStarted;
        controller.playerActions.Attack.canceled -= OnAttackCanceled;

        controller.playerActions.Chain.started -= OnChainStarted;
        controller.playerActions.Chain.canceled -= OnChainCanceled;
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {
        Move();
    }

    public virtual void Update()
    {
        
    }

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnAttackStarted(InputAction.CallbackContext context)
    {
        stateMachine.isPressAttack = true;
        stateMachine.ChangeState(stateMachine.comboAttackState);
    }

    protected virtual void OnAttackCanceled(InputAction.CallbackContext context)
    {
        stateMachine.isPressAttack = false;
    }

    protected virtual void OnChainStarted(InputAction.CallbackContext context)
    {
        if (!stateMachine.isAttacking)
        {
            stateMachine.ChangeState(stateMachine.chainState);
        }
    }

    protected virtual void OnChainCanceled(InputAction.CallbackContext context)
    {
        if (IsGround())
        {
            stateMachine.ChangeState(stateMachine.idleState);
        }
        else
        {
            stateMachine.ChangeState(stateMachine.fallState);
        }
    }

    protected void StartAnimation(int animatorHash)
    {
        stateMachine.player.animator.SetBool(animatorHash, true);
    }

    protected void StopAnimation(int animatorHash)
    {
        stateMachine.player.animator.SetBool(animatorHash, false);
    }

    private void ReadMovementInput()
    {
        stateMachine.movementInput = stateMachine.player.controller.playerActions.Move.ReadValue<Vector2>();
        if (!stateMachine.isAttacking) TogglePlayer();
    }

    private void TogglePlayer()
    {
        if (stateMachine.player.playerComponent.isDead) return;

        if (stateMachine.movementInput.x < 0f && !stateMachine.player.spriteRenderer.flipX)
        {
            stateMachine.player.spriteRenderer.flipX = true;
            for (int i = 0; i < stateMachine.player.comboColliders.Count; i++)
            {
                stateMachine.player.comboColliders[i].transform.localPosition = stateMachine.player.weaponHitBoxLeftPos[i];
            }
            ToggleAttackEffect();
            if (IsGround()) stateMachine.player.flipDust.Play();
        }
        else if (stateMachine.movementInput.x > 0f && stateMachine.player.spriteRenderer.flipX)
        {
            stateMachine.player.spriteRenderer.flipX = false;
            for (int i = 0; i < stateMachine.player.comboColliders.Count; i++)
            {
                stateMachine.player.comboColliders[i].transform.localPosition = stateMachine.player.weaponHitBoxRightPos[i];
            }
            ToggleAttackEffect();
            if (IsGround()) stateMachine.player.flipDust.Play();
        }
    }

    protected void ToggleAttackEffect()
    {
        Vector2 attackEffectScale = stateMachine.player.attackEffects.transform.localScale;
        attackEffectScale.x *= -1;
        stateMachine.player.attackEffects.transform.localScale = attackEffectScale;
    }

    private void Move()
    {
        if (!stateMachine.isAttacking) ChangeVelocityX(GetMovementDirection().x * GetMovementSpeed());
    }

    private Vector2 GetMovementDirection()
    {
        return Vector2.right * stateMachine.movementInput.x;
    }

    private float GetMovementSpeed()
    {
        float moveSpeed = stateMachine.movementSpeed * stateMachine.movementSpeedModifier;
        return moveSpeed;
    }

    protected void ChangeVelocityX(float velocity)
    {
        // before velocity
        Vector2 currentVelocity = stateMachine.player.rigidBody.velocity;

        // after velocity
        currentVelocity.x = velocity;

        // update velocity
        stateMachine.player.rigidBody.velocity = currentVelocity;
    }

    protected bool IsGround()
    {
        List<Vector2> positions = new List<Vector2>();

        Vector2 centerPosition = stateMachine.player.boxCollider.bounds.center;
        float extentX = stateMachine.player.boxCollider.bounds.extents.x;

        positions.Add(centerPosition);
        positions.Add(centerPosition - new Vector2(extentX / 2, 0));
        positions.Add(centerPosition + new Vector2(extentX / 2, 0));

        float rayDistance = stateMachine.player.boxCollider.bounds.extents.y + 0.2f;
        LayerMask groundLayer = LayerMask.GetMask("Ground");

        foreach (Vector2 position in positions)
        {
            if (Physics2D.Raycast(position, Vector2.down, rayDistance, groundLayer))
            {
                return true;
            }
        }

        return false;
    }

    protected void ForceMove(Vector2 direction, float force)
    {
        stateMachine.player.rigidBody.AddForce(direction.normalized * force, ForceMode2D.Impulse);
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
}
