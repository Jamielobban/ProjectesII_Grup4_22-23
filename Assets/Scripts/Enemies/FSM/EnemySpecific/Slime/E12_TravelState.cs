using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E12_TravelState : TravelState
{
    Enemy12 enemy;
    public E12_TravelState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_TravelState stateData, Enemy12 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        Vector3 aux;
        aux.x = Mathf.Abs(enemy.transform.position.x - enemy.actualDestination.x);
        aux.y = Mathf.Abs(enemy.transform.position.y - enemy.actualDestination.y);
        aux.z = 0;
        if (aux.x <= enemy.error.x && aux.y <= enemy.error.y)
        {
           enemy.actualDestination = enemy.pathManager.GetNewDestiny(enemy.zoneID);
            stateMachine.ChangeState(enemy.idleState);
        }

        if(enemy.actualDestination.x > enemy.transform.position.x)
        {
            enemy.transform.localScale = new Vector3(Mathf.Abs(enemy.transform.localScale.x), enemy.transform.localScale.y, enemy.transform.localScale.z);
        }
        else if(enemy.actualDestination.x < enemy.transform.position.x)
        {
            enemy.transform.localScale = new Vector3(-1 * Mathf.Abs(enemy.transform.localScale.x), enemy.transform.localScale.y, enemy.transform.localScale.z);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.actualDestination, enemy.velocity * Time.deltaTime);
    }
}
