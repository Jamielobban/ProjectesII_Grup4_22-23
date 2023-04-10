using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class E14_IdleState : IdleState
{
    Enemy14 enemy;
    float startTime;
    SpriteRenderer sr;
    bool changeStarted = false;

    

    public E14_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Enemy14 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        sr = enemy.GetComponentInChildren<SpriteRenderer>();

        sr.material.SetFloat("_OutlineAlpha", 0);

        startTime = Time.time;

        enemy.idleSwordsInstance = GameObject.Instantiate(enemy.idleSwords, sr.transform);

        enemy.CreateIdleSwordsSounds();
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
