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
    const float timeInQLS = 4;
    bool doingQLS = false;

    private bool startUp = false;
    private bool startDown = false;

    private bool nextShootReady = false;

    private float defDistanceRay = 100;
    LineRenderer m_lineRenderer;
    const float widthX = 5f;//0.104f
    bool canApplyDamge;
    float lastTimeLaserHit = 0;

    private float defaultRotation;
    GameObject FPC;

    public E13_FiringState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_FiringState stateData, Enemy13 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        m_lineRenderer = enemy.GetComponentInChildren<LineRenderer>();
        //m_lineRenderer.widthMultiplier = widthX;
       
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        switch(enemy.mode)
        {
            case 1:
                //Control when activate attack

                if(!doingBF && !doingLaserSpin && Time.time - enemy.lastTimeExitState >= enemy.waitBetweenAttacks)
                {
                    //if(Time.time - lastTimeEnterLaserSpinNormal >= (Time.time - lastTimeEnterBigFatman) * 2)
                    //{
                        enemy.firePoint.localPosition = new Vector3(-0.05f, -0.46f, 0);
                        enemy.flip = false;
                        doingLaserSpin = true;
                        enemy.anim.SetBool("idle", false);
                        enemy.anim.SetBool("fire", true);
                        enemy.anim.SetBool("laserSpin", true);
                        lastTimeEnterLaserSpinNormal = Time.time;
                    //}
                    //else
                    //{
                    //    enemy.firePoint.localPosition = new Vector3(-0.15f, 0.79f, 0);
                    //    doingBF = true;
                    //    enemy.anim.SetBool("idle", false);
                    //    enemy.anim.SetBool("fire", true);
                    //    enemy.anim.SetBool("animiationLoop", true);
                    //    enemy.anim.SetBool("bigFatman", true);
                    //    lastTimeEnterBigFatman = Time.time;
                    //}
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
                break;
            case 3:
                break;
            default:
                break;
        }

        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

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
            Debug.Log("si");
        }
        else if (doingSpread)
        {

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
    public void StartLaser()
    {
        
        m_lineRenderer.enabled = true;
        canApplyDamge = true;
        
        

    }

    public void EndLaser()
    {
        FunctionTimer.Create(() =>
        {
            if(enemy != null && m_lineRenderer != null)
            {
                
                m_lineRenderer.enabled = false;
                canApplyDamge = false;
                
            }
           
        }, 0.15f);
        
    }

    void ShootLaser()
    {
        if (Physics2D.Raycast(m_lineRenderer.transform.position, -m_lineRenderer.transform.up, defDistanceRay, enemy.laserAffectsLayer))
        {
            RaycastHit2D _hit = Physics2D.Raycast(m_lineRenderer.transform.position, -m_lineRenderer.transform.up, defDistanceRay, enemy.laserAffectsLayer);            

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
            Draw2DRay(m_lineRenderer.transform.position, _hit.point);
        }
        else
        {
            Draw2DRay(m_lineRenderer.transform.position, -m_lineRenderer.transform.up * defDistanceRay);
        }
    }

    void Draw2DRay(Vector2 startPoint, Vector2 endPoint)
    {
        m_lineRenderer.SetPosition(0, startPoint);
        m_lineRenderer.SetPosition(1, endPoint);

    }
    public void LaserChargeParticles()
    {
        GameObject.Instantiate(enemy.laserChargeParticles, enemy.firePoint.transform.position + new Vector3(0.1f,0.5f,0), Quaternion.identity);
        //Debug.Log("siiiiiiiiiii");
    }

    public void LaserDebajo()
    {
        m_lineRenderer.sortingOrder = -1;
    }

    public void LaserArriba()
    {
        m_lineRenderer.sortingOrder = 5;
    }
}
