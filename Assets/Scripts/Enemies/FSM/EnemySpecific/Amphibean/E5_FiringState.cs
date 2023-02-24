using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E5_FiringState : FiringState
{
    Enemy5 enemy;
    float lastAttackTime;
    int? fireSoundKey;

    bool attackStarted;

    public E5_FiringState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_FiringState stateData, Enemy5 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
        lastAttackTime = 0;
        attackStarted = false;        
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

        if (!inRange)
        {
            if (!attackStarted)
            {
                stateMachine.ChangeState(enemy.chasingState);
            }
        }

        if (enemy.anim.GetBool("waitingNewAttack") && Time.time - lastAttackTime >= stateData.timeBetweenShoots)
        {
            enemy.anim.SetBool("waitingNewAttack", false);
            enemy.anim.SetBool("attackDone", false);

        }

        if (((angle < 90 && angle > -90) && enemy.transform.localScale.x > 0) || ((angle > 90 || angle < -90) && enemy.transform.localScale.x < 0))
        {
            enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);

        }

        if (enemy.vectorToPlayer.magnitude >= 5) {
            if (!enemy.agent.enabled)
            {
                enemy.agent.enabled = true;
            }
        }
        else
        {
            if (enemy.agent.enabled && enemy.transform.position.y >= enemy.player.transform.position.y + 2)
            {
                if(enemy.player.position.x > enemy.transform.position.x)
                {
                    if(enemy.transform.position.x + 2 <= enemy.player.transform.position.x)
                    {
                        enemy.agent.enabled = false;
                    }
                }
                else
                {
                    if (enemy.player.position.x <= enemy.transform.position.x)
                    {
                        enemy.agent.enabled = false;
                    }
                }
            }
        }

        if (enemy.agent.enabled)
        {
            enemy.agent.SetDestination(new Vector3(enemy.player.position.x + -Mathf.Abs(enemy.player.localScale.x)*2, enemy.player.position.y + 2, enemy.transform.position.z));
        }

        enemy.firePoint.localRotation = Quaternion.Euler(0, 0, angleFirePoint * Mathf.Sign(enemy.transform.localScale.x));
    }

    public void AttackDone()
    {
        enemy.anim.SetBool("attackDone", true);
        enemy.anim.SetBool("waitingNewAttack", true);
        lastAttackTime = Time.time;
        
        attackStarted = false;

        FireProjectile();
    }

    public void AttackStarted()
    {
        attackStarted = true;        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
    }

    public void FireProjectile()
    {
        GameObject bullet = GameObject.Instantiate(stateData.bulletType, enemy.GetComponent<Entity>().GetFirePointTransform().position, enemy.GetComponent<Entity>().GetFirePointTransform().rotation);
        fireSoundKey = AudioManager.Instance.LoadSound(stateData.shootShound, enemy.GetComponent<Entity>().GetFirePointTransform().position);
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * bullet.GetComponentInChildren<EnemyProjectile>().bulletData.speed, ForceMode2D.Impulse);
        

        //shootDone = true;
    }
}
