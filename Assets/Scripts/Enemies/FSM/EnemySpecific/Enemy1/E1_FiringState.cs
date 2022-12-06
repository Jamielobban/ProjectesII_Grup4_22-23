using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_FiringState : FiringState
{
    private Enemy1 enemy;
    private bool nextShootReady;

    public E1_FiringState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_FiringState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        nextShootReady = false;
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
            stateMachine.ChangeState(enemy.chasingState);
        }

        nextShootReady = Time.time - lastShootTime >= stateData.timeBetweenShoots;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (nextShootReady)
        {
            float waitTime = 0;
            for(int j = 0; j < stateData.numberOfBursts; j++, waitTime += 0.2f)
            {
                FunctionTimer.Create(FireProjectile, waitTime);
            }                   

            lastShootTime = Time.time;            
        }
    }
    void FireProjectile()
    {
        int par = 0;
        if (stateData.angleOfCone % 2 == 0)
            par = 5;

        for (int i = 0, grados = ((stateData.angleOfCone / 2) * -10) + par; i < stateData.angleOfCone; i++, grados += 10)
        {
            GameObject bullet = GameObject.Instantiate(stateData.bulletType, enemy.enemyData.firePoint.transform.position, enemy.enemyData.firePoint.rotation);
            //instance.GetComponent<EnemyProjectile>().bulletDamage = enemyBulletDamage;
            AudioManager.Instance.PlaySound(stateData.shootShound, enemy.enemyData.firePoint.transform.position);
            bullet.transform.Rotate(0, 0, bullet.transform.rotation.z + grados);

            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * bullet.GetComponent<EnemyProjectile>().bulletData.speed, ForceMode2D.Impulse);
            //Destroy(instance, 3);
        }
    }
}
