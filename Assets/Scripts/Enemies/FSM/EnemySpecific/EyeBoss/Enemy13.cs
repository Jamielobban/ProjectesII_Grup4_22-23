using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy13 : Entity
{
    public E13_DeadState deadState { get; private set; }
    public E13_IdleState idleState { get; private set; }
    public E13_FiringState firingState { get; private set; }
    public E13_BlockState blockState { get; private set; }

    [SerializeField]
    private D_FiringState firingStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_BlockState blockStateData;

    public int mode = 1;
    public float waitBetweenAttacks = 3f;
    protected float angle;
    public int direction;
    public float lastTimeExitState = 0;
   
    public LayerMask laserAffectsLayer;
    public GameObject laserChargeParticles;
    public GameObject centerVr;
    public GameObject shield;
    public GameObject sparks;
    public GameObject sombra;
    public GameObject spinFx;
    public SpriteMask spriteMask;

    public bool flip = true;

    public Color colorMode1;
    public Color colorMode2;
    public Color colorMode3;
    public GameObject eyesBall;
    public EyeBossPathScript pathScript;
    public Vector3 restingPoint;

    public GameObject eyeMonsterPrefab;
    public float laserMaxSize;

    public AudioClip laserChargeSound;
    public int? laserChargeSoundKey;

    public AudioClip laserSound;
    public int? laserSoundKey;

    public AudioClip dashSound;
    public int? dashSoundKey;
    
    public AudioClip dashSound2;
    public int? dashSoundKey2;

    public AudioClip bulletThrowSound;
    public int? bulletThrowSoundKey;

    public AudioClip squelchingSound;
    public int? squelchingSoundKey;

    public AudioClip backThemeSound;
    public int? backThemeSoundKey;

    public AudioClip deadSound;
    public int? deadSoundKey;



    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override Transform GetBurnValues()
    {
        throw new System.NotImplementedException();
    }

    public override void GetDamage(float damageHit, HealthStateTypes damageType, float knockBackForce, Vector3 bulletPosition, TransformMovementType type)
    {
        if(stateMachine.currentState != idleState)
        {
            if (stateMachine.currentState != firingState || (mode != 3 || (!firingState.doingShieldSpin && !firingState.returningRest)))
            {
                base.GetDamage(damageHit, damageType, knockBackForce, bulletPosition, type);
            }
        }
        
    }

    public override void Start()
    {
        base.Start();

        
        firingState = new E13_FiringState(this, stateMachine, "fire", firingStateData, this);
        deadState = new E13_DeadState(this, stateMachine, "dead", deadStateData, this);
        idleState = new E13_IdleState(this, stateMachine, "idle", idleStateData, this);
        blockState = new E13_BlockState(this, stateMachine, "block", blockStateData, this);

        stateMachine.Initialize(idleState);

        mode = 1;//1
        waitBetweenAttacks = 3f;

        backThemeSoundKey = AudioManager.Instance.LoadSound(backThemeSound, player.transform, 0, true, false, 0.5f);


        laserMaxSize = GetComponentInChildren<LineRenderer>().widthMultiplier;
        foreach(LineRenderer lr in GetComponentsInChildren<LineRenderer>()) {
            lr.material.SetFloat("_ClipUvUp", 0.5f);
            lr.material.SetFloat("_ClipUvDown", 0.5f);
        }
    }

    public override void Update()
    {
        base.Update();

        if (enemyHealth >= 1666.67f * 2)
        {
            mode = 1;
            sr.material.SetColor("_OutlineColor", colorMode1);
            waitBetweenAttacks = 3;
        }
        else if (enemyHealth >= 1666.67f)
        {
            if (mode != 2 && !firingState.doingAttack)
                mode = 2;

            sr.material.SetColor("_OutlineColor", colorMode2);
            waitBetweenAttacks = 2;
        }
        else
        {
            if (mode != 3 && !firingState.doingAttack)
                mode = 3;

            sr.material.SetColor("_OutlineColor", colorMode3);
            waitBetweenAttacks = 1.35f;
        }

        //switch (mode)
        //{
        //    case 1:
        //        sr.material.SetColor("_OutlineColor", colorMode1);
        //        waitBetweenAttacks = 3;
        //        break;
        //    case 2:
        //        sr.material.SetColor("_OutlineColor", colorMode2);
        //        waitBetweenAttacks = 2;
        //        break;
        //    case 3:
        //        sr.material.SetColor("_OutlineColor", colorMode3);
        //        waitBetweenAttacks = 1.35f;
        //        break;
        //}

        angle = Mathf.Atan2(vectorToPlayer.y, vectorToPlayer.x) * Mathf.Rad2Deg;
        

        if (isDead && stateMachine.currentState != deadState)
        {
            stateMachine.ChangeState(deadState);
        }        
        
        
        if(angle > 54 && angle <= 126)
        {
            direction = 1;
        }
        else if( (angle > 18 && angle <= 54) || (angle > 126 && angle <= 162))
        {
            direction = 2;
        }
        else if( (angle >= -18 && angle < 18  ) || (angle <= -162 || angle > 162 ))
        {
            direction = 3;
        }
        else if ((angle > -162 && angle <= -126) || (angle < -18 && angle >= -54))
        {
            direction = 4;
        }
        else if(angle > -126 && angle < -54)
        {
            direction = 5;
        }

        Vector3 aux = transform.localScale;

        if((angle > 90 || angle < -90) && flip)
        {
            if(aux.x > 0)
            {
                aux.x = -1 * Mathf.Abs(aux.x);
            }
        }
        else if(flip)
        {
            aux.x = Mathf.Abs(aux.x);
        }

        transform.localScale = aux;

        anim.SetInteger("direction", direction);

       // Debug.Log(stateMachine.currentState);
    }

    protected override void GetDamage(float damageHit)
    {
        
            base.GetDamage(damageHit);
        
    }

    public void SearchFunction(string funcName)
    {
        if (this.gameObject == null || stateMachine.currentState == null || stateMachine.currentState == deadState)
            return;
        var exampleType = stateMachine.currentState.GetType();
        var exampleMethod = exampleType.GetMethod(funcName);
        try { exampleMethod.Invoke(stateMachine.currentState, null); } catch { /*Debug.Log(funcName); Debug.Log(stateMachine.currentState);*/ };
        if (this.gameObject == null)
        {
            CancelInvoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(stateMachine.currentState == firingState && mode == 3 && (firingState.doingShieldSpin || firingState.returningRest) && collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().GetDamage(2);
        }
    }

}
