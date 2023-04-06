using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E14_FiringState : FiringState
{
    Enemy14 enemy;

    float lastTime4Waves = 0;
    const float timeIn4Waves = 8;
    bool doing4Waves = false;

    public bool doingAttack;
    public E14_FiringState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_FiringState stateData, Enemy14 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        switch (enemy.mode)
        {
            case 1:
                if (!doing4Waves && Time.time - enemy.lastTimeExitState >= enemy.waitBetweenAttacks)
                {
                    int random = Random.Range(1, 11);

                    if (random <= 10)
                    {
                        doingAttack = true;
                        doing4Waves = true;
                        //enemy.firePoint.localPosition = new Vector3(-0.05f, -0.46f, 0);
                        
                        enemy.anim.SetBool("idle", false);
                        enemy.anim.SetBool("fire", true);
                        enemy.anim.SetBool("4waves", true);
                        lastTime4Waves = Time.time;
                    }
                }
                break;
            case 2:

                break;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void GenerateFireWave()
    {
        GameObject waveSword1Left = GameObject.Instantiate(enemy.flameWave, new Vector3(-6.7f - 0.65f + enemy.transform.position.x, -4.756f + enemy.transform.position.y, 0), Quaternion.identity);
        waveSword1Left.GetComponent<SpawnFire>().id = 1;
        waveSword1Left.GetComponent<SpawnFire>().player = enemy.player;
        GameObject waveSword1Right = GameObject.Instantiate(enemy.flameWave, new Vector3(6.7f + 0.65f + enemy.transform.position.x, -4.756f + enemy.transform.position.y, 0), Quaternion.identity);
        waveSword1Right.GetComponent<SpawnFire>().id = 1;
        waveSword1Right.GetComponent<SpawnFire>().player = enemy.player;


    }

    public void GenerateFireWaveFar()
    {
        GameObject waveSword2Left = GameObject.Instantiate(enemy.flameWave, new Vector3(-7.789f - 0.85f + enemy.transform.position.x, -4.756f + enemy.transform.position.y, 0), Quaternion.identity);
        waveSword2Left.GetComponent<SpawnFire>().id = 2;
        waveSword2Left.GetComponent<SpawnFire>().player = enemy.player;


        GameObject waveSword2Right = GameObject.Instantiate(enemy.flameWave, new Vector3(7.789f + 0.85f + enemy.transform.position.x, -4.756f + enemy.transform.position.y, 0), Quaternion.identity);
        waveSword2Right.GetComponent<SpawnFire>().id = 2;
        waveSword2Right.GetComponent<SpawnFire>().player = enemy.player;

    }

    public void ExitState()
    {
        if (doing4Waves)
        {
            doing4Waves = false;
            //enemy.firePoint.localPosition = new Vector3(-0.05f, -0.46f, 0);
            enemy.anim.SetBool("idle", true);
            enemy.anim.SetBool("fire", false);
            enemy.anim.SetBool("4waves", false);
        }

        doingAttack = false;
        enemy.lastTimeExitState = Time.time;
    }

    public void ChangeY()
    {
        enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 3.7f, enemy.transform.position.z);
    }

    public void ChangeY2()
    {
        enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y - 3.7f, enemy.transform.position.z);
    }
}
