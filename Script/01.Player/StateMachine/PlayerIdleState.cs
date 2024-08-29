using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.movementSpeedModifier = 0f;
        base.Enter();
        StartAnimation(stateMachine.player.animationData.idleParameterHash);
    }

    public override void Exit()
    {
        stateMachine.movementSpeedModifier = 1f;
        base.Exit();
        StopAnimation(stateMachine.player.animationData.idleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if(stateMachine.movementInput != Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.runState);
            return;
        }
    }
}
