using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackState : PlayerBaseState
{
    private Vector2 mousePosition;
    private Vector2 mouseWorldPosition;
    private Vector2 startPosition;
    private Vector2 mouseDirection;

    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        ChangeVelocityX(0f);
        stateMachine.isAttacking = true;

        if (!stateMachine.isComboAttacking) TogglePlayerWithAttack();

        base.Enter();
        StartAnimation(stateMachine.player.animationData.attackParameterHash);
    }

    public override void Exit()
    {
        stateMachine.isAttacking = false;
        base.Exit();
        StopAnimation(stateMachine.player.animationData.attackParameterHash);
    }

    private void TogglePlayerWithAttack()
    {
        mousePosition = Mouse.current.position.ReadValue();
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        startPosition = stateMachine.player.transform.position;
        mouseDirection = (mouseWorldPosition - startPosition).normalized;

        if (mouseDirection.x < 0f && !stateMachine.player.spriteRenderer.flipX)
        {
            stateMachine.player.spriteRenderer.flipX = true;
            for (int i = 0; i < stateMachine.player.comboColliders.Count; i++)
            {
                stateMachine.player.comboColliders[i].transform.localPosition = stateMachine.player.weaponHitBoxLeftPos[i];
            }
            ToggleAttackEffect();
        }
        else if (mouseDirection.x > 0f && stateMachine.player.spriteRenderer.flipX)
        {
            stateMachine.player.spriteRenderer.flipX = false;
            for (int i = 0; i < stateMachine.player.comboColliders.Count; i++)
            {
                stateMachine.player.comboColliders[i].transform.localPosition = stateMachine.player.weaponHitBoxRightPos[i];
            }
            ToggleAttackEffect();
        }
    }
}
