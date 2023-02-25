using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E5_FiringState : FiringState
{
    Enemy5 enemy;
    float lastAttackTime;
    int? fireSoundKey;

    bool attackStarted;
    bool fireBreathStarted;
    float timeBetweenBreaths = 0.2f;
    float lastBreathAttack;

    public E5_FiringState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_FiringState stateData, Enemy5 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
        lastAttackTime = 0;
        attackStarted = false;
        fireBreathStarted = false;
        lastBreathAttack = 0;
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

        if (enemy.anim.GetBool("waitingNewBallAttack") && Time.time - lastAttackTime >= stateData.timeBetweenShoots)
        {
            enemy.anim.SetBool("waitingNewBallAttack", false);
            enemy.anim.SetBool("attackDone", false);

        }

        if (enemy.anim.GetBool("waitingNewFlamesAttack") && Time.time - lastBreathAttack >= timeBetweenBreaths)
        {
            enemy.anim.SetBool("waitingNewFlamesAttack", false);
            enemy.anim.SetBool("attackDone", false);

        }

        if (((angle < 90 && angle > -90) && enemy.transform.localScale.x > 0)|| ((angle > 90 || angle < -90) && enemy.transform.localScale.x < 0))
        {
            if (!fireBreathStarted)
            {
                enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);
            }
        }

        //if (enemy.vectorToPlayer.magnitude >= 5)
        //{
        //    if (!enemy.agent.enabled && !fireBreathStarted)
        //    {
        //        enemy.agent.enabled = true;
        //    }
        //}
        //else
        //{
        //    if (enemy.agent.enabled && enemy.transform.position.y >= enemy.player.transform.position.y + 2f)
        //    {
        //        if (enemy.player.position.x > enemy.transform.position.x)
        //        {
        //            if (enemy.transform.position.x + 2f <= enemy.player.transform.position.x)
        //            {
        //                enemy.agent.enabled = false;
        //                enemy.longRange = false;
        //                enemy.anim.SetBool("longRange", enemy.longRange);
        //            }
        //        }
        //        else
        //        {
        //            if (enemy.player.position.x <= enemy.transform.position.x)
        //            {
        //                enemy.agent.enabled = false;
        //                enemy.longRange = false;
        //                enemy.anim.SetBool("longRange", enemy.longRange);
        //            }
        //        }
        //    }
        //}

        //if (enemy.agent.enabled)
        //{
        //    enemy.agent.SetDestination(new Vector3(enemy.player.position.x + -Mathf.Abs(enemy.player.localScale.x) * 2f, enemy.player.position.y + 2f, enemy.transform.position.z));
        //    enemy.longRange = true;
        //    enemy.anim.SetBool("longRange", enemy.longRange);

        //}

        if (enemy.longRange == false)
        {
            enemy.agent.enabled = false;
        }
        else if(!fireBreathStarted)
        {
            enemy.agent.enabled = true;            

            if(enemy.player.transform.position.x >= enemy.transform.position.x)
            {
                enemy.agent.SetDestination(new Vector3(enemy.player.position.x - 3f, enemy.player.position.y + 3f, enemy.transform.position.z));
            }
            else
            {
                enemy.agent.SetDestination(new Vector3(enemy.player.position.x + 3f, enemy.player.position.y + 3f, enemy.transform.position.z));
            }
        }

        enemy.anim.SetBool("longRange", enemy.longRange);

        enemy.firePoint.localRotation = Quaternion.Euler(0, 0, angleFirePoint * Mathf.Sign(enemy.transform.localScale.x));
        
    }

    public void AttackDone()
    {        
        enemy.anim.SetBool("attackDone", true);
        enemy.anim.SetBool("waitingNewBallAttack", true);
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

    public void StartFireBreath()
    {
        attackStarted = true;
        fireBreathStarted = true;

        //Instantiate fum
        float aux = 0;
        if(enemy.transform.localScale.x < 0)
        {
            aux = 0.24f;
        }
        else
        {
            aux = -0.24f;
        }
        GameObject smoke = GameObject.Instantiate(enemy.smokePrefab, enemy.GetComponent<Entity>().transform.position + new Vector3(aux, 2f,0), Quaternion.identity);
    }

    public void DoFireBreath()
    {
        //Instantiate foc
        GameObject flames = GameObject.Instantiate(enemy.fireBreathPrefab, enemy.GetComponent<Entity>().GetFirePointTransform().position, Quaternion.identity);
        if(enemy.transform.localScale.x < 0)
        {
            flames.GetComponent<SpriteRenderer>().flipX = true;
        }

        FunctionTimer.Create(() =>
        {
            if(enemy.gameObject != null)
            {
                enemy.flamesAreOn = true;
            }
        }, 0.1f);

        FunctionTimer.Create(() =>
        {
            enemy.anim.SetBool("attackDone", true);
            enemy.anim.SetBool("waitingNewFlamesAttack", true);
            lastBreathAttack = Time.time;

            attackStarted = false;
            fireBreathStarted = false;
            if (enemy.gameObject != null)
            {
                enemy.flamesAreOn = false;
            }

        }, 1.0f);
    }

    public void FireProjectile()
    {
        GameObject bullet = GameObject.Instantiate(stateData.bulletType, enemy.GetComponent<Entity>().GetFirePointTransform().position, enemy.GetComponent<Entity>().GetFirePointTransform().rotation);
        fireSoundKey = AudioManager.Instance.LoadSound(stateData.shootShound, enemy.GetComponent<Entity>().GetFirePointTransform().position);
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * bullet.GetComponentInChildren<EnemyProjectile>().bulletData.speed, ForceMode2D.Impulse);
        

        //shootDone = true;
    }
}
