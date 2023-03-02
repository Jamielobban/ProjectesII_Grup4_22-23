using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class E7_FiringState : FiringState
{
    Enemy7 enemy;
    float enterTime;
    GameObject bullet;
    public E7_FiringState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_FiringState stateData, Enemy7 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
        
    }

    public override void Enter()
    {
        base.Enter();
        enterTime = Time.time;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastTimeShoot = Time.time;
        enemy.timeBetweenShoots = Random.Range(5f, 10f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!enemy.doingAttack)
        {
            if (((angle < 90 && angle > -90) && enemy.transform.localScale.x > 0) || ((angle > 90 || angle < -90) && enemy.transform.localScale.x < 0))
            {
                enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);

            }           
        }

        enemy.firePoint.localRotation = Quaternion.Euler(0, 0, angleFirePoint * Mathf.Sign(enemy.transform.localScale.x));

        if (Time.time - enterTime >= stateData.stateDuration)
        {
            stateMachine.ChangeState(enemy.chasingState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void ShootProjectile()
    {
        bullet = GameObject.Instantiate(stateData.bulletType, enemy.GetComponent<Entity>().GetFirePointTransform().position, enemy.GetComponent<Entity>().GetFirePointTransform().rotation);
        bullet.transform.localScale = Vector3.zero;

        //fireSoundKey = AudioManager.Instance.LoadSound(stateData.shootShound, enemy.GetComponent<Entity>().GetFirePointTransform().position);
        bullet.transform.DOScale(1, 0.4f);
              
        
    }

    public void AnimationStarted()
    {
        enemy.doingAttack = true;
    }
    public void AnimationEnded()
    {
        enemy.doingAttack = false;
    }

    public void ApplyImpulse()
    {
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * bullet.GetComponent<EnemyProjectile>().bulletData.speed, ForceMode2D.Impulse);
    }
}
