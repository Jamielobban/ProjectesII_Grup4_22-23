using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy15 : Entity
{
    public E15_DeadState deadState { get; private set; }
    public E15_IdleState idleState { get; private set; }
    public E15_FiringState firingState { get; private set; }

    [SerializeField]
    private D_FiringState firingStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    private D_IdleState idleStateData;


    public int mode = 1; //1
    public float waitBetweenAttacks = 2; //2
    public float lastTimeExitState = 0;

    public Color colorMode1;
    public Color colorMode2;

    public GameObject gas;
    public GameObject boomerang;
    public GameObject rippleDamage;

    public AudioClip gasSound;
    public int? gasSoundKey;
    public AudioClip boomerangSound;
    public int? boomerangSoundKey;
    public AudioClip groundSound;
    public int? groundSoundKey;
    public AudioClip explosion;
    public int? explosionKey;
    public AudioClip golemSounds;
    public int? golemSoundsKey;
    public AudioClip rockDeadSound;
    public int? rockDeadSoundKey;

    public AudioClip backThemeSound;
    public int? backThemeKey;

    float waitTimeSound = 0;
    float startWaitSound = 0;

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
        base.GetDamage(damageHit, damageType, knockBackForce, bulletPosition, type);
    }

    public override void Start()
    {
        base.Start();

        firingState = new E15_FiringState(this, stateMachine, "fire", firingStateData, this);
        deadState = new E15_DeadState(this, stateMachine, "dead", deadStateData, this);
        idleState = new E15_IdleState(this, stateMachine, "idle", idleStateData, this);

        stateMachine.Initialize(firingState);

        golemSoundsKey = AudioManager.Instance.LoadSound(golemSounds, this.transform, 0, false, true, MixerGroups.ENEMIES,0.4f);

        mode = 1; //1
        waitBetweenAttacks = 2; //2

        backThemeKey = AudioManager.Instance.LoadSound(backThemeSound, player.transform, 0, true, false, MixerGroups.MUSIC,0.5f);
    }

    public override void Update()
    {
        base.Update();

        if (enemyHealth >= 3750)
        {
            mode = 1;
            sr.material.SetColor("_OutlineColor", colorMode1);
            waitBetweenAttacks = 0.7f;
        }
        else
        {
            if (mode != 2 && !firingState.doingAttack)
                mode = 2;

            sr.material.SetColor("_OutlineColor", colorMode2);
            waitBetweenAttacks = 1f;
        }

        if(waitTimeSound == 0)
        {
            waitTimeSound = Random.Range(6.5f, 8.5f);
        }

        if(Time.time - startWaitSound >= waitTimeSound)
        {
            golemSoundsKey = AudioManager.Instance.LoadSound(golemSounds, this.transform, 0, false, true, MixerGroups.ENEMIES,0.4f);
            startWaitSound = Time.time;

        }


        if (isDead && stateMachine.currentState != deadState)
        {
            stateMachine.ChangeState(deadState);
        }
    }

    protected override void GetDamage(float damageHit)
    {
        base.GetDamage(damageHit);
    }

    public void SearchFunction(string funcName)
    {
        Debug.Log("aquisiiiiiiiiiiiiiiiiiii");

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
}
