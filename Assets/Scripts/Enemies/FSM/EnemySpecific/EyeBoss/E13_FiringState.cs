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
    bool doingShieldSpin = false;
    

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

    int eyeThorwStartDirection;
    Vector2 actualDestination;
    Vector3 error = new Vector3(0.5f, 0.5f, 1);

    public E13_FiringState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_FiringState stateData, Enemy13 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        lineRenderers = enemy.GetComponentsInChildren<LineRenderer>();
        
        //m_lineRenderer.widthMultiplier = widthX;
        sr = enemy.GetComponent<SpriteRenderer>();

    }

    public override void Exit()
    {
        base.Exit();
        if(doingQLS || doingLaserSpin)
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

        switch(enemy.mode)
        {
            case 1:
                //Control when activate attack
                //sr.material.SetColor("", enemy.colorMode1);

                if(!doingBF && !doingLaserSpin && Time.time - enemy.lastTimeExitState >= enemy.waitBetweenAttacks)
                {
                    if (Time.time - lastTimeEnterLaserSpinNormal >= (Time.time - lastTimeEnterBigFatman) * 2)
                    {
                        enemy.firePoint.localPosition = new Vector3(-0.05f, -0.46f, 0);
                        enemy.flip = false;
                        doingLaserSpin = true;
                        enemy.anim.SetBool("idle", false);
                        enemy.anim.SetBool("fire", true);
                        enemy.anim.SetBool("laserSpin", true);
                        lastTimeEnterLaserSpinNormal = Time.time;
                    }
                    else
                    {
                        enemy.firePoint.localPosition = new Vector3(-0.15f, 0.79f, 0);
                        doingBF = true;
                        enemy.anim.SetBool("idle", false);
                        enemy.anim.SetBool("fire", true);
                        enemy.anim.SetBool("animiationLoop", true);
                        enemy.anim.SetBool("bigFatman", true);
                        lastTimeEnterBigFatman = Time.time;
                    }
                }

                //Control when deactivate attack


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
                        enemy.flip = true;
                        doingShieldSpin = true;
                        enemy.anim.SetBool("idle", false);
                        enemy.anim.SetBool("fire", true);
                        enemy.anim.SetBool("animiationLoop", true);
                        enemy.anim.SetBool("shieldSpin", true);
                        actualDestination = enemy.pathScript.GetNextPoint(enemy);
                        lastTimenterShieldSpin = Time.time;
                    }                    

                }

                if (doingShieldSpin)
                {
                    Vector3 aux;
                    aux.x = Mathf.Abs(enemy.transform.position.x - actualDestination.x);
                    aux.y = Mathf.Abs(enemy.transform.position.y - actualDestination.y);
                    aux.z = 0;
                    if (aux.x <= error.x && aux.y <= error.y)
                    {
                        actualDestination = enemy.pathScript.GetNextPoint(enemy);
                    }
                }

                if (doingShieldSpin && Time.time - lastTimenterShieldSpin >= timeShieldSpin)
                {
                    doingShieldSpin = false;
                    enemy.anim.SetBool("idle", true);
                    enemy.anim.SetBool("fire", false);
                    enemy.anim.SetBool("animiationLoop", false);
                    enemy.anim.SetBool("shieldSpin", false);
                    
                    enemy.lastTimeExitState = Time.time;                    
                }

                

                if (doingyesFormsAndSingleLaserSpin && Time.time - lastTimenterEyesFormsAndSingleLaserSpin >= 7)
                {
                    RotateScript rs;
                    if(enemy.firePoint.TryGetComponent<RotateScript>(out rs))
                    {
                        if(rs.velocity > 0)
                            rs.velocity *= -1;
                    }
                }

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
                    lineRenderers[0].enabled = false;
                    enemy.flip = true;
                    RotateScript rs;
                    if (enemy.firePoint.TryGetComponent<RotateScript>(out rs))
                    {
                        GameObject.Destroy(rs);
                    }
                }

                break;
            default:
                break;
        }

        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (doingShieldSpin)
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, actualDestination, enemy.velocity);
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
            doingLaserSpin = false;
            enemy.anim.SetBool("laserSpin", false);
            doingQLS = false;
            enemy.anim.SetBool("laserQls", false);
            enemy.lastTimeExitState = Time.time;
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
            //Debug.Log("si");
        }
        else if (doingSpread)
        {
            doingSpread = false;
            enemy.anim.SetBool("idle", true);
            enemy.anim.SetBool("fire", false);
            enemy.anim.SetBool("bigFatman", false);
            enemy.lastTimeExitState = Time.time;
            //Debug.Log("si");
        }
        else if (doingEyesBall)
        {
            doingEyesBall = false;
            enemy.anim.SetBool("idle", true);
            enemy.anim.SetBool("fire", false);
            enemy.anim.SetBool("bigFatman", false);
            enemy.lastTimeExitState = Time.time;
        }

    }
    void Machinegun()
    {
        GameObject bullet = GameObject.Instantiate(stateData.bulletType, enemy.GetComponent<Entity>().GetFirePointTransform().position, enemy.GetComponent<Entity>().GetFirePointTransform().rotation);
        //fireSoundKey = AudioManager.Instance.LoadSound(stateData.shootShound, enemy.GetComponent<Entity>().GetFirePointTransform().position);       
        bullet.transform.Rotate(0, 0, bullet.transform.rotation.z + UnityEngine.Random.Range(-25, 25));
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * bullet.GetComponent<EnemyProjectile>().bulletData.speed, ForceMode2D.Impulse);
        lastShootTime = Time.time;        
    }
    void Spreadgun()
    {
        GameObject bullet = GameObject.Instantiate(stateData.bulletType, enemy.GetComponent<Entity>().GetFirePointTransform().position, Quaternion.Euler(0,0,Random.Range(-180f,180f)));
        //fireSoundKey = AudioManager.Instance.LoadSound(stateData.shootShound, enemy.GetComponent<Entity>().GetFirePointTransform().position);       
        //bullet.transform.Rotate(0, 0, bullet.transform.rotation.z + UnityEngine.Random.Range(-25, 25));
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * bullet.GetComponent<EnemyProjectile>().bulletData.speed, ForceMode2D.Impulse);
        lastShootTime = Time.time;
    }
    void SpinGun()
    {
        GameObject bullet = GameObject.Instantiate(stateData.bulletType, enemy.GetComponent<Entity>().GetFirePointTransform().position + enemy.GetComponent<Entity>().GetFirePointTransform().right, Quaternion.identity/*enemy.GetComponent<Entity>().GetFirePointTransform().rotation*/);        
        bullet.GetComponent<Rigidbody2D>().AddForce(enemy.GetComponent<Entity>().GetFirePointTransform().right * bullet.GetComponent<EnemyProjectile>().bulletData.speed * 0.5f, ForceMode2D.Impulse);

        GameObject bullet2 = GameObject.Instantiate(stateData.bulletType, enemy.GetComponent<Entity>().GetFirePointTransform().position - enemy.GetComponent<Entity>().GetFirePointTransform().right, Quaternion.identity/*enemy.GetComponent<Entity>().GetFirePointTransform().rotation*/);
        bullet2.GetComponent<Rigidbody2D>().AddForce(-enemy.GetComponent<Entity>().GetFirePointTransform().right * bullet2.GetComponent<EnemyProjectile>().bulletData.speed * 0.5f, ForceMode2D.Impulse);

        GameObject bullet3 = GameObject.Instantiate(stateData.bulletType, enemy.GetComponent<Entity>().GetFirePointTransform().position + enemy.GetComponent<Entity>().GetFirePointTransform().up, Quaternion.identity/*enemy.GetComponent<Entity>().GetFirePointTransform().rotation*/);
        bullet3.GetComponent<Rigidbody2D>().AddForce(enemy.GetComponent<Entity>().GetFirePointTransform().up * bullet3.GetComponent<EnemyProjectile>().bulletData.speed * 0.5f, ForceMode2D.Impulse);

        GameObject bullet4 = GameObject.Instantiate(stateData.bulletType, enemy.GetComponent<Entity>().GetFirePointTransform().position - enemy.GetComponent<Entity>().GetFirePointTransform().up, Quaternion.identity/*enemy.GetComponent<Entity>().GetFirePointTransform().rotation*/);
        bullet4.GetComponent<Rigidbody2D>().AddForce(-enemy.GetComponent<Entity>().GetFirePointTransform().up * bullet4.GetComponent<EnemyProjectile>().bulletData.speed * 0.5f, ForceMode2D.Impulse);

        lastShootTime = Time.time;
    }
    public void StartLaser()
    {        
        lineRenderers[0].enabled = true;
        if (doingQLS)
        {
            lineRenderers[1].enabled = true;
            lineRenderers[2].enabled = true;
            lineRenderers[3].enabled = true;
            enemy.firePoint.gameObject.AddComponent<RotateScript>();
            enemy.firePoint.GetComponent<RotateScript>().velocity = 0.5f;
        }
        canApplyDamge = true;   
    }

    public void EndLaser()
    {
        FunctionTimer.Create(() =>
        {
            if(enemy != null && lineRenderers[0] != null && lineRenderers[1] != null && lineRenderers[2] != null && lineRenderers[3] != null && enemy.firePoint != null)
            {

                foreach(LineRenderer lr in lineRenderers)
                {
                    lr.enabled = false;
                }
                canApplyDamge = false;

                GameObject.Destroy(enemy.firePoint.GetComponent<RotateScript>());
            }
           
        }, 0.15f);
        
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
        GameObject.Instantiate(enemy.laserChargeParticles, enemy.firePoint.transform.position + new Vector3(0.1f,0.5f,0), Quaternion.identity);
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
        bullet.transform.localScale = new Vector3(0.1f, 0.1f, 1);
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
}
