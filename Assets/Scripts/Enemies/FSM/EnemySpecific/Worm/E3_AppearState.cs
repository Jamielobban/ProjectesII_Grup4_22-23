using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_AppearState : AppearState
{
    Enemy3 enemy;
    float enterTime;
    protected float angle;
    public E3_AppearState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_AppearState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.holes[enemy.actualHole].GetComponentInChildren<SpriteRenderer>().enabled = false;
        enterTime = Time.time;        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time - enterTime >= stateData.timeAppearDuration && !enemy.GetIfIsDead())
        {
            stateMachine.ChangeState(enemy.firingState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        angle = Mathf.Atan2(entity.vectorToPlayer.y, entity.vectorToPlayer.x) * Mathf.Rad2Deg;

        if (((angle < 90 && angle > -90) && enemy.transform.localScale.x < 0) || ((angle > 90 || angle < -90) && enemy.transform.localScale.x > 0))
        {
            enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);

        }
    }
}
