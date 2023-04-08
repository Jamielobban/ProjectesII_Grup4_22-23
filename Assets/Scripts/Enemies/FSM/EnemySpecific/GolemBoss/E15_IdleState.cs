using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class E15_IdleState : IdleState
{
    Enemy15 enemy;
    float startTime;
    SpriteRenderer sr;
    bool changeStarted = false;

    public E15_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Enemy15 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        sr = enemy.GetComponentInChildren<SpriteRenderer>();

        sr.material.SetFloat("_OutlineAlpha", 0);

        startTime = Time.time;
       
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time - startTime >= 1f && !changeStarted)
        {
            sr.material.DOFloat(1, "_OutlineAlpha", 1f);
            changeStarted = true;
        }

        if (Time.time - startTime >= 2.2f)
        {
            stateMachine.ChangeState(enemy.firingState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
