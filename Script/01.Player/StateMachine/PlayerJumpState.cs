using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.jumpForce = stateMachine.player.data.airData.jumpForce;
        ForceMove(Vector2.up, stateMachine.jumpForce);
        stateMachine.player.jumpDust.Play();
        SoundManager.instance.PlaySound(stateMachine.player.clipJump, 0.5f, 0f);

        base.Enter();
        StartAnimation(stateMachine.player.animationData.jumpParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.player.animationData.jumpParameterHash);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (stateMachine.player.rigidBody.velocity.y <= 0)
        {
            stateMachine.ChangeState(stateMachine.fallState);
            return;
        }
    }
}
