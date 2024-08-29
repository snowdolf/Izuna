using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string groundParameterName = "@Ground";
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string runParameterName = "Run";

    [SerializeField] private string airParameterName = "@Air";
    [SerializeField] private string jumpParameterName = "Jump";
    [SerializeField] private string fallParameterName = "Fall";

    [SerializeField] private string attackParameterName = "@Attack";
    [SerializeField] private string comboAttackParameterName = "ComboAttack";

    [SerializeField] private string deathParameterName = "Death";

    public int groundParameterHash { get; private set; }
    public int idleParameterHash { get; private set; }
    public int runParameterHash { get; private set; }

    public int airParameterHash { get; private set; }
    public int jumpParameterHash { get; private set; }
    public int fallParameterHash { get; private set; }

    public int attackParameterHash { get; private set; }
    public int comboAttackParameterHash { get; private set; }

    public int deathParameterHash { get; private set; }

    public void Initialize()
    {
        groundParameterHash = Animator.StringToHash(groundParameterName);
        idleParameterHash = Animator.StringToHash(idleParameterName);
        runParameterHash = Animator.StringToHash(runParameterName);

        airParameterHash = Animator.StringToHash(airParameterName);
        jumpParameterHash = Animator.StringToHash(jumpParameterName);
        fallParameterHash = Animator.StringToHash(fallParameterName);

        attackParameterHash = Animator.StringToHash(attackParameterName);
        comboAttackParameterHash = Animator.StringToHash(comboAttackParameterName);

        deathParameterHash = Animator.StringToHash(deathParameterName);
    }
}
