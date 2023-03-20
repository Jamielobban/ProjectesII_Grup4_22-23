using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;

public class E1_FiringState : FiringState
{
    
    private Enemy1 enemy;
    private bool nextShootReady;
    int a;
    bool b;
    float enterTime;
    const float attackAnim1Duration = 0.7f;
    const float attackAnim2Duration = 1.4f;
    
    int? fireSoundKey;    
    int? attackSoundsKey;    
    float attackDuration;
    bool animationDone = true;

    public E1_FiringState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_FiringState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
        
        //attackDuration += 0.2f * (stateData.numberOfBursts-1);
    }

    public override void Enter()
    {
        base.Enter();

        if (enemy.GetVariant() == Enemy1Variants.TURRET)
        {
            enemy.anim.SetInteger("AttackType", 0);
            attackDuration = attackAnim2Duration;
        }
        else
        {
            enemy.anim.SetInteger("AttackType", 1);
            attackDuration = attackAnim1Duration;
        }

        nextShootReady = false;
        a = 0;
        b = false;

        enterTime = Time.time;

        attackSoundsKey = AudioManager.Instance.LoadSound(stateData.attackSounds, enemy.transform, 0, true);

        //if (!enemy.anim.GetBool("waitingTimeAttack") && !(Time.time - lastShootTime >= stateData.timeBetweenShoots))
        //{
        //    enemy.anim.SetBool("waitingTimeAttack", true);
        //}
        if (enemy.GetVariant() == Enemy1Variants.TURRET)
        {
            enemy.firePoint.localPosition = new Vector3(0, 0, 0);

        }
        else
        {
            enemy.firePoint.localPosition = new Vector3(3.94f, -1.55f, 0);
        }


        if (Time.time - lastShootTime >= stateData.timeBetweenShoots)
        {
            enemy.anim.SetBool("waitingTimeAttack", false);
        }
        else
        {
            enemy.anim.SetBool("waitingTimeAttack", true);
        }
    }

    public override void Exit()
    {
        base.Exit();

        if (!enemy.anim.GetBool("waitingTimeAttack"))
        {
            enemy.anim.SetBool("waitingTimeAttack", true);
        }

        if (attackSoundsKey.HasValue)
        {
            AudioManager.Instance.RemoveAudio(attackSoundsKey.Value);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        inRange = entity.vectorToPlayer.magnitude  < enemy.enemyData.stopDistanceFromPlayer + 5;

        if (!inRange && animationDone)
        {
            stateMachine.ChangeState(enemy.chasingState);
        }

        if(enemy.anim.GetBool("waitingTimeAttack") && Time.time - lastShootTime >= stateData.timeBetweenShoots - attackDuration)
        {
            enemy.anim.SetBool("waitingTimeAttack", false);
        }

        nextShootReady = (Time.time - lastShootTime >= stateData.timeBetweenShoots) && Time.time >= enterTime + attackDuration;

        if (enemy.GetVariant() == Enemy1Variants.BIGFATMAN && nextShootReady)
        {
            Machinegun();
        }



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
        //Debug.Log(angle);

        if ( ((angle < 90 && angle > -90) && enemy.transform.localScale.x < 0) || ((angle > 90 || angle < -90 ) && enemy.transform.localScale.x > 0))        
        {
            enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);
            
        }

        enemy.firePoint.localRotation = Quaternion.Euler(0, 0, angleFirePoint * Mathf.Sign(enemy.transform.localScale.x));
        //enemy.rb.rotation = angle;

        //if (nextShootReady)
        //{
        //    if (enemy.anim.GetBool("waitingTimeAttack"))
        //    {
        //        enemy.anim.SetBool("waitingTimeAttack", false);
        //    }

        //    if (enemy.GetVariant() != Enemy1Variants.BIGFATMAN)
        //    {

        //        float waitTime = 0;
        //        for (int j = 0; j < stateData.numberOfBursts; j++, waitTime += 0.2f)
        //        {
        //            FunctionTimer.Create(FireProjectile, waitTime);
        //            FunctionTimer.Create(() => { if (!enemy.GetIfIsDead()) enemy.anim.SetBool("waitingTimeAttack", true); }, waitTime + 0.2f);
        //        }
        //    }
        //    else
        //    {
        //        Machinegun();
        //    }

        //    lastShootTime = Time.time;
        //    enemy.anim.SetBool("waitingTimeAttack", true);
        //}
        //else if (!enemy.anim.GetBool("waitingTimeAttack"))
        //{
        //    enemy.anim.SetBool("waitingTimeAttack", true);
        //}
    }

    public void AnimDoneTrue()
    {
        animationDone = true;
    }
    public void AnimDoneFalse()
    {
        animationDone = false;
    }
    public void ShootLoop()
    {
        //Debug.Log("In1");
        if (enemy.GetVariant() != Enemy1Variants.BIGFATMAN)
        {
            //Debug.Log("In2");


            float waitTime = 0;
            for (int j = 0; j < stateData.numberOfBursts; j++, waitTime += 0.2f)
            {
                FunctionTimer.Create(() =>
                {
                    if(enemy != null && !enemy.GetIfIsDead())
                    {
                        FireProjectile();
                    }
                }, waitTime);
                FunctionTimer.Create(() => {
                    if (enemy != null && !enemy.GetIfIsDead())
                    {
                        enemy.anim.SetBool("waitingTimeAttack", true);
                    }
                }, waitTime + 0.2f);
            }
        }
        //else
        //{
        //    Machinegun();             
        //}

        lastShootTime = Time.time;
    }
    void FireProjectile()
    {
        int par = 0;
        if (stateData.angleOfCone % 2 == 0)
            par = 5;

        for (int i = 0, grados = ((stateData.angleOfCone / 2) * -10) + par; i < stateData.angleOfCone; i++, grados += 10)
        {
            GameObject bullet = GameObject.Instantiate(stateData.bulletType, enemy.GetComponent<Entity>().GetFirePointTransform().position, enemy.GetComponent<Entity>().GetFirePointTransform().rotation);
            //instance.GetComponent<EnemyProjectile>().bulletDamage = enemyBulletDamage;
            fireSoundKey = AudioManager.Instance.LoadSound(stateData.shootShound, enemy.GetComponent<Entity>().GetFirePointTransform().position);
            bullet.transform.Rotate(0, 0, bullet.transform.rotation.z + grados);

            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * bullet.GetComponent<EnemyProjectile>().bulletData.speed, ForceMode2D.Impulse);
            //Destroy(instance, 3);
        }
        //FunctionTimer.Create(FireProjectile, waitTime);
                    //FunctionTimer.Create(()=>{ if(!enemy.GetIfIsDead())enemy.anim.SetBool("waitingTimeAttack", true);}, waitTime + 0.2f);
    }
    void Machinegun()
    {
        GameObject bullet = GameObject.Instantiate(stateData.bulletType, enemy.GetComponent<Entity>().GetFirePointTransform().position, enemy.GetComponent<Entity>().GetFirePointTransform().rotation);

        fireSoundKey = AudioManager.Instance.LoadSound(stateData.shootShound, enemy.GetComponent<Entity>().GetFirePointTransform().position);

        //Metralleta
        bullet.transform.Rotate(0, 0, bullet.transform.rotation.z + UnityEngine.Random.Range(-25, 25));


        //banda a banda
        //instance.transform.Rotate(0, 0, instance.transform.rotation.z + a);

        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * bullet.GetComponent<EnemyProjectile>().bulletData.speed, ForceMode2D.Impulse);

        lastShootTime = Time.time;
        //banda a banda
        //if (b)
        //{
        //    a += 10;
        //    if (a == 30)
        //    {
        //        b = false;
        //    }
        //}
        //else
        //{
        //    a -= 10;
        //    if (a == -30)
        //    {
        //        b = true;

        //    }
        //}
    }
}
