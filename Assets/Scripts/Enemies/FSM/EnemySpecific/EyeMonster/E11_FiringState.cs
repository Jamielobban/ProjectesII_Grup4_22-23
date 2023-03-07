using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class E11_FiringState : FiringState
{
    Enemy11 enemy;
    bool animEnded = true;
    float lastTimeInLoop;
    float startimeInLoop;
    bool isInLoop;
    private float defDistanceRay = 100;
    LineRenderer m_lineRenderer;

    const float widthX = 0.2f;//0.104f
    bool rotateFirePoint = true;

    bool canApplyDamge = false;
    int? laserAlarmKey;
    int? laserShotKey;

    bool doLaserRedSound = false;

    public E11_FiringState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_FiringState stateData, Enemy11 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        isInLoop = false;
        startimeInLoop = 0;
        lastTimeInLoop = 0;
        m_lineRenderer = enemy.GetComponentInChildren<LineRenderer>();
    }

    public override void Exit()
    {
        base.Exit();
        if (laserAlarmKey.HasValue)
        {
            AudioManager.Instance.RemoveAudio(laserAlarmKey.Value);
        }
    }


    public void StartAnim()
    {
        animEnded = false;
        laserAlarmKey = AudioManager.Instance.LoadSound(enemy.laserAlarm, enemy.player.transform, 0, true);
        AudioManager.Instance.GetAudioFromDictionaryIfPossible(laserAlarmKey.Value).pitch = 2;
        Vector3 aux = enemy.shadow.transform.localPosition;
        aux.x = -1.317f;
        enemy.shadow.transform.localPosition = aux;
    }

    public void StartLaser()
    {
        isInLoop = true;
        canApplyDamge = false;
        m_lineRenderer.enabled = true;

        startimeInLoop = Time.time;
        m_lineRenderer.widthMultiplier = widthX;
        m_lineRenderer.material.SetFloat("_FlickerPercent", 0.2f);
        m_lineRenderer.material.SetFloat("_FlickerFreq", 0.5f);
        m_lineRenderer.material.SetColor("_Color", enemy.laserFollow);
        m_lineRenderer.material.SetColor("_GlowColor", enemy.laserFollow);
        


        FunctionTimer.Create(()=>{
            if(m_lineRenderer != null)
            {
                m_lineRenderer.material.SetFloat("_FlickerFreq", 1);
                if (laserAlarmKey.HasValue)
                {
                    AudioManager.Instance.GetAudioFromDictionaryIfPossible(laserAlarmKey.Value).pitch = 3;
                }
            }
        }, 1);

        FunctionTimer.Create(() => {
            if (m_lineRenderer != null)
            {
                m_lineRenderer.material.SetFloat("_FlickerFreq", 2);
                AudioManager.Instance.GetAudioFromDictionaryIfPossible(laserAlarmKey.Value).pitch = 4;

            }
        }, 2);

        FunctionTimer.Create(() => {
            if (m_lineRenderer != null)
            {
                m_lineRenderer.material.SetFloat("_FlickerFreq", 3);
                AudioManager.Instance.GetAudioFromDictionaryIfPossible(laserAlarmKey.Value).pitch = 6;
            }
        }, 3);

        FunctionTimer.Create(() => {
            if (m_lineRenderer != null)
            {
                m_lineRenderer.material.SetFloat("_FlickerFreq", 5f);
                AudioManager.Instance.GetAudioFromDictionaryIfPossible(laserAlarmKey.Value).pitch = 10;
            }
        }, 4);

        FunctionTimer.Create(() => {
            if (m_lineRenderer != null)
            {
                m_lineRenderer.material.SetFloat("_FlickerFreq", 0);
                doLaserRedSound = true;
            }
        }, 5.5f);

        FunctionTimer.Create(() =>
        {            
            if(m_lineRenderer != null)
            {
                m_lineRenderer.material.SetColor("_Color", enemy.laserDamage);
                m_lineRenderer.material.SetColor("_GlowColor", enemy.laserDamage);
                m_lineRenderer.widthMultiplier = 3;
                rotateFirePoint = false;
                canApplyDamge = true;
                if (laserAlarmKey.HasValue)
                {
                    AudioManager.Instance.RemoveAudio(laserAlarmKey.Value);
                }
            }
        }, 5.75f);
    }    

    public void EndLaser()
    {
        m_lineRenderer.enabled = false;
        m_lineRenderer.material.SetFloat("_FlickerFreq", 0);
        m_lineRenderer.material.SetFloat("_FlickerPercent", 0);
       
    }

    public void EndAnimation()
    {
        animEnded = true;
        rotateFirePoint = true;
        Vector3 aux = enemy.shadow.transform.localPosition;
        aux.x = -0.256f;
        enemy.shadow.transform.localPosition = aux;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Mathf.Abs(enemy.vectorToPlayer.magnitude) >= enemy.distanceToPassToIdle && animEnded)
        {
            stateMachine.ChangeState(enemy.idleState);
        }

        if (((angle < 90 && angle > -90) && enemy.transform.localScale.x < 0) || ((angle > 90 || angle < -90) && enemy.transform.localScale.x > 0) && animEnded && enemy.anim.GetBool("waitingAttackAgain"))
        {           
            enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);            
        }

        if(rotateFirePoint)
            enemy.firePoint.localRotation = Quaternion.Euler(0, 0, angleFirePoint * Mathf.Sign(enemy.transform.localScale.x));

        if(isInLoop && Time.time - startimeInLoop >= stateData.stateDuration)
        {
            EndLaser();
            enemy.anim.SetBool("waitingAttackAgain", true);
            lastTimeInLoop = Time.time;
            isInLoop = false;
        }

        if(!isInLoop && Time.time - lastTimeInLoop >= stateData.timeBetweenShoots && enemy.anim.GetBool("waitingAttackAgain"))
        {
            enemy.anim.SetBool("waitingAttackAgain", false);
        }

        if (isInLoop)
        {
            ShootLaser();
        }

        //Debug.Log(isInLoop);
    }

    void ShootLaser()
    {
        if (Physics2D.Raycast(enemy.firePoint.position, enemy.firePoint.right, defDistanceRay, enemy.laserAffectsLayer))
        {
            RaycastHit2D _hit = Physics2D.Raycast(enemy.firePoint.position, enemy.firePoint.right, defDistanceRay, enemy.laserAffectsLayer);

            if (doLaserRedSound)
            {
                laserShotKey = AudioManager.Instance.LoadSound(enemy.laserShot, enemy.player.transform);

                if (_hit.transform.gameObject.CompareTag("Player"))
                {
                    AudioSource aux = AudioManager.Instance.GetAudioFromDictionaryIfPossible(laserShotKey.Value);
                    if(aux != null)
                    {
                        aux.volume = 1;
                    }
                }
                else
                {
                    AudioSource aux = AudioManager.Instance.GetAudioFromDictionaryIfPossible(laserShotKey.Value);
                    if (aux != null)
                    {
                        aux.volume = 0f;
                    }
                }
                doLaserRedSound = false;
            }

            if (_hit.transform.gameObject.CompareTag("Player"))
            {
                if (canApplyDamge)
                {
                    _hit.rigidbody.gameObject.GetComponent<PlayerMovement>().GetDamage(enemy.laserDamageAmount);
                    canApplyDamge = false;
                    if (laserShotKey.HasValue)
                    {
                        AudioSource aux = AudioManager.Instance.GetAudioFromDictionaryIfPossible(laserShotKey.Value);
                        if (aux != null)
                        {
                            aux.volume = 1f;
                        }
                    }
                }
                if (laserAlarmKey.HasValue)
                {
                    AudioSource aux = AudioManager.Instance.GetAudioFromDictionaryIfPossible(laserAlarmKey.Value);
                    if(aux != null)
                    {
                        aux.volume = 1;
                    }
                }
            }
            else
            {
                if (laserAlarmKey.HasValue)
                {
                    AudioSource aux = AudioManager.Instance.GetAudioFromDictionaryIfPossible(laserAlarmKey.Value);
                    if (aux != null)
                    {
                        aux.volume = 0;
                    }
                }
            }
            Draw2DRay(enemy.firePoint.position, _hit.point);
        }
        else
        {
            Draw2DRay(enemy.firePoint.position, enemy.firePoint.transform.right * defDistanceRay);
        }
    }

    void Draw2DRay(Vector2 startPoint, Vector2 endPoint)
    {
        m_lineRenderer.SetPosition(0, startPoint);
        m_lineRenderer.SetPosition(1, endPoint);
        
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
