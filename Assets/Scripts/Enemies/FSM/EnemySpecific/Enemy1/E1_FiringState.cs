using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class E1_FiringState : FiringState
{
    private Enemy1 enemy;
    private bool nextShootReady;
    int a;
    bool b;
    public E1_FiringState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_FiringState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        nextShootReady = false;
        a = 0;
        b = false;
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

        //string a = "Time is: " + Time.time;
        //Debug.Log(a);
        //string b = "LastShootTime is: " + lastShootTime;
        //Debug.Log(b);
        //string c = "La resta es: " + (Time.time - lastShootTime);
        //Debug.Log(c);
        



        nextShootReady = Time.time - lastShootTime >= stateData.timeBetweenShoots;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        //if(angle > 90 && angle < 270)
        //{
        //    if(enemy.transform.localScale.x > 0)
        //    {
        //        enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);
        //    }
        //}
        Debug.Log(angle);

        if ( ((angle < 90 && angle > -90) && enemy.transform.localScale.x < 0) || ((angle > 90 || angle < -90 ) && enemy.transform.localScale.x > 0))        
        {
            enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);
            
        }

        enemy.GetComponentsInChildren<Transform>().Where(t => (t.gameObject.CompareTag("FirePoint"))).ToArray()[0].localRotation = Quaternion.Euler(0, 0, angle * Mathf.Sign(enemy.transform.localScale.x));
        //enemy.rb.rotation = angle;

        if (nextShootReady)
        {
            if(enemy.GetVariant() != Enemy1Variants.BIGFATMAN)
            {
                float waitTime = 0;
                for (int j = 0; j < stateData.numberOfBursts; j++, waitTime += 0.2f)
                {
                    FunctionTimer.Create(FireProjectile, waitTime);
                }
            }
            else
            {
                Machinegun();
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
    void Machinegun()
    {
        GameObject bullet = GameObject.Instantiate(stateData.bulletType, enemy.enemyData.firePoint.transform.position, enemy.enemyData.firePoint.rotation);

        AudioManager.Instance.PlaySound(stateData.shootShound, enemy.enemyData.firePoint.transform.position);

        //Metralleta
        bullet.transform.Rotate(0, 0, bullet.transform.rotation.z + UnityEngine.Random.Range(-25, 25));


        //banda a banda
        //instance.transform.Rotate(0, 0, instance.transform.rotation.z + a);

        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * bullet.GetComponent<EnemyProjectile>().bulletData.speed, ForceMode2D.Impulse);
        

        //banda a banda
        if (b)
        {
            a += 10;
            if (a == 30)
            {
                b = false;
            }
        }
        else
        {
            a -= 10;
            if (a == -30)
            {
                b = true;

            }
        }
    }
}
