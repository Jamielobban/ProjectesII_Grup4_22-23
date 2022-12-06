using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_Chasingtate : ChasingState
{
    private Enemy1 enemy;
    
    public E1_Chasingtate(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChaseState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        inRange = entity.vectorToPlayer.magnitude < enemy.enemyData.stopDistanceFromPlayer;

        if (inRange)
        {
            stateMachine.ChangeState(enemy.firingState);
        }        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, enemy.player.position, -enemy.enemyData.speed * Time.fixedDeltaTime);

    }


}
