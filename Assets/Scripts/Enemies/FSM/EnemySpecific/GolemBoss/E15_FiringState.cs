using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class E15_FiringState : FiringState
{
    Enemy15 enemy;
    public bool doingAttack;

    float lastTimeGas = 0;
    float timeInGas = 1.9f;
    bool doingGas = false;

    float lastTimeProjectile = 0;
    float timeInProjectile = 1.5f;
    bool doingProjectile = false;

    float lastTimeBoomerang = 0;
    float timeInBoomerang = 8;
    bool doingBoomerang = false;

    float lastTimePunch = 0;
    float timeInPunch = 1.5f;
    bool doingPunch = false;


    GameObject gasInstance;


    int timesFeed = 0;

    public bool playerInsidepunchArea;
    public bool playerInsideGasArea;

    public E15_FiringState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_FiringState stateData, Enemy15 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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

        if(!doingGas && !doingPunch)
        {
            if (((angle < 90 && angle > -90) && enemy.transform.localScale.x < 0) || ((angle > 90 || angle < -90) && enemy.transform.localScale.x > 0))
            {
                enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);

            }
        }       

        enemy.firePoint.localRotation = Quaternion.Euler(0, 0, angleFirePoint * Mathf.Sign(enemy.transform.localScale.x));

        if(enemy.transform.localScale.x > 0)
        {
            enemy.gas.transform.rotation = Quaternion.Euler(0, 0, -41.3f);
        }
        else
        {
            enemy.gas.transform.rotation = Quaternion.Euler(0, 0, -131.3f);
        }

        switch (enemy.mode)
        {
            case 1:

                if (!doingBoomerang && !doingGas && !doingPunch && Time.time - enemy.lastTimeExitState >= enemy.waitBetweenAttacks)
                {

                    if (playerInsidepunchArea)
                    {
                        doingAttack = true;


                        doingBoomerang = false;
                        enemy.anim.SetBool("boomerang", false);


                        enemy.anim.SetBool("idle", false);
                        enemy.anim.SetBool("fire", true);
                        enemy.anim.SetBool("punch", true);
                        enemy.anim.SetBool("animationLoop", false);

                        doingPunch = true;

                        enemy.groundSoundKey = AudioManager.Instance.LoadSound(enemy.groundSound, enemy.GetComponentInChildren<CircleCollider2D>().bounds.center, 0.7f);

                        lastTimePunch = Time.time;
                    }
                    else if (playerInsideGasArea)
                    {
                        doingAttack = true;
                        doingBoomerang = false;

                        enemy.anim.SetBool("idle", false);
                        enemy.anim.SetBool("fire", true);
                        enemy.anim.SetBool("boomerang", false);
                        enemy.anim.SetBool("gas", true);
                        enemy.anim.SetBool("animationLoop", false);

                        doingGas = true;

                        enemy.gasSoundKey = AudioManager.Instance.LoadSound(enemy.gasSound, enemy.firePoint, 0.5f);

                        if (enemy.gasSoundKey.HasValue)
                            AudioManager.Instance.GetAudioFromDictionaryIfPossible(enemy.gasSoundKey.Value).pitch = 2;

                        lastTimeGas = Time.time;
                    }
                    else
                    {
                        doingAttack = true;
                        doingBoomerang = true;

                        enemy.anim.SetBool("idle", false);
                        enemy.anim.SetBool("fire", true);
                        enemy.anim.SetBool("boomerang", true);
                        enemy.anim.SetBool("gas", false);
                        enemy.anim.SetBool("animationLoop", true);

                        lastTimeBoomerang = Time.time;

                        StartBoomerang();
                    }
                }

                if(!doingGas && !doingPunch)
                {
                    if (!enemy.agent.enabled)
                    {
                        enemy.agent.enabled = true;
                    }
                    float xSpace;
                    if(enemy.transform.localScale.x > 0)
                    {
                        xSpace = -2;
                    }
                    else
                    {
                        xSpace = 2;
                    }
                    enemy.agent.destination = enemy.player.transform.position + new Vector3(xSpace, 2,0);
                }
                else
                {
                    enemy.agent.enabled = false;
                }

                if (doingGas && Time.time - lastTimeGas >= timeInGas)
                {
                    doingAttack = false;
                    doingBoomerang = false;

                    enemy.anim.SetBool("idle", true);
                    enemy.anim.SetBool("fire", false);
                    enemy.anim.SetBool("boomerang", false);
                    enemy.anim.SetBool("gas", false);
                    enemy.anim.SetBool("animationLoop", false);

                    doingGas = false;


                    enemy.lastTimeExitState = Time.time;
                }

                if (doingPunch && Time.time - lastTimePunch >= timeInPunch)
                {
                    doingAttack = false;
                    doingBoomerang = false;

                    enemy.anim.SetBool("idle", true);
                    enemy.anim.SetBool("fire", false);
                    enemy.anim.SetBool("boomerang", false);
                    enemy.anim.SetBool("punch", false);
                    enemy.anim.SetBool("animationLoop", false);

                    doingPunch = false;


                    enemy.lastTimeExitState = Time.time;
                }

                break;
            case 2:
                if (!doingBoomerang && !doingGas && !doingPunch && !doingProjectile && Time.time - enemy.lastTimeExitState >= enemy.waitBetweenAttacks)
                {

                    if (playerInsidepunchArea)
                    {
                        doingAttack = true;


                        doingBoomerang = false;
                        enemy.anim.SetBool("boomerang", false);


                        enemy.anim.SetBool("idle", false);
                        enemy.anim.SetBool("fire", true);
                        enemy.anim.SetBool("punch", true);
                        enemy.anim.SetBool("animationLoop", false);

                        doingPunch = true;

                        enemy.groundSoundKey = AudioManager.Instance.LoadSound(enemy.groundSound, enemy.GetComponentInChildren<CircleCollider2D>().bounds.center, 0.7f);

                        lastTimePunch = Time.time;
                    }
                    else if (playerInsideGasArea)
                    {
                        doingAttack = true;
                        doingBoomerang = false;

                        enemy.anim.SetBool("idle", false);
                        enemy.anim.SetBool("fire", true);
                        enemy.anim.SetBool("boomerang", false);
                        enemy.anim.SetBool("gas", true);
                        enemy.anim.SetBool("animationLoop", false);

                        enemy.gasSoundKey = AudioManager.Instance.LoadSound(enemy.gasSound, enemy.firePoint, 0.5f);

                        if (enemy.gasSoundKey.HasValue)
                            AudioManager.Instance.GetAudioFromDictionaryIfPossible(enemy.gasSoundKey.Value).pitch = 2;

                        doingGas = true;

                        lastTimeGas = Time.time;
                    }
                    else
                    {
                        if(timesFeed >= 3)
                        {
                            doingAttack = true;
                            doingBoomerang = true;

                            enemy.anim.SetBool("idle", false);
                            enemy.anim.SetBool("fire", true);
                            enemy.anim.SetBool("boomerang", true);
                            enemy.anim.SetBool("gas", false);
                            enemy.anim.SetBool("animationLoop", true);

                            lastTimeBoomerang = Time.time;

                            timesFeed = 0;

                            StartBoomerang();
                        }
                        else
                        {
                            doingAttack = true;
                            doingProjectile = true;

                            timesFeed++;

                            enemy.anim.SetBool("idle", false);
                            enemy.anim.SetBool("fire", true);
                            enemy.anim.SetBool("projectile", true);
                            enemy.anim.SetBool("gas", false);
                            enemy.anim.SetBool("animationLoop", false);

                            lastTimeProjectile = Time.time;

                            enemy.explosionKey = AudioManager.Instance.LoadSound(enemy.explosion, enemy.transform, 0.3f);
                        }

                        
                    }
                }

                if (!doingGas && !doingPunch)
                {
                    if (!enemy.agent.enabled)
                    {
                        enemy.agent.enabled = true;
                    }
                    float xSpace;
                    if (enemy.transform.localScale.x > 0)
                    {
                        xSpace = -2;
                    }
                    else
                    {
                        xSpace = 2;
                    }
                    enemy.agent.destination = enemy.player.transform.position + new Vector3(xSpace, 2, 0);
                }
                else
                {
                    enemy.agent.enabled = false;
                }

                if (doingGas && Time.time - lastTimeGas >= timeInGas)
                {
                    doingAttack = false;
                    doingBoomerang = false;

                    enemy.anim.SetBool("idle", true);
                    enemy.anim.SetBool("fire", false);
                    enemy.anim.SetBool("boomerang", false);
                    enemy.anim.SetBool("gas", false);
                    enemy.anim.SetBool("animationLoop", false);

                    doingGas = false;


                    enemy.lastTimeExitState = Time.time;
                }

                if (doingPunch && Time.time - lastTimePunch >= timeInPunch)
                {
                    doingAttack = false;
                    doingBoomerang = false;

                    enemy.anim.SetBool("idle", true);
                    enemy.anim.SetBool("fire", false);
                    enemy.anim.SetBool("boomerang", false);
                    enemy.anim.SetBool("punch", false);
                    enemy.anim.SetBool("animationLoop", false);

                    doingPunch = false;


                    enemy.lastTimeExitState = Time.time;
                }

                if (doingProjectile && Time.time - lastTimeProjectile >= timeInProjectile)
                {
                    doingAttack = false;
                    doingProjectile = false;

                    enemy.anim.SetBool("idle", true);
                    enemy.anim.SetBool("fire", false);
                    enemy.anim.SetBool("projectile", false);
                    enemy.anim.SetBool("punch", false);
                    enemy.anim.SetBool("animationLoop", false);

                    doingProjectile = false;


                    enemy.lastTimeExitState = Time.time;
                }

                break;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void DoGas()
    {
        Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaa");
        enemy.firePoint.localPosition = new Vector3(1.65f, 0.16f, 0);
        gasInstance = GameObject.Instantiate(enemy.gas, enemy.firePoint.position, enemy.gas.transform.rotation);
    }

    public void StartBoomerang()
    {       

        enemy.boomerang.SetActive(true);
    }

    public void EndBoomerang()
    {
        enemy.boomerang.SetActive(false);

        doingAttack = false;
        doingBoomerang = false;

        enemy.anim.SetBool("idle", true);
        enemy.anim.SetBool("fire", false);
        enemy.anim.SetBool("boomerang", false);        
        enemy.anim.SetBool("animationLoop", false);       


        enemy.lastTimeExitState = Time.time;
    }

    public void SpawnRipple()
    {
        enemy.rippleDamage.SetActive(true);
    }

    public void ShootProjectile()
    {
        int numProj = Random.Range(3, 7);
        float gradosPerBullet = 30 / numProj;
        for(int i = 0; i < numProj; i++)
        {
            Vector3 rot;
            rot.x = enemy.firePoint.rotation.eulerAngles.x;
            rot.y = enemy.firePoint.rotation.eulerAngles.y;
            rot.z = enemy.firePoint.rotation.eulerAngles.z;
            GameObject projectile = GameObject.Instantiate(stateData.bulletType, enemy.firePoint.position, Quaternion.Euler(rot + new Vector3(0, 0, -15 + gradosPerBullet * i)));
            projectile.GetComponent<Rigidbody2D>().AddForce(projectile.transform.right * projectile.GetComponent<EnemyProjectile>().bulletData.speed, ForceMode2D.Impulse);
        }
    }
    
}
