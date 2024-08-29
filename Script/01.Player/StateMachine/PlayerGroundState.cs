using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundState : PlayerBaseState
{
    private float idleTransitionDelay = 0.05f;
    private float idleTimer;

    public PlayerGroundState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.player.animationData.groundParameterHash);
        idleTimer = idleTransitionDelay;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.player.animationData.groundParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.movementInput == Vector2.zero)
        {
            idleTimer -= Time.deltaTime;

            if (idleTimer <= 0)
            {
                stateMachine.ChangeState(stateMachine.idleState);
                return;
            }
        }
        else
        {
            idleTimer = idleTransitionDelay;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // ground -> fall without jump
        if (!IsGround() && stateMachine.player.rigidBody.velocity.y < Physics.gravity.y * Time.fixedDeltaTime)
        {
            stateMachine.ChangeState(stateMachine.fallState);
        }
    }

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        if (stateMachine.movementInput == Vector2.zero) return;

        base.OnMovementCanceled(context);
    }

    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
        base.OnJumpStarted(context);
        stateMachine.ChangeState(stateMachine.jumpState);
    }
}
