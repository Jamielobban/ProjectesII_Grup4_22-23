using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class E8_SlashState : SlashState
{
    Enemy8 enemy;
    public bool doingAttack;
    float lastAttackTime;
    int? chainsSounds;
    public E8_SlashState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_SlashState stateData, Enemy8 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;       
    }

    public override void Enter()
    {
        base.Enter();
        lastAttackTime = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(!enemy.inRange && !doingAttack)
        {
            enemy.stateMachine.ChangeState(enemy.chasingState);
        }

        if(!doingAttack && Time.time - lastAttackTime >= stateData.timeBetwenSlashes)
        {
            doingAttack = true;
            StartAttack();            
        }
    }

    public void StartAttack()
    {
        enemy.transform.DOPunchPosition(enemy.vectorToPlayer.normalized * 5, 1, 2);
        chainsSounds = AudioManager.Instance.LoadSound(stateData.slashSound, enemy.transform);

        FunctionTimer.Create(() =>
        {
            lastAttackTime = Time.time;
            doingAttack = false;

        }, 1);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
