using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class E9_ChasingState : ChasingState
{
    Enemy9 enemy;
    public E9_ChasingState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChaseState stateData, Enemy9 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.agent.enabled = true;

        //followSoundKey = AudioManager.Instance.LoadSound(stateData.followSounds, enemy.transform, 0.2f, true);
    }

    public override void Exit()
    {
        base.Exit();

        enemy.agent.enabled = false;

        //if (followSoundKey.HasValue)
        //{
        //    AudioManager.Instance.RemoveAudio(followSoundKey.Value);
        //}
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (enemy.enemyVariant)
        {
            inRange = entity.vectorToPlayer.magnitude < enemy.enemyData.stopDistanceFromPlayer;

            if (inRange)
            {
                stateMachine.ChangeState(enemy.firingState);
            }
        }
        else
        {
            inRange = entity.vectorToPlayer.magnitude < enemy.enemyData.stopDistanceFromPlayer && entity.vectorToPlayer.magnitude >= 6;

            if (inRange)
            {
                stateMachine.ChangeState(enemy.firingState);
            }
        }

        if (enemy.enemyVariant && enemy.agent.enabled)
        {
            enemy.agent.SetDestination(new Vector3(enemy.player.position.x, enemy.player.position.y, enemy.transform.position.z));
        }
        else
        {
            if (entity.vectorToPlayer.magnitude >= enemy.enemyData.stopDistanceFromPlayer)
            {
                enemy.agent.SetDestination(new Vector3(enemy.player.position.x, enemy.player.position.y, enemy.transform.position.z));
            }
            else if (entity.vectorToPlayer.magnitude <= 7 && enemy.agent)
            {
                enemy.agent.SetDestination(enemy.player.position + (-enemy.vectorToPlayer.normalized * 7));
            }
        }
    }

    public void EndAnimation()
    {
    }

    public void ShootSlash()
    {
    }

    public void StartAnimation()
    {
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        //if (((angle < 90 && angle > -90) && enemy.transform.localScale.x < 0) || ((angle > 90 || angle < -90) && enemy.transform.localScale.x > 0))
        //{
        //    enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);

        //}

        enemy.GetComponentsInChildren<Transform>().Where(t => (t.gameObject.CompareTag("FirePoint"))).ToArray()[0].localRotation = Quaternion.Euler(0, 0, angle * Mathf.Sign(enemy.transform.localScale.x));

        //enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, enemy.player.position, enemy.enemyData.speed * Time.fixedDeltaTime);
        
    }
}
