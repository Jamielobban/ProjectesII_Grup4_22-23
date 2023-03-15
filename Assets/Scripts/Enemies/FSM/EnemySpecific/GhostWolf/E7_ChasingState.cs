using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class E7_ChasingState : ChasingState
{
    Enemy7 enemy;
    int? chasingSounds;
    public E7_ChasingState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChaseState stateData, Enemy7 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.agent.enabled = true;
        chasingSounds = AudioManager.Instance.LoadSound(stateData.followSounds, enemy.transform, 0, true);
    }

    public override void Exit()
    {
        base.Exit();

        enemy.agent.enabled = false;

        if (chasingSounds.HasValue)
        {
            AudioManager.Instance.RemoveAudio(chasingSounds.Value);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (enemy.agent.enabled)
        {
            if(enemy.transform.position.x <= enemy.player.transform.position.x)
            {
                enemy.agent.SetDestination(new Vector3(enemy.player.position.x - 2, enemy.player.position.y, enemy.transform.position.z));
            }
            else
            {
                enemy.agent.SetDestination(new Vector3(enemy.player.position.x + 2, enemy.player.position.y, enemy.transform.position.z));
            }
        }

        if(enemy.GetComponentInChildren<PlayerDetetctorArea>().playerInside && !enemy.doingAttack)
        {
            enemy.stateMachine.ChangeState(enemy.slashState);
        }

        if(enemy.vectorToPlayer.magnitude >= enemy.distanceToPassToIdle && !enemy.doingAttack)
        {
            enemy.stateMachine.ChangeState(enemy.idleState);
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
