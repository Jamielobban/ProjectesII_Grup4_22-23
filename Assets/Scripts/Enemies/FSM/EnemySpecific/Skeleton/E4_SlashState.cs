using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_SlashState : SlashState
{
    private Enemy4 enemy;
    private bool nextSlashReady;
    private float lastSlashDone;
    bool lastLoopValue;
    bool inRange;
    bool animationDone = true;

    public E4_SlashState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_SlashState stateData, Enemy4 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
        nextSlashReady = true;
        lastSlashDone = 0;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        inRange = entity.vectorToPlayer.magnitude < enemy.enemyData.stopDistanceFromPlayer + 5;

        if (!inRange && animationDone)
        {
            stateMachine.ChangeState(enemy.chasingState);
        }

        lastLoopValue = nextSlashReady;
        nextSlashReady = Time.time - lastSlashDone <= stateData.timeBetwenSlashes;
        if(lastLoopValue != nextSlashReady && nextSlashReady == true)
        {
            enemy.WhichAttackDo();
            enemy.anim.SetBool("AttackType", enemy.attack0attack1);
        }
        enemy.anim.SetBool("SlashReady", nextSlashReady);
    }
    public void SetEndAttackTime()
    {
        lastSlashDone = Time.time;
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (((angle < 90 && angle > -90) && enemy.transform.localScale.x < 0) || ((angle > 90 || angle < -90) && enemy.transform.localScale.x > 0))
        {
            enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);

        }

        enemy.firePoint.localRotation = Quaternion.Euler(0, 0, angleFirePoint * Mathf.Sign(enemy.transform.localScale.x));
    }
}
