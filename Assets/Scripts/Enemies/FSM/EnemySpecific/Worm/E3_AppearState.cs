using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_AppearState : AppearState
{
    Enemy3 enemy;
    float enterTime;
    protected float angle;
    bool isVisible = false;
    SpriteRenderer enemySr;
    Animator enemyActr;
    BoxCollider2D enemyBc2d;

    public E3_AppearState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_AppearState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemySr = enemy.GetComponentInChildren<SpriteRenderer>();
        enemyActr = enemy.GetComponentInChildren<Animator>();
        enemyBc2d = enemy.GetComponent<BoxCollider2D>();        

        if (Mathf.Abs(enemy.vectorToPlayer.magnitude) <= stateData.maxDistanceToAppear)
        {
            enemy.holes[enemy.actualHole].GetComponentInChildren<SpriteRenderer>().enabled = false;

            enemySr.enabled = true;
                
            enterTime = Time.time;
            isVisible = true;

            enemyActr.SetBool("inRange", true);
            
        }
        else
        {
            enemySr.enabled = false;
            isVisible = false;
        }

    }

    public override void Exit()
    {
        base.Exit();
        enemyActr.SetBool("inRange", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time - enterTime >= stateData.timeAppearDuration && !enemy.GetIfIsDead() && isVisible)
        {
            stateMachine.ChangeState(enemy.firingState);
        }

        if (Mathf.Abs(enemy.vectorToPlayer.magnitude) <= stateData.maxDistanceToAppear && !isVisible)
        {
            enemy.holes[enemy.actualHole].GetComponentInChildren<SpriteRenderer>().enabled = false;

            enemySr.enabled = true;

            enterTime = Time.time;
            isVisible = true;

            enemyActr.SetBool("inRange", true);
        }

        enemyBc2d.enabled = isVisible;
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
