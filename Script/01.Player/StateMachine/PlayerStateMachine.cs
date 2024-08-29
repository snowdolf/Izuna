using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player player { get; }

    public Vector2 movementInput { get; set; }
    public float movementSpeed { get; private set; }
    public float movementSpeedModifier { get; set; } = 1f;

    public float jumpForce { get; set;}

    public bool isAttacking { get; set; }
    public bool isPressAttack { get; set; }
    public bool isApplyForce { get; set; }
    public bool isComboAttacking { get; set; }
    public int comboIndex { get; set; }

    public PlayerIdleState idleState { get; }
    public PlayerRunState runState { get; }

    public PlayerJumpState jumpState { get; }
    public PlayerFallState fallState { get; }

    public PlayerComboAttackState comboAttackState { get;}

    public PlayerChainState chainState { get; }

    public PlayerDeathState deathState { get; }

    public PlayerStateMachine(Player player)
    {
        this.player = player;
        movementSpeed = player.data.groundData.baseSpeed;

        idleState = new PlayerIdleState(this);
        runState = new PlayerRunState(this);

        jumpState = new PlayerJumpState(this);
        fallState = new PlayerFallState(this);

        comboAttackState = new PlayerComboAttackState(this);

        chainState = new PlayerChainState(this);

        deathState = new PlayerDeathState(this);
    }
}
