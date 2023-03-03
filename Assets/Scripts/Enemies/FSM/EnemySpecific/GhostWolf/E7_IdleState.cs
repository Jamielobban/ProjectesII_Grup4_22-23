using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class E7_IdleState : IdleState
{
    Enemy7 enemy;
    Vector3 actualDestination;
    Vector3 error;
    int randomIndex;
    int actualIndex;
    public E7_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Enemy7 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.agent.enabled = true;

        actualIndex = Random.Range(0, enemy.idleTravelPoints.Length);
        actualDestination = enemy.idleTravelPoints[actualIndex];

        error = new Vector3(0.1f, 0.1f, 0);
    }

    public override void Exit()
    {
        base.Exit();

        enemy.agent.enabled = false;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (enemy.vectorToPlayer.magnitude < stateData.rangeToPassToChasing)
        {
            enemy.stateMachine.ChangeState(enemy.chasingState);
        }
        else
        {
            Vector3 aux;            
            aux.x = Mathf.Abs(enemy.transform.position.x - actualDestination.x);
            aux.y = Mathf.Abs(enemy.transform.position.y - actualDestination.y);
            aux.z = 0;
            if (aux.x <= error.x && aux.y <= error.y)
            {
                do { randomIndex = Random.Range(0, enemy.idleTravelPoints.Length); } while(randomIndex == actualIndex);
                actualIndex = randomIndex;
                actualDestination = enemy.idleTravelPoints[actualIndex];
            }

            enemy.agent.SetDestination(actualDestination);
        }
        

        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        //if (((angle < 90 && angle > -90) && enemy.transform.localScale.x > 0) || ((angle > 90 || angle < -90) && enemy.transform.localScale.x < 0))
        //{
        //    enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);

        //}


        enemy.GetComponentsInChildren<Transform>().Where(t => (t.gameObject.CompareTag("FirePoint"))).ToArray()[0].localRotation = Quaternion.Euler(0, 0, angle * Mathf.Sign(enemy.transform.localScale.x));
    }

    public void AnimationStarted()
    {
        enemy.doingAttack = true;
    }
    public void AnimationEnded()
    {
        enemy.doingAttack = false;
    }
}
