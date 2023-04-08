using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class E15_FiringState : FiringState
{
    Enemy15 enemy;
    public bool doingAttack;

    float lastTimeGas = 0;
    float timeInGas = 8;
    bool doingGas = false;

    float lastTimeProjectile = 0;
    float timeInProjectile = 8;
    bool doingProjectile = false;

    float lastTimeBoomerang = 0;
    float timeInBoomerang = 8;
    bool doingBoomerang = false;

    float lastTimePunch = 0;
    float timeInPunch = 8;
    bool doingPunch = false;


    GameObject gasInstance;
    GameObject armsBoomerang;

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

        if (((angle < 90 && angle > -90) && enemy.transform.localScale.x < 0) || ((angle > 90 || angle < -90) && enemy.transform.localScale.x > 0))
        {
            enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);

        }

        enemy.firePoint.localRotation = Quaternion.Euler(0, 0, angleFirePoint * Mathf.Sign(enemy.transform.localScale.x));

        switch (enemy.mode)
        {
            case 1:

                if(!doingBoomerang && !doingGas && !doingPunch && Time.time - enemy.lastTimeExitState >= enemy.waitBetweenAttacks)
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

                if(doingGas && Time.time - lastTimeGas >= timeInGas)
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
                if (!doingGas && Time.time - enemy.lastTimeExitState >= enemy.waitBetweenAttacks)
                {
                    int random = Random.Range(1, 11);

                    if (random <= 10)
                    {

                    }

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
        enemy.firePoint.localPosition = new Vector3(-0.35f, 0.16f, 0);
        gasInstance = GameObject.Instantiate(enemy.gas, enemy.firePoint.position, enemy.gas.transform.rotation);
    }

    public void StartBoomerang()
    {
        enemy.agent.enabled = false;
        armsBoomerang = GameObject.Instantiate(enemy.boomerang, enemy.GetComponentInChildren<SpriteRenderer>().transform.position, Quaternion.identity);
        armsBoomerang.transform.DOMove(enemy.player.transform.position, 0.5f);
        FunctionTimer.Create(() =>
        {
            if (enemy != null && !enemy.GetIfIsDead() && armsBoomerang != null && armsBoomerang.activeInHierarchy)
            {
                armsBoomerang.transform.DOMove(enemy.GetComponentInChildren<SpriteRenderer>().transform.position, 0.5f);

                FunctionTimer.Create(() =>
                {
                    if (enemy != null && !enemy.GetIfIsDead() && armsBoomerang != null && armsBoomerang.activeInHierarchy)
                    {
                        GameObject.Destroy(armsBoomerang);

                        doingAttack = false;
                        doingBoomerang = false;

                        enemy.anim.SetBool("idle", true);
                        enemy.anim.SetBool("fire", false);
                        enemy.anim.SetBool("boomerang", false);
                        enemy.anim.SetBool("animationLoop", false);


                        enemy.lastTimeExitState = Time.time;
                    }
                }, 0.5f);
            }
        }, 0.5f);
    }
    
}
