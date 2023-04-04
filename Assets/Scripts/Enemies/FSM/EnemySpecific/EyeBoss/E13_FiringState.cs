using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class E13_FiringState : FiringState
{
    Enemy13 enemy;

    float lastTimeEnterBigFatman = 0;
    const float timeInBF = 8;
    bool doingBF = false;


    float lastTimeEnterBulletSpread = 0;
    const float timeInSpread = 3.5f;
    bool doingSpread = false;


    float lastTimeEnterLaserSpinNormal = 0;
    const float timeInLaserSpin = 5;
    bool doingLaserSpin = false;


    float lastTimenterQuadruleLaserSpin = 0;
    const float timeInQLS = 4.5f;
    bool doingQLS = false;

    float lastTimenterEyesBall = 0;
    const float timeEyesBall = 1.2f;
    bool doingEyesBall = false;

    float lastTimenterEyesFormsAndSingleLaserSpin = 0;
    const float timeyesFormsAndSingleLaserSpin = 15f;
    bool doingyesFormsAndSingleLaserSpin = false;

    float lastTimenterShieldSpin = 0;
    const float timeShieldSpin = 8f;
    public bool doingShieldSpin = false;
    

    private bool startUp = false;
    private bool startDown = false;

    private bool nextShootReady = false;

    private float defDistanceRay = 100;

    public LineRenderer[] lineRenderers;
    
    const float widthX = 5f;//0.104f
    bool canApplyDamge;
    float lastTimeLaserHit = 0;

    private float defaultRotation;
    GameObject FPC;
    SpriteRenderer sr;

    GameObject bullet;
    public bool doingAttack = false;

    int eyeThorwStartDirection;
    Vector2 actualDestination;
    Vector3 error = new Vector3(0.5f, 0.5f, 1);
    public bool returningRest = false;

    float lastTimenterEyeSpawn = 0;
    float timeEyeSpawn = 8f;
    public bool doingEyeSpawn = false;
    int eyesToSpawn;
    bool[] eyesSpawned = new bool[5];

    GameObject spinFxInstance;

    bool eyeThrowUp = false;

    public E13_FiringState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_FiringState stateData, Enemy13 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();


        lineRenderers = enemy.GetComponentsInChildren<LineRenderer>();
        
        //m_lineRenderer.widthMultiplier = widthX;
        sr = enemy.GetComponentInChildren<SpriteRenderer>();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.agent.enabled = false;

        if (enemy.spriteMask.enabled)
        {
            enemy.spriteMask.enabled = false;
            enemy.shield.SetActive(false);
        }

        if (doingQLS || doingLaserSpin || doingyesFormsAndSingleLaserSpin)
        {
            foreach (LineRenderer lr in lineRenderers)
            {
                lr.enabled = false;
            }
            canApplyDamge = false;

            RotateScript rs;
            if (enemy.firePoint.TryGetComponent(out rs))
            {                
                GameObject.Destroy(rs); // Destruye el componente RotateScript
            }
        }
        if(bullet != null && bullet.activeSelf)
        {
            GameObject.Destroy(bullet);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Debug.Log(enemy.anim.GetBool("animiationLoop"));

        if(!doingQLS && !doingyesFormsAndSingleLaserSpin && !doingLaserSpin)
        {
            enemy.GetComponentInChildren<ChangeTexture>().SetTexture(20);
        }

        switch(enemy.mode)
        {
            case 1:
                //Control when activate attack
                //sr.material.SetColor("", enemy.colorMode1);
                enemy.agent.enabled = false;

                if (enemy.spriteMask.enabled)
                {
                    enemy.spriteMask.enabled = false;
                    enemy.shield.SetActive(false);
                }

                if (!doingEyeSpawn && !doingBF && !doingLaserSpin && Time.time - enemy.lastTimeExitState >= enemy.waitBetweenAttacks)
                {
                    int random = Random.Range(1, 11);

                    if (random <= 3)
                    {
                        doingAttack = true;

                        enemy.firePoint.localPosition = new Vector3(-0.05f, -0.46f, 0);
                        enemy.flip = false;
                        doingLaserSpin = true;
                        enemy.anim.SetBool("idle", false);
                        enemy.anim.SetBool("fire", true);
                        enemy.anim.SetBool("laserSpin", true);
                        lastTimeEnterLaserSpinNormal = Time.time;
                    }
                    else if (random <= 7)
                    {
                        doingAttack = true;

                        enemy.firePoint.localPosition = new Vector3(-0.15f, 0.79f, 0);
                        doingBF = true;
                        enemy.anim.SetBool("idle", false);
                        enemy.anim.SetBool("fire", true);
                        enemy.anim.SetBool("animiationLoop", true);
                        enemy.anim.SetBool("bigFatman", true);
                        lastTimeEnterBigFatman = Time.time;
                    }
                    else
                    {
                        doingAttack = true;

                        eyesToSpawn = Random.Range(3, 6);
                        timeEyeSpawn = eyesToSpawn + 0.5f;
                        for (int i = 0; i < eyesSpawned.Count(); i++)
                        {
                            eyesSpawned[i] = false;
                        }
                        enemy.firePoint.localPosition = new Vector3(-0.15f, 0.79f, 0);
                        doingEyeSpawn = true;
                        enemy.anim.SetBool("idle", false);
                        enemy.anim.SetBool("fire", true);
                        enemy.anim.SetBool("animiationLoop", true);
                        enemy.anim.SetBool("bigFatman", true);
                        lastTimenterEyeSpawn = Time.time;
                    }
                }

                //Control when deactivate attack
                if (doingEyeSpawn)
                {
                    int eyeToSpawn = (int)(Time.time - lastTimenterEyeSpawn)-1;
                    if(eyeToSpawn >= 0)
                    {
                        //Debug.Log(eyeToSpawn);
                        if (!eyesSpawned[eyeToSpawn])
                        {
                            GameObject eyeMonsterr = GameObject.Instantiate(enemy.eyeMonsterPrefab, enemy.transform.position + (Vector3)Random.insideUnitCircle.normalized * Random.Range(5f, 8f), Quaternion.identity);
                            eyeMonsterr.GetComponent<EnemySpawn>().spawnEnemyAtStart = true;
                            eyesSpawned[eyeToSpawn] = true;
                        }
                       
                    }

                    if(Time.time - lastTimenterEyeSpawn >= timeEyeSpawn)
                    {
                        doingEyeSpawn = false;
                        enemy.anim.SetBool("idle", true);
                        enemy.anim.SetBool("fire", false);
                        enemy.anim.SetBool("animiationLoop", false);
                        enemy.anim.SetBool("bigFatman", false);
                        enemy.lastTimeExitState = Time.time;
                        doingAttack = false;
                        for (int i = 0; i < eyesSpawned.Count(); i++)
                        {
                            eyesSpawned[i] = false;
                        }
                    }
                }

                if (doingBF && enemy.anim.GetBool("animiationLoop") && Time.time - lastTimeEnterBigFatman >= timeInBF)
                {
                    enemy.anim.SetBool("animiationLoop", false);                    
                }

                //Update

                if (doingBF)
                {
                    nextShootReady = Time.time - lastShootTime >= 0.06f;
                    if(nextShootReady)
                        Machinegun();
                }

                if (doingLaserSpin && canApplyDamge)
                {                    
                    ShootLaser();
                }

                

                if(!doingLaserSpin)
                    enemy.firePoint.localRotation = Quaternion.Euler(0, 0, angleFirePoint * Mathf.Sign(enemy.transform.localScale.x));

                //if (m_lineRenderer.enabled)
                //{
                //    Quaternion aux = enemy.firePoint.rotation;
                //    enemy.firePoint.rotation = Quaternion.Euler(0, 0, Time.deltaTime * 100f + aux.eulerAngles.z);
                //}

                break;
            case 2:

                enemy.agent.enabled = false;

                if (enemy.spriteMask.enabled)
                {
                    enemy.spriteMask.enabled = false;
                    enemy.shield.SetActive(false);
                }

                if (!doingSpread && !doingQLS &&!doingEyesBall && Time.time - enemy.lastTimeExitState >= enemy.waitBetweenAttacks)
                {
                    int aux = Random.Range(1, 11);
                    if (aux <= 3)
                    {
                        enemy.firePoint.localPosition = new Vector3(0.1f, -0.5f, 0);
                        enemy.flip = false;
                        doingQLS = true;
                        enemy.anim.SetBool("idle", false);
                        enemy.anim.SetBool("fire", true);
                        enemy.anim.SetBool("animiationLoop", true);
                        enemy.anim.SetBool("laserQls", true);
                        lastTimenterQuadruleLaserSpin = Time.time;
                        doingAttack = true;

                    }
                    else if(aux <= 7)
                    {
                        enemy.firePoint.localPosition = new Vector3(-0.15f, 0.79f, 0);
                        doingSpread = true;
                        enemy.anim.SetBool("idle", false);
                        enemy.anim.SetBool("fire", true);
                        enemy.anim.SetBool("animiationLoop", true);
                        enemy.anim.SetBool("bigFatman", true);
                        lastTimeEnterBulletSpread = Time.time;
                        doingAttack = true;

                    }
                    else
                    {
                        enemy.firePoint.localPosition = new Vector3(-0.15f, 0.79f, 0);
                        doingEyesBall = true;
                        enemy.anim.SetBool("idle", false);
                        enemy.anim.SetBool("fire", true);
                        enemy.anim.SetBool("animiationLoop", true);
                        enemy.anim.SetBool("bigFatman", true);
                        lastTimenterEyesBall = Time.time;            
                        doingAttack = true;

                    }
                }

                //Control when deactivate attack


                if (doingSpread && enemy.anim.GetBool("animiationLoop") && Time.time - lastTimeEnterBulletSpread >= timeInSpread)
                {
                    enemy.anim.SetBool("animiationLoop", false);
                }

                if(doingEyesBall && enemy.anim.GetBool("animiationLoop") && Time.time - lastTimenterEyesBall >= timeEyesBall)
                {
                    enemy.anim.SetBool("animiationLoop", false);
                }

                //Update

                if (doingSpread)
                {
                    nextShootReady = Time.time - lastShootTime >= 0.01f;
                    if (nextShootReady)
                        Spreadgun();
                }

                if(doingQLS && Time.time - lastTimenterQuadruleLaserSpin >= timeInQLS)
                {
                    enemy.anim.SetBool("animiationLoop", false);
                }

                if (doingQLS && canApplyDamge)
                {
                    Shoot4Lasers();
                }

                if (!doingQLS)
                    enemy.firePoint.localRotation = Quaternion.Euler(0, 0, angleFirePoint * Mathf.Sign(enemy.transform.localScale.x));

                break;
            case 3:

                if (!doingyesFormsAndSingleLaserSpin && !doingShieldSpin && Time.time - enemy.lastTimeExitState >= enemy.waitBetweenAttacks)
                {                    
                    if (Time.time - lastTimenterEyesFormsAndSingleLaserSpin >= (Time.time - lastTimenterShieldSpin)/*false*/)
                    {
                        enemy.firePoint.gameObject.AddComponent<RotateScript>();
                        enemy.firePoint.gameObject.GetComponent<RotateScript>().velocity = 0.5f;
                        enemy.firePoint.localPosition = new Vector3(-0.15f, 0.79f, 0);
                        enemy.flip = false;
                        doingyesFormsAndSingleLaserSpin = true;
                        enemy.anim.SetBool("idle", false);
                        enemy.anim.SetBool("fire", true);
                        enemy.anim.SetBool("animiationLoop", false);
                        enemy.anim.SetBool("laserSpinEyeForms", true);
                        lastTimenterEyesFormsAndSingleLaserSpin = Time.time;
                        doingAttack = true;
                        FunctionTimer.Create(() =>
                        {
                            if (enemy != null)
                            {
                                enemy.anim.SetBool("animiationLoop", true);
                            }

                        }, 1);

                    }
                    else //shield
                    {
                        enemy.agent.acceleration = 500;
                        enemy.flip = true;
                        doingShieldSpin = true;
                        enemy.anim.SetBool("idle", false);
                        enemy.anim.SetBool("fire", true);
                        enemy.anim.SetBool("animiationLoop", true);
                        enemy.anim.SetBool("shieldSpin", true);
                        enemy.sombra.transform.DOScale(1.5f, 0.7f);
                        enemy.sombra.transform.DOLocalMoveY(-1.02f, 0.7f);
                        enemy.transform.DOLocalMoveY(enemy.transform.position.y - 1, 0.7f);
                        actualDestination = enemy.pathScript.GetNextPoint(enemy);
                        enemy.sparks.SetActive(true);
                        FunctionTimer.Create(() =>
                        {
                            if(enemy != null && enemy.shield != null && enemy.sparks != null)
                            {
                                enemy.agent.enabled = true;
                                enemy.agent.destination = actualDestination;
                                enemy.spriteMask.enabled = true;
                                enemy.shield.SetActive(true);
                                enemy.spriteMask.sprite = sr.sprite;
                                enemy.sparks.SetActive(false);
                                if(spinFxInstance == null || !spinFxInstance.activeInHierarchy)
                                {
                                    spinFxInstance = GameObject.Instantiate(enemy.spinFx, sr.transform);
                                }
                            }
                        }, 0.8f);
                        lastTimenterShieldSpin = Time.time;
                        doingAttack = true;

                    }

                }
                if (!doingShieldSpin)                
                {
                    if (enemy.spriteMask.enabled)
                    {
                        enemy.spriteMask.enabled = false;
                        enemy.shield.SetActive(false);
                    }
                }

                if (doingShieldSpin && !returningRest)
                {
                    Vector3 aux;
                    aux.x = Mathf.Abs(enemy.transform.position.x - actualDestination.x);
                    aux.y = Mathf.Abs(enemy.transform.position.y - actualDestination.y);
                    aux.z = 0;
                    if (aux.x <= error.x && aux.y <= error.y)
                    {
                        actualDestination = enemy.pathScript.GetNextPoint(enemy);
                        enemy.agent.destination = actualDestination;

                    }
                }

                if (doingShieldSpin && Time.time - lastTimenterShieldSpin >= timeShieldSpin)
                {
                    returningRest = true;
                    enemy.agent.acceleration = 10000;
                    enemy.agent.destination = enemy.restingPoint;
                    actualDestination = enemy.restingPoint;
                }

                if(returningRest)
                {
                    Vector3 aux;
                    aux.x = Mathf.Abs(enemy.transform.position.x - enemy.restingPoint.x);
                    aux.y = Mathf.Abs(enemy.transform.position.y - enemy.restingPoint.y);
                    aux.z = 0;
                    if (aux.x <= error.x && aux.y <= error.y)
                    {
                        enemy.anim.SetBool("idle", true);
                        enemy.anim.SetBool("fire", false);
                        enemy.anim.SetBool("animiationLoop", false);
                        enemy.anim.SetBool("shieldSpin", false);
                        doingShieldSpin = false;
                        returningRest = false;
                        enemy.agent.enabled = false;
                        enemy.lastTimeExitState = Time.time;
                        enemy.transform.DOMove(enemy.restingPoint - new Vector3(0,1,0), 0.1f);
                        doingAttack = false;
                        GameObject.Destroy(spinFxInstance);
                        
                        FunctionTimer.Create(() =>
                        {
                            if(enemy != null && enemy.sombra != null && !enemy.GetIfIsDead())
                            {
                                enemy.sombra.transform.DOScale(1, 0.7f);
                                enemy.sombra.transform.DOLocalMoveY(-2.73f, 0.7f);
                                enemy.transform.DOLocalMoveY(enemy.restingPoint.y, 0.7f);
                            }
                        }, 0.1f);                        
                       

                    }

                }

                //if (doingyesFormsAndSingleLaserSpin && Time.time - lastTimenterEyesFormsAndSingleLaserSpin >= 7)
                //{
                //    RotateScript rs;
                //    if(enemy.firePoint.TryGetComponent<RotateScript>(out rs))
                //    {
                //        if(rs.velocity > 0)
                //            rs.velocity *= -1;
                //    }
                //}

                if (doingyesFormsAndSingleLaserSpin)
                {
                    nextShootReady = Time.time - lastShootTime >= 0.11f;
                    if (nextShootReady)
                        SpinGun();
                }

                if (doingyesFormsAndSingleLaserSpin && canApplyDamge)
                {
                    ShootLaser();
                }

                if (doingyesFormsAndSingleLaserSpin && enemy.anim.GetBool("animiationLoop") && Time.time - lastTimenterEyesFormsAndSingleLaserSpin >= timeyesFormsAndSingleLaserSpin)
                {
                    enemy.anim.SetBool("animiationLoop", false);
                    doingyesFormsAndSingleLaserSpin = false;
                    enemy.anim.SetBool("idle", true);
                    enemy.anim.SetBool("fire", false);
                    enemy.anim.SetBool("laserSpinEyeForms", false);
                    enemy.lastTimeExitState = Time.time;
                    //lineRenderers[0].enabled = false;
                    enemy.flip = true;
                    doingAttack = false;
                    RotateScript rs;
                    if (enemy.firePoint.TryGetComponent<RotateScript>(out rs))
                    {
                        GameObject.Destroy(rs);
                    }
                    EndLaser();
                }

                break;
            default:
                break;
        }

        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!doingShieldSpin)  
        {
            enemy.agent.enabled = false;
        }
        else
        {
            if (enemy.shield.activeInHierarchy && enemy.agent.enabled)
            {
                enemy.shield.GetComponent<SpriteRenderer>().material.SetFloat("_TextureScrollXSpeed", enemy.agent.velocity.normalized.x * 5);
                enemy.shield.GetComponent<SpriteRenderer>().material.SetFloat("_TextureScrollYSpeed", enemy.agent.velocity.normalized.y * 5);
            }
        }


    }

    public void FlipLeft()
    {
        Vector3 aux = enemy.transform.localScale;
        aux.x = -1 * Mathf.Abs(aux.x);
        enemy.transform.localScale = aux;

    }
    public void FlipRight()
    {
        
        Vector3 aux = enemy.transform.localScale;
        aux.x = Mathf.Abs(aux.x);
        enemy.transform.localScale = aux;
        

    }

    public void StartUp()
    {
        startUp = true;
        enemy.firePoint.localRotation = Quaternion.Euler(0, 0, 90);
        //myVr.GetComponent<RotateScript>().counter += 180;

        defaultRotation = 90;
    }
    public void StartDown()
    {
        startDown = true;
        enemy.firePoint.localRotation = Quaternion.Euler(0, 0, -90);
        //myVr.GetComponent<RotateScript>().counter += -180;

        defaultRotation = -90;

    }
    
    public void EndUp()
    {
        if (startUp)
        {
            startUp = false;
            enemy.flip = true;
            enemy.anim.SetBool("idle", true);
            enemy.anim.SetBool("fire", false);
            doingLaserSpin = false;
            enemy.anim.SetBool("laserSpin", false);
            enemy.lastTimeExitState = Time.time;
            doingAttack = false;

        }
    }
    public void EndDown()
    {
        if (startDown)
        {
            startDown = false;
            enemy.flip = true;
            enemy.anim.SetBool("idle", true);
            enemy.anim.SetBool("fire", false);
            //doingLaserSpin = false;
            enemy.anim.SetBool("laserSpin", false);
            doingQLS = false;
            enemy.anim.SetBool("laserQls", false);
            enemy.lastTimeExitState = Time.time;
            doingLaserSpin = false;
            doingAttack = false;
        }
    }
    public void EndEyesThrow()
    {
        if (doingBF)
        {
            doingBF = false;
            enemy.anim.SetBool("idle", true);
            enemy.anim.SetBool("fire", false);
            enemy.anim.SetBool("bigFatman", false);
            enemy.lastTimeExitState = Time.time;
            doingAttack = false;

            //Debug.Log("si");
        }
        else if (doingSpread)
        {
            doingSpread = false;
            enemy.anim.SetBool("idle", true);
            enemy.anim.SetBool("fire", false);
            enemy.anim.SetBool("bigFatman", false);
            enemy.lastTimeExitState = Time.time;
            doingAttack = false;

            //Debug.Log("si");
        }
        else if (doingEyesBall)
        {
            doingEyesBall = false;
            enemy.anim.SetBool("idle", true);
            enemy.anim.SetBool("fire", false);
            enemy.anim.SetBool("bigFatman", false);
            enemy.lastTimeExitState = Time.time;
            doingAttack = false;

        }

    }
    void Machinegun()
    {
        GameObject bullet = GameObject.Instantiate(stateData.bulletType, enemy.GetComponent<Entity>().GetFirePointTransform().position, enemy.GetComponent<Entity>().GetFirePointTransform().rotation);
        if (eyeThrowUp)
        {
            bullet.GetComponentInChildren<SpriteRenderer>().sortingOrder = -1;
        }
        //fireSoundKey = AudioManager.Instance.LoadSound(stateData.shootShound, enemy.GetComponent<Entity>().GetFirePointTransform().position);       
        bullet.transform.Rotate(0, 0, bullet.transform.rotation.z + UnityEngine.Random.Range(-25, 25));
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * bullet.GetComponent<EnemyProjectile>().bulletData.speed, ForceMode2D.Impulse);
        lastShootTime = Time.time;        
    }
    void Spreadgun()
    {
        GameObject bullet = GameObject.Instantiate(stateData.bulletType, enemy.GetComponent<Entity>().GetFirePointTransform().position, Quaternion.Euler(0,0,Random.Range(-180f,180f)));
        if (eyeThrowUp)
        {
            bullet.GetComponentInChildren<SpriteRenderer>().sortingOrder = -1;
        }
        //fireSoundKey = AudioManager.Instance.LoadSound(stateData.shootShound, enemy.GetComponent<Entity>().GetFirePointTransform().position);       
        //bullet.transform.Rotate(0, 0, bullet.transform.rotation.z + UnityEngine.Random.Range(-25, 25));
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * bullet.GetComponent<EnemyProjectile>().bulletData.speed, ForceMode2D.Impulse);
        lastShootTime = Time.time;
    }
    void SpinGun()
    {
        GameObject bullet = GameObject.Instantiate(stateData.bulletType, enemy.GetComponent<Entity>().GetFirePointTransform().position + enemy.GetComponent<Entity>().GetFirePointTransform().right, Quaternion.identity/*enemy.GetComponent<Entity>().GetFirePointTransform().rotation*/);
        if (eyeThrowUp)
        {
            bullet.GetComponentInChildren<SpriteRenderer>().sortingOrder = -1;
        }
        bullet.GetComponent<Rigidbody2D>().AddForce(enemy.GetComponent<Entity>().GetFirePointTransform().right * bullet.GetComponent<EnemyProjectile>().bulletData.speed * 0.5f, ForceMode2D.Impulse);

        GameObject bullet2 = GameObject.Instantiate(stateData.bulletType, enemy.GetComponent<Entity>().GetFirePointTransform().position - enemy.GetComponent<Entity>().GetFirePointTransform().right, Quaternion.identity/*enemy.GetComponent<Entity>().GetFirePointTransform().rotation*/);
        if (eyeThrowUp)
        {
            bullet2.GetComponentInChildren<SpriteRenderer>().sortingOrder = -1;
        }
        bullet2.GetComponent<Rigidbody2D>().AddForce(-enemy.GetComponent<Entity>().GetFirePointTransform().right * bullet2.GetComponent<EnemyProjectile>().bulletData.speed * 0.5f, ForceMode2D.Impulse);

        GameObject bullet3 = GameObject.Instantiate(stateData.bulletType, enemy.GetComponent<Entity>().GetFirePointTransform().position + enemy.GetComponent<Entity>().GetFirePointTransform().up, Quaternion.identity/*enemy.GetComponent<Entity>().GetFirePointTransform().rotation*/);
        if (eyeThrowUp)
        {
            bullet3.GetComponentInChildren<SpriteRenderer>().sortingOrder = -1;
        }
        bullet3.GetComponent<Rigidbody2D>().AddForce(enemy.GetComponent<Entity>().GetFirePointTransform().up * bullet3.GetComponent<EnemyProjectile>().bulletData.speed * 0.5f, ForceMode2D.Impulse);

        GameObject bullet4 = GameObject.Instantiate(stateData.bulletType, enemy.GetComponent<Entity>().GetFirePointTransform().position - enemy.GetComponent<Entity>().GetFirePointTransform().up, Quaternion.identity/*enemy.GetComponent<Entity>().GetFirePointTransform().rotation*/);
        if (eyeThrowUp)
        {
            bullet4.GetComponentInChildren<SpriteRenderer>().sortingOrder = -1;
        }
        bullet4.GetComponent<Rigidbody2D>().AddForce(-enemy.GetComponent<Entity>().GetFirePointTransform().up * bullet4.GetComponent<EnemyProjectile>().bulletData.speed * 0.5f, ForceMode2D.Impulse);

        lastShootTime = Time.time;
    }
    public void StartLaser()
    {        
        lineRenderers[0].enabled = true;
        lineRenderers[0].material.DOFloat(0, "_ClipUvUp", 0.15f);
        lineRenderers[0].material.DOFloat(0, "_ClipUvDown", 0.15f);
        if (doingQLS)
        {
            lineRenderers[1].enabled = true;
            lineRenderers[1].material.DOFloat(0, "_ClipUvUp", 0.15f);
            lineRenderers[1].material.DOFloat(0, "_ClipUvDown", 0.15f);

            lineRenderers[2].enabled = true;
            lineRenderers[2].material.DOFloat(0, "_ClipUvUp", 0.15f);
            lineRenderers[2].material.DOFloat(0, "_ClipUvDown", 0.15f);

            lineRenderers[3].enabled = true;
            lineRenderers[3].material.DOFloat(0, "_ClipUvUp", 0.15f);
            lineRenderers[3].material.DOFloat(0, "_ClipUvDown", 0.15f);

            enemy.firePoint.gameObject.AddComponent<RotateScript>();
            enemy.firePoint.GetComponent<RotateScript>().velocity = 0.5f;
        }
        canApplyDamge = true;   
    }

    public void EndLaser()
    {
        foreach (LineRenderer lr in lineRenderers)
        {            
            lr.material.DOFloat(0.5f, "_ClipUvDown", 0.35f);
            lr.material.DOFloat(0.5f, "_ClipUvUp", 0.35f);
        }

        FunctionTimer.Create(() =>
        {
            if(enemy != null && lineRenderers[0] != null && lineRenderers[1] != null && lineRenderers[2] != null && lineRenderers[3] != null && enemy.firePoint != null)
            {

                foreach(LineRenderer lr in lineRenderers)
                {
                    lr.enabled = false;
                    lr.material.DOFloat(0.5f, "_ClipUvDown", 0.35f);
                    lr.material.DOFloat(0.5f, "_ClipUvUp", 0.35f);
                }
                canApplyDamge = false;

                GameObject.Destroy(enemy.firePoint.GetComponent<RotateScript>());
            }
           
        }, 0.35f);
        
    }

    void ShootLaser()
    {
        if (Physics2D.Raycast(lineRenderers[0].transform.position, -lineRenderers[0].transform.up, defDistanceRay, enemy.laserAffectsLayer))
        {
            RaycastHit2D _hit = Physics2D.Raycast(lineRenderers[0].transform.position, -lineRenderers[0].transform.up, defDistanceRay, enemy.laserAffectsLayer);            

            if (_hit.transform.gameObject.CompareTag("Player"))
            {
                if (canApplyDamge && Time.time - lastTimeLaserHit >= 0.3f)
                {
                    _hit.rigidbody.gameObject.GetComponent<PlayerMovement>().GetDamage(1);                    
                    lastTimeLaserHit = Time.time;
                }
                
            }
            else
            {
                
            }
            Draw2DRay(lineRenderers[0].transform.position, _hit.point, 0);
        }
        else
        {
            Draw2DRay(lineRenderers[0].transform.position, -lineRenderers[0].transform.up * defDistanceRay, 0);
        }
    }

    void Shoot4Lasers()
    {
        //Laser 1

        if (Physics2D.Raycast(enemy.firePoint.transform.position, -enemy.firePoint.transform.up, defDistanceRay, enemy.laserAffectsLayer))
        {
            RaycastHit2D _hit = Physics2D.Raycast(enemy.firePoint.transform.position, -enemy.firePoint.transform.up, defDistanceRay, enemy.laserAffectsLayer);

            if (_hit.transform.gameObject.CompareTag("Player"))
            {
                if (canApplyDamge && Time.time - lastTimeLaserHit >= 0.3f)
                {
                    _hit.rigidbody.gameObject.GetComponent<PlayerMovement>().GetDamage(1);
                    lastTimeLaserHit = Time.time;
                }

            }
            else
            {

            }
            Draw2DRay(enemy.firePoint.transform.position, _hit.point, 0);
        }
        else
        {
            Draw2DRay(enemy.firePoint.transform.position, -enemy.firePoint.transform.up * defDistanceRay, 0);
        }

        //Laser 2

        if (Physics2D.Raycast(enemy.firePoint.transform.position, enemy.firePoint.transform.up, defDistanceRay, enemy.laserAffectsLayer))
        {
            RaycastHit2D _hit = Physics2D.Raycast(enemy.firePoint.transform.position, enemy.firePoint.transform.up, defDistanceRay, enemy.laserAffectsLayer);

            if (_hit.transform.gameObject.CompareTag("Player"))
            {
                if (canApplyDamge && Time.time - lastTimeLaserHit >= 0.3f)
                {
                    _hit.rigidbody.gameObject.GetComponent<PlayerMovement>().GetDamage(1);
                    lastTimeLaserHit = Time.time;
                }

            }
            else
            {

            }
            Draw2DRay(enemy.firePoint.transform.position, _hit.point, 1);
        }
        else
        {
            Draw2DRay(enemy.firePoint.transform.position, enemy.firePoint.transform.up * defDistanceRay, 1);
        }

        //Laser 3

        if (Physics2D.Raycast(enemy.firePoint.transform.position, enemy.firePoint.transform.right, defDistanceRay, enemy.laserAffectsLayer))
        {
            RaycastHit2D _hit = Physics2D.Raycast(enemy.firePoint.transform.position, enemy.firePoint.transform.right, defDistanceRay, enemy.laserAffectsLayer);

            if (_hit.transform.gameObject.CompareTag("Player"))
            {
                if (canApplyDamge && Time.time - lastTimeLaserHit >= 0.3f)
                {
                    _hit.rigidbody.gameObject.GetComponent<PlayerMovement>().GetDamage(1);
                    lastTimeLaserHit = Time.time;
                }

            }
            else
            {

            }
            Draw2DRay(enemy.firePoint.transform.position, _hit.point, 2);
        }
        else
        {
            Draw2DRay(enemy.firePoint.transform.position, enemy.firePoint.transform.right * defDistanceRay, 2);
        }

        //Laser 4

        if (Physics2D.Raycast(enemy.firePoint.transform.position, -enemy.firePoint.transform.right, defDistanceRay, enemy.laserAffectsLayer))
        {
            RaycastHit2D _hit = Physics2D.Raycast(enemy.firePoint.transform.position, -enemy.firePoint.transform.right, defDistanceRay, enemy.laserAffectsLayer);

            if (_hit.transform.gameObject.CompareTag("Player"))
            {
                if (canApplyDamge && Time.time - lastTimeLaserHit >= 0.3f)
                {
                    _hit.rigidbody.gameObject.GetComponent<PlayerMovement>().GetDamage(1);
                    lastTimeLaserHit = Time.time;
                }

            }
            else
            {

            }
            Draw2DRay(enemy.firePoint.transform.position, _hit.point, 3);
        }
        else
        {
            Draw2DRay(enemy.firePoint.transform.position, -enemy.firePoint.transform.right * defDistanceRay, 3);
        }

    }

    void Draw2DRay(Vector2 startPoint, Vector2 endPoint, int arrayPos)
    {
        lineRenderers[arrayPos].SetPosition(0, startPoint);
        lineRenderers[arrayPos].SetPosition(1, endPoint);

    }
    public void LaserChargeParticles()
    {
        GameObject.Instantiate(enemy.laserChargeParticles, enemy.transform.position + new Vector3(0.1f,0,0), Quaternion.identity); //0.05 -0.5
        //Debug.Log("siiiiiiiiiii");
    }

    public void LaserDebajo()
    {
        lineRenderers[0].sortingOrder = -1;
    }

    public void EyeBallIfNeededUp()
    {
        if (!doingEyesBall)
        {
            return;
        }

        bullet = GameObject.Instantiate(enemy.eyesBall, enemy.firePoint.position, enemy.firePoint.rotation);
        bullet.transform.localScale = new Vector3(0.05f, 0.1f, 1);
        bullet.transform.DOScale(1, 1);

        SpriteRenderer[] eyesSprites = bullet.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer eyeSprite in eyesSprites)
        {
            if (eyeSprite.sortingOrder == -1)
            {
                eyeSprite.sortingOrder = -2;
            }
            else
            {
                eyeSprite.sortingOrder = -1;
            }
        }
        FunctionTimer.Create(() =>
        {
            if (enemy != null && enemy.firePoint != null && !enemy.GetIfIsDead() && stateMachine.currentState != enemy.deadState)
            {
                bullet.GetComponent<Rigidbody2D>().AddForce(enemy.firePoint.right * bullet.GetComponentInChildren<EnemyProjectile>().bulletData.speed * 2, ForceMode2D.Impulse);
                bullet.GetComponent<RotateScript>().velocity = 3;
            }
        }, 1);      
    }

    public void EyeBallIfNeededDown()
    {
        if (!doingEyesBall)
        {
            return;
        }

        bullet = GameObject.Instantiate(enemy.eyesBall, enemy.firePoint.position, enemy.firePoint.rotation);
        bullet.transform.localScale = new Vector3(0.1f, 0.1f, 1);
        bullet.transform.DOScale(1, 1);

        SpriteRenderer[] eyesSprites = bullet.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer eyeSprite in eyesSprites)
        {
            if (eyeSprite.sortingOrder == -1)
            {
                eyeSprite.sortingOrder = 1;
            }
            else
            {
                eyeSprite.sortingOrder = 2;
            }
        }
        FunctionTimer.Create(() =>
        {
            if (enemy != null && enemy.firePoint != null && !enemy.GetIfIsDead() && stateMachine.currentState != enemy.deadState)
            {
                bullet.GetComponent<Rigidbody2D>().AddForce(enemy.firePoint.right * bullet.GetComponentInChildren<EnemyProjectile>().bulletData.speed * 2, ForceMode2D.Impulse);
                bullet.GetComponent<RotateScript>().velocity = 3;
            }            
        }, 1);
    }

    public void LaserArriba()
    {
        lineRenderers[0].sortingOrder = 5;
    }

    public void EyeUpAnim()
    {
        eyeThrowUp = true;
    }

    public void EyeDownAnim()
    {
        eyeThrowUp = false;
    }

}
