using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy6 : Entity
{
    public E6_ChasingState chasingState { get; private set; }
    public E6_DeadState deadState { get; private set; }
    public E6_IdleState idleState { get; private set; }

    [SerializeField]
    private D_ChaseState chasingStateData;    
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    private D_IdleState idleStateData;

    public float distanceToPassToIdle;

    public GameObject burningCircle;
    public GameObject explosion;

    public AudioClip burningCircleSound;
    int? circleSoundKey;
    
    public AudioClip explosionSound;
    int? explosionSoundKey;

    public AudioClip[] zombieSounds;
    float lastTimeZSPlayed;
    float timeBetweenSound;

    public Enemy6()
    {

    }

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

        chasingState = new E6_ChasingState(this, stateMachine, "chase", chasingStateData, this);        
        deadState = new E6_DeadState(this, stateMachine, "dead", deadStateData, this);
        idleState = new E6_IdleState(this, stateMachine, "idle", idleStateData, this);

        stateMachine.Initialize(chasingState);

        agent.speed = enemyData.speed;
        circleSoundKey = AudioManager.Instance.LoadSound(burningCircleSound, burningCircle.transform, 0, true);

        timeBetweenSound = Random.Range(3, 8);
        lastTimeZSPlayed = 0;
    }

    public override void Update()
    {
        base.Update();

        if(!isDead && (Time.time - lastTimeZSPlayed >= timeBetweenSound || lastTimeZSPlayed == 0))
        {
            AudioManager.Instance.LoadSound(zombieSounds[Random.Range(0, zombieSounds.Length)], this.transform);
            lastTimeZSPlayed = Time.time;
            timeBetweenSound = Random.Range(3, 8);
        }

        if(!isDead && vectorToPlayer.magnitude <= 3)
        {
            isDead = true;
        }

        if (isDead && stateMachine.currentState != deadState)
        {
            stateMachine.ChangeState(deadState);
        }

        if (!isDead && stateMachine.currentState != idleState && vectorToPlayer.magnitude >= distanceToPassToIdle && vectorToPlayer.magnitude >= enemyData.stopDistanceFromPlayer)
        {
            stateMachine.ChangeState(idleState);
        }
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
}
