using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerBaseState
{
    public PlayerDeathState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.movementSpeedModifier = 0f;
        base.Enter();
        stateMachine.player.animator.SetTrigger(stateMachine.player.animationData.deathParameterHash);
        RemoveInputActionsCallbacks();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
