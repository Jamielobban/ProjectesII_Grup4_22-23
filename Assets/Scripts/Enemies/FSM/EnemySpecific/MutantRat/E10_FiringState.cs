using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class E10_FiringState : FiringState
{
    Enemy10 enemy;
    float lastTimeAttackExit;
    bool animationDone;
    public E10_FiringState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_FiringState stateData, Enemy10 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
        lastTimeAttackExit = 0;
        animationDone = true;
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

        if(Time.time - lastTimeAttackExit >= stateData.timeBetweenShoots && lastTimeAttackExit != 0 && enemy.anim.GetBool("waitingTimeAttack"))
        {
            enemy.anim.SetBool("waitingTimeAttack", false);
        }

        inRange = entity.vectorToPlayer.magnitude < enemy.enemyData.stopDistanceFromPlayer + 5;

        if (!inRange && animationDone && enemy.anim.GetBool("waitingTimeAttack"))
        {
            stateMachine.ChangeState(enemy.chasingState);
        }

        if (((angle < 90 && angle > -90) && enemy.transform.localScale.x < 0) || ((angle > 90 || angle < -90) && enemy.transform.localScale.x > 0) && animationDone)
        {
            enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);

        }

        if(animationDone)
        enemy.GetComponentsInChildren<Transform>().Where(t => (t.gameObject.CompareTag("FirePoint"))).ToArray()[0].localRotation = Quaternion.Euler(0, 0, angle * Mathf.Sign(enemy.transform.localScale.x));
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void StartAnimation()
    {
        animationDone = false;
    }

    public void EndAttack()
    {
        lastTimeAttackExit = Time.time;
        enemy.anim.SetBool("waitingTimeAttack", true);
        animationDone = true;
    }

    public void Shoot()
    {
        GameObject bullet = GameObject.Instantiate(stateData.bulletType, enemy.GetComponent<Entity>().GetFirePointTransform().position, Quaternion.identity);
        
        //fireSoundKey = AudioManager.Instance.LoadSound(stateData.shootShound, enemy.GetComponent<Entity>().GetFirePointTransform().position);
        

        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * 8, ForceMode2D.Impulse);

        bullet.GetComponentInChildren<SpriteRenderer>().DOFade(0, 0.7f);
    }
}
