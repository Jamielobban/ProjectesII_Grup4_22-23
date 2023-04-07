using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class E14_FiringState : FiringState
{
    Enemy14 enemy;

    float lastTime4Waves = 0;
    float timeIn4Waves = 8;
    bool doing4Waves = false;

    float lastTimeMultiSword = 0;
    float timeInMultiSword = 8;
    bool doingMultiSword = false;

    float lastTimeSpawnNear = 0;
    float timeInSpawnNear = 8;
    bool doingSpawnNear = false;

    float lastTimeSwordMissiles = 0;
    float timeInSwordMissiles = 8;
    bool doingSwordMissiles = false;

    public bool doingAttack;
    GameObject multipleSwords;

    GameObject idleSwords;
    GameObject a2Swords;
    GameObject a1Swords;

    public E14_FiringState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_FiringState stateData, Enemy14 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        idleSwords = enemy.idleSwordsInstance;
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
                if (!doingSwordMissiles && !doingSpawnNear && Time.time - enemy.lastTimeExitState >= enemy.waitBetweenAttacks)
                {
                    int random = Random.Range(1, 11);

                    if (random <= 3)
                    {
                        doingAttack = true;
                        doingSwordMissiles = true;
                        //enemy.firePoint.localPosition = new Vector3(-0.05f, -0.46f, 0);

                        enemy.anim.SetBool("idle", false);
                        enemy.anim.SetBool("fire", true);
                        enemy.anim.SetBool("missileSwords", true);
                        enemy.anim.SetBool("animationLoop", true);

                        timeInSwordMissiles = Random.Range(1f, 2.5f);

                        enemy.GetComponent<BoxCollider2D>().enabled = false;                        

                        lastTimeSwordMissiles = Time.time;

                        idleSwords.GetComponentInChildren<SpriteRenderer>().material.DOFade(0, 0.3f);
                    }
                    else{
                        doingAttack = true;
                        doingSpawnNear = true;
                        enemy.firePoint.localPosition = new Vector3(0, -2.56f, 0);

                        enemy.anim.SetBool("idle", false);
                        enemy.anim.SetBool("fire", true);
                        enemy.anim.SetBool("spawnNear", true);
                        enemy.anim.SetBool("animationLoop", true);
                        lastTimeSpawnNear = Time.time;

                        enemy.GetComponent<BoxCollider2D>().enabled = false;

                        timeInSpawnNear = Random.Range(1f, 4f);
                        idleSwords.GetComponentInChildren<SpriteRenderer>().material.DOFade(0, 0.3f);
                    }

                }

                if (doingSwordMissiles && Time.time - lastTimeSwordMissiles >= timeInSwordMissiles && enemy.anim.GetBool("animationLoop") && !enemy.GetComponent<BoxCollider2D>().enabled)
                {
                    enemy.transform.position = enemy.posibleSpawnPoints[0];
                    enemy.GetComponent<BoxCollider2D>().enabled = true;
                    enemy.anim.SetBool("animationLoop", false);
                }

                if (doingSpawnNear && enemy.agent.enabled)
                {
                    enemy.agent.destination = enemy.player.transform.position + new Vector3(0, 2, 0);
                }

                if (doingSpawnNear && Time.time - lastTimeSpawnNear >= timeInSpawnNear && enemy.anim.GetBool("animationLoop"))
                {
                    //Debug.Log("aaaaaaa");
                    enemy.transform.position = enemy.player.transform.position + new Vector3(0, 2, 0);
                    enemy.anim.SetBool("animationLoop", false);
                    enemy.GetComponent<BoxCollider2D>().enabled = true;
                }

                if (doingSpawnNear && !enemy.anim.GetBool("animationLoop"))
                {
                    if (((angle < 90 && angle > -90) && enemy.transform.localScale.x < 0) || ((angle > 90 || angle < -90) && enemy.transform.localScale.x > 0))
                    {
                        enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);

                    }

                }

                break;
            case 2:
                if (!doingMultiSword && !doing4Waves && !doingSpawnNear && Time.time - enemy.lastTimeExitState >= enemy.waitBetweenAttacks)
                {
                    int random = Random.Range(1, 11);

                    if (random <= 2)
                    {
                        doingAttack = true;
                        doing4Waves = true;
                        //enemy.firePoint.localPosition = new Vector3(-0.05f, -0.46f, 0);

                        enemy.anim.SetBool("idle", false);
                        enemy.anim.SetBool("fire", true);
                        enemy.anim.SetBool("4waves", true);
                        enemy.anim.SetBool("animationLoop", true);

                        timeIn4Waves = Random.Range(1.5f, 4f);

                        enemy.GetComponent<BoxCollider2D>().enabled = false;

                        enemy.GetComponentInChildren<SpriteRenderer>().material.SetFloat("_ClipUvDown", 0.09f);

                        lastTime4Waves = Time.time;

                        idleSwords.GetComponentInChildren<SpriteRenderer>().material.DOFade(0, 0.3f);
                        //idleSwords.SetActive(false);
                    }
                    else if (random <= 5)
                    {
                        doingAttack = true;
                        doingMultiSword = true;
                        enemy.firePoint.localPosition = new Vector3(0, -2.56f, 0);

                        enemy.anim.SetBool("idle", false);
                        enemy.anim.SetBool("fire", true);
                        enemy.anim.SetBool("multiSword", true);
                        enemy.anim.SetBool("animationLoop", true);

                        lastTimeMultiSword = Time.time;
                        timeInMultiSword = Random.Range(1.5f, 3.5f);

                        enemy.GetComponent<BoxCollider2D>().enabled = false;

                        idleSwords.GetComponentInChildren<SpriteRenderer>().material.DOFade(0, 0.3f);
                        //idleSwords.SetActive(false);
                    }
                    else
                    {
                        doingAttack = true;
                        doingSpawnNear = true;
                        enemy.firePoint.localPosition = new Vector3(0, -2.56f, 0);

                        enemy.anim.SetBool("idle", false);
                        enemy.anim.SetBool("fire", true);
                        enemy.anim.SetBool("spawnNear", true);
                        enemy.anim.SetBool("animationLoop", true);
                        lastTimeSpawnNear = Time.time;

                        enemy.GetComponent<BoxCollider2D>().enabled = false;

                        timeInSpawnNear = Random.Range(1f, 4f);
                        idleSwords.GetComponentInChildren<SpriteRenderer>().material.DOFade(0, 0.3f);
                    }
                }

                if (doingMultiSword && Time.time - lastTimeMultiSword >= timeInMultiSword && enemy.anim.GetBool("animationLoop") && !enemy.GetComponent<BoxCollider2D>().enabled)
                {
                    enemy.transform.position = enemy.posibleSpawnPoints[Random.Range(0, enemy.posibleSpawnPoints.Length)];
                    enemy.GetComponent<BoxCollider2D>().enabled = true;
                    enemy.anim.SetBool("animationLoop", false);
                }

                if (doing4Waves && Time.time - lastTime4Waves >= timeIn4Waves && enemy.anim.GetBool("animationLoop"))
                {
                    enemy.transform.position = enemy.posibleSpawnPoints[Random.Range(0, enemy.posibleSpawnPoints.Length)];
                    enemy.GetComponent<BoxCollider2D>().enabled = true;
                    enemy.anim.SetBool("animationLoop", false);
                }

                if (doingSpawnNear && enemy.agent.enabled)
                {
                    enemy.agent.destination = enemy.player.transform.position + new Vector3(0,2,0);
                }

                if (doingSpawnNear && Time.time - lastTimeSpawnNear >= timeInSpawnNear && enemy.anim.GetBool("animationLoop"))
                {
                    //Debug.Log("aaaaaaa");
                    enemy.transform.position = enemy.player.transform.position + new Vector3(0, 2, 0);
                    enemy.anim.SetBool("animationLoop", false);
                    enemy.GetComponent<BoxCollider2D>().enabled = true;
                }

                if (doingSpawnNear && !enemy.anim.GetBool("animationLoop"))
                {
                    if (((angle < 90 && angle > -90) && enemy.transform.localScale.x < 0) || ((angle > 90 || angle < -90) && enemy.transform.localScale.x > 0))
                    {
                        enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);

                    }
                }

                if (doingMultiSword && multipleSwords != null && multipleSwords.GetComponent<SpawnObjectsInCircle>().spawnsDone)
                {
                    RotateScript rs;
                    if (!multipleSwords.TryGetComponent(out rs))
                    {
                        multipleSwords.AddComponent<RotateScript>();
                        multipleSwords.GetComponent<RotateScript>().velocity = 2;
                        multipleSwords.AddComponent<SwordThrower>();
                        multipleSwords.GetComponent<SwordThrower>().player = enemy.player;
                    }

                    if (multipleSwords.GetComponent<SwordThrower>().finished)
                    {
                        doingAttack = false;
                        doingMultiSword = false;
                        enemy.firePoint.localPosition = new Vector3(0, -2.56f, 0);

                        enemy.anim.SetBool("idle", true);
                        enemy.anim.SetBool("fire", false);
                        enemy.anim.SetBool("multiSword", false);
                        enemy.anim.SetBool("animationLoop", false);
                        GameObject.Destroy(multipleSwords.gameObject);

                        enemy.lastTimeExitState = Time.time;

                        idleSwords.GetComponentInChildren<SpriteRenderer>().material.DOFade(1, 0.3f);

                    }
                }
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
        waveSword1Left.GetComponent<SpawnObjectsInCircle>().id = 1;
        waveSword1Left.GetComponent<SpawnObjectsInCircle>().player = enemy.player;
        GameObject waveSword1Right = GameObject.Instantiate(enemy.flameWave, new Vector3(6.7f + 0.65f + enemy.transform.position.x, -4.756f + enemy.transform.position.y, 0), Quaternion.identity);
        waveSword1Right.GetComponent<SpawnObjectsInCircle>().id = 1;
        waveSword1Right.GetComponent<SpawnObjectsInCircle>().player = enemy.player;


    }

    public void GenerateFireWaveFar()
    {
        GameObject waveSword2Left = GameObject.Instantiate(enemy.flameWave, new Vector3(-7.789f - 0.85f + enemy.transform.position.x, -4.756f + enemy.transform.position.y, 0), Quaternion.identity);
        waveSword2Left.GetComponent<SpawnObjectsInCircle>().id = 2;
        waveSword2Left.GetComponent<SpawnObjectsInCircle>().player = enemy.player;


        GameObject waveSword2Right = GameObject.Instantiate(enemy.flameWave, new Vector3(7.789f + 0.85f + enemy.transform.position.x, -4.756f + enemy.transform.position.y, 0), Quaternion.identity);
        waveSword2Right.GetComponent<SpawnObjectsInCircle>().id = 2;
        waveSword2Right.GetComponent<SpawnObjectsInCircle>().player = enemy.player;

    }

    public void ClippingBug()
    {
        enemy.GetComponentInChildren<SpriteRenderer>().material.SetFloat("_ClipUvDown", 0);
        enemy.GetComponentInChildren<SpriteRenderer>().material.SetFloat("_ClipUvUp", 0.09f);

        enemy.GetComponent<BoxCollider2D>().offset = new Vector2(enemy.GetComponent<BoxCollider2D>().offset.x, -2.537f);

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

            enemy.GetComponentInChildren<SpriteRenderer>().material.SetFloat("_ClipUvUp", 0);

            idleSwords.GetComponentInChildren<SpriteRenderer>().material.DOFade(1, 0.3f);

        }

        if (doingSpawnNear)
        {
            doingSpawnNear = false;
            //enemy.firePoint.localPosition = new Vector3(-0.05f, -0.46f, 0);
            enemy.anim.SetBool("idle", true);
            enemy.anim.SetBool("fire", false);
            enemy.anim.SetBool("spawnNear", false);

            enemy.agent.enabled = false;

            a2Swords = null;
            idleSwords.GetComponentInChildren<SpriteRenderer>().material.DOFade(1, 0.3f);
        }

        if (doingSwordMissiles)
        {
            doingSwordMissiles = false;
            //enemy.firePoint.localPosition = new Vector3(-0.05f, -0.46f, 0);
            enemy.anim.SetBool("idle", true);
            enemy.anim.SetBool("fire", false);
            enemy.anim.SetBool("missileSwords", false);

            enemy.agent.enabled = false;

            enemy.GetComponentInChildren<SpriteRenderer>().material.SetFloat("_ClipUvUp", 0);
            a1Swords = null;
            idleSwords.GetComponentInChildren<SpriteRenderer>().material.DOFade(1, 0.3f);
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

        if (doing4Waves)
        {
            enemy.GetComponent<BoxCollider2D>().offset = new Vector2(enemy.GetComponent<BoxCollider2D>().offset.x, 2.22f);
        }
    }

    public void LoopTrue()
    {

        if (doingMultiSword)
        {
            enemy.anim.SetBool("animationLoop", true);


            multipleSwords = GameObject.Instantiate(enemy.multiSwords, enemy.firePoint.position, Quaternion.identity);            
            multipleSwords.GetComponent<SpawnObjectsInCircle>().player = enemy.player;
            //swords.AddComponent<RotateScript>();
            //swords.GetComponent<RotateScript>().velocity = 2;

        }
        
    }

    public void LoopFalse()
    {
        enemy.anim.SetBool("animationLoop", false);
    }

    public void ChangeYAppear()
    {
        if (doing4Waves)
        {
            ChangeY();
            GameObject.Instantiate(enemy.sword4waves, enemy.GetComponentInChildren<SpriteRenderer>().transform);
        }
    }

    public void StartAttack2()
    {
        a2Swords = GameObject.Instantiate(enemy.attack2Swords, enemy.GetComponentInChildren<SpriteRenderer>().transform);
        enemy.agent.enabled = true;
    }

    public void StartAttackMissiles()
    {
        if (!doingSwordMissiles)
            return;

        enemy.GetComponentInChildren<SpriteRenderer>().material.SetFloat("_ClipUvUp", 0.09f);
        a1Swords = GameObject.Instantiate(enemy.missileSwords, enemy.GetComponentInChildren<SpriteRenderer>().transform);
        enemy.agent.enabled = true;        
    }

    public void MissilesMove()
    {
        if (!doingSwordMissiles)
            return;

        if(a1Swords != null && a1Swords.activeInHierarchy)
        {
            a1Swords.AddComponent<SwordMissileManager>();
            a1Swords.GetComponent<SwordMissileManager>().player = enemy.player;
        }
    }
}
