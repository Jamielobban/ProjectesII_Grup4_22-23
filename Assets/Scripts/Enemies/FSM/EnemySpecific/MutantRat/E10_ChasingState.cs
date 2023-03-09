using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class E10_ChasingState : ChasingState
{
    Enemy10 enemy;
    private int? followSoundKey;
    public E10_ChasingState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChaseState stateData, Enemy10 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.agent.enabled = true;
        
    }

    public override void Exit()
    {
        base.Exit();

        enemy.agent.enabled = false;        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        
        if (((angle < 90 && angle > -90) && enemy.transform.localScale.x < 0) || ((angle > 90 || angle < -90) && enemy.transform.localScale.x > 0))
        {
            enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);

        }

        enemy.GetComponentsInChildren<Transform>().Where(t => (t.gameObject.CompareTag("FirePoint"))).ToArray()[0].localRotation = Quaternion.Euler(0, 0, angle * Mathf.Sign(enemy.transform.localScale.x));

        if(enemy.agent.enabled)
            enemy.agent.SetDestination(new Vector3(enemy.player.position.x, enemy.player.position.y, enemy.transform.position.z));

        inRange = entity.vectorToPlayer.magnitude < enemy.enemyData.stopDistanceFromPlayer;

        if (inRange)
        {
            stateMachine.ChangeState(enemy.firingState);
        }
    }

    public void StartAnimation()
    {
       
    }

    public void EndAttack()
    {
        
    }

    public void Shoot()
    {
        
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
