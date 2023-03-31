using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E13_IdleState : IdleState
{
    Enemy13 enemy;
    public E13_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Enemy13 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void FlipLeft()
    {
        Vector3 aux = enemy.transform.localScale;
        aux.x = -1 * Mathf.Abs(aux.x);
        enemy.transform.localScale = aux;

    }
    public void FlipRight()
    {
        
        Vector3 aux = enemy.transform.localScale;
        aux.x = Mathf.Abs(aux.x);
        enemy.transform.localScale = aux;
        

    }

    public void EndUp()
    {
        
    }
    public void EndDown()
    {
        
    }
    public void EndEyesThrow()
    {
    }

}