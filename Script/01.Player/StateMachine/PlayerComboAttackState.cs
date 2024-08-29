using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboAttackState : PlayerAttackState
{
    private bool alreadyAppliedCombo;
    private bool canceledCombo;

    private int comboIndex;

    AttackInfoData attackInfoData;

    public PlayerComboAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.player.animationData.comboAttackParameterHash);

        comboIndex = stateMachine.comboIndex;

        attackInfoData = stateMachine.player.data.attackData.GetAttackInfoData(comboIndex);
        stateMachine.player.animator.SetInteger("Combo", comboIndex);

        stateMachine.isComboAttacking = true;

        alreadyAppliedCombo = false;
        canceledCombo = false;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.player.animationData.comboAttackParameterHash);

        if (!alreadyAppliedCombo)
        {
            stateMachine.comboIndex = comboIndex;
        }
        if (canceledCombo)
        {
            stateMachine.comboIndex = 0;
        }
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(stateMachine.player.animator, "Attack");

        if (normalizedTime < 1f)
        {
            if (normalizedTime >= attackInfoData.comboTransitionTime)
            {
                TryComboAttack();
            }

            if (normalizedTime >= attackInfoData.forceTransitionTime)
            {
                TryApplyForce();
            }
        }
        else
        {
            stateMachine.isApplyForce = false;
            stateMachine.isComboAttacking = false;

            if (alreadyAppliedCombo)
            {
                stateMachine.comboIndex = attackInfoData.comboStateIndex;
                stateMachine.ChangeState(stateMachine.comboAttackState);
            }
            else
            {
                canceledCombo = true;
                if (IsGround())
                {
                    stateMachine.ChangeState(stateMachine.idleState);
                }
                else
                {
                    stateMachine.ChangeState(stateMachine.fallState);
                }
            }
        }
    }

    private void TryComboAttack()
    {
        if (alreadyAppliedCombo) return;

        if (attackInfoData.comboStateIndex == -1) return;

        if (!stateMachine.isPressAttack) return;

        alreadyAppliedCombo = true;
    }

    private void TryApplyForce()
    {
        if (stateMachine.isApplyForce) return;

        if (!stateMachine.isPressAttack) return;

        stateMachine.isApplyForce = true;

        Vector2 direction = stateMachine.player.spriteRenderer.flipX ? Vector2.left : Vector2.right;
        ForceMove(direction, attackInfoData.force);
    }
}
