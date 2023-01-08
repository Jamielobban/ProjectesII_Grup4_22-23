using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class E3_FiringState : FiringState
{
    Enemy3 enemy;
    float enterTime;
    bool shootDone = false;
    int? shootSoundKey;

    public E3_FiringState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_FiringState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enterTime = Time.time;
        shootDone = false;

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time - enterTime >= stateData.stateDuration && !enemy.GetIfIsDead())
        {
            stateMachine.ChangeState(enemy.disappearState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (((angle < 90 && angle > -90) && enemy.transform.localScale.x < 0) || ((angle > 90 || angle < -90) && enemy.transform.localScale.x > 0))
        {
            enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);

        }

        enemy.firePoint.localRotation = Quaternion.Euler(0, 0, angleFirePoint * Mathf.Sign(enemy.transform.localScale.x));
    }

    public void FireProjectile()
    {
        GameObject bullet = GameObject.Instantiate(stateData.bulletType, enemy.GetComponent<Entity>().GetFirePointTransform().position, enemy.GetComponent<Entity>().GetFirePointTransform().rotation);
        shootSoundKey = AudioManager.Instance.LoadSound(stateData.shootShound, enemy.GetComponent<Entity>().GetFirePointTransform().position);
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * bullet.GetComponentInChildren<EnemyProjectile>().bulletData.speed, ForceMode2D.Impulse);

        //bullet.GetComponent<Transform>().DORotate(Vector3.forward, 1).SetLoops(-1);

        shootDone = true;
    }
}
