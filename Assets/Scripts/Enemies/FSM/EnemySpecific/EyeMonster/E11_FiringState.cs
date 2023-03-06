using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E11_FiringState : FiringState
{
    Enemy11 enemy;
    bool animEnded = true;
    float lastTimeInLoop;
    float startimeInLoop;
    bool isInLoop;

    public E11_FiringState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_FiringState stateData, Enemy11 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        isInLoop = false;
        startimeInLoop = 0;
        lastTimeInLoop = 0;        
    }

    public override void Exit()
    {
        base.Exit();
    }


    public void StartAnim()
    {
        animEnded = false;
    }

    public void StartLaser()
    {
        isInLoop = true;
        startimeInLoop = Time.time;
    }

    public void ShotProjectile()
    {

    }

    public void EndLaser()
    {

    }

    public void EndAnimation()
    {
        animEnded = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Mathf.Abs(enemy.vectorToPlayer.magnitude) >= enemy.distanceToPassToIdle && animEnded)
        {
            stateMachine.ChangeState(enemy.idleState);
        }

        if (((angle < 90 && angle > -90) && enemy.transform.localScale.x < 0) || ((angle > 90 || angle < -90) && enemy.transform.localScale.x > 0) && animEnded)
        {           
            enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);            
        }

        enemy.firePoint.localRotation = Quaternion.Euler(0, 0, angleFirePoint * Mathf.Sign(enemy.transform.localScale.x));

        if(isInLoop && Time.time - startimeInLoop >= stateData.stateDuration)
        {
            EndLaser();
            enemy.anim.SetBool("waitingAttackAgain", true);
            lastTimeInLoop = Time.time;
            isInLoop = false;
        }

        if(!isInLoop && Time.time - lastTimeInLoop >= stateData.timeBetweenShoots && enemy.anim.GetBool("waitingAttackAgain"))
        {
            enemy.anim.SetBool("waitingAttackAgain", false);
        }

        Debug.Log(isInLoop);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
