using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy7 : Entity
{
    public E7_ChasingState chasingState { get; private set; }
    public E7_DeadState deadState { get; private set; }
    public E7_IdleState idleState { get; private set; }
    public E7_FiringState firingState { get; private set; }
    public E7_SlashState slashState { get; private set; }

    [SerializeField]
    private D_ChaseState chasingStateData;
    [SerializeField]
    private D_FiringState firingStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_SlashState slashStateData;

    public Vector3[] idleTravelPoints;
    public float distanceToPassToIdle;
    [HideInInspector]
    public bool doingAttack;
    [HideInInspector]
    public float lastTimeShoot;
    [HideInInspector]
    public float timeBetweenShoots;

    float lastTimeFliped;

    public Enemy7()
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

        chasingState = new E7_ChasingState(this, stateMachine, "chase", chasingStateData, this);
        firingState = new E7_FiringState(this, stateMachine, "fire", firingStateData, this);
        deadState = new E7_DeadState(this, stateMachine, "dead", deadStateData, this);
        idleState = new E7_IdleState(this, stateMachine, "idle", idleStateData, this);
        slashState = new E7_SlashState(this, stateMachine, "slash", slashStateData, this);

        stateMachine.Initialize(chasingState);

        timeBetweenShoots = Random.Range(5f, 10f);

        agent.speed = enemyData.speed;
        lastTimeFliped = 0;
    }

    public override void Update()
    {
        base.Update();

        if(stateMachine.currentState == idleState && agent.enabled)
        {
            agent.speed = enemyData.speed;
        }
        else if(stateMachine.currentState == chasingState && agent.enabled)
        {
            agent.speed = enemyData.speed * 3;
        }

        if (isDead && stateMachine.currentState != deadState)
        {
            stateMachine.ChangeState(deadState);
        }

        //if (!isDead && stateMachine.currentState != idleState && vectorToPlayer.magnitude >= distanceToPassToIdle && !doingAttack)
        //{
        //    stateMachine.ChangeState(idleState);
        //}

        if(!isDead && stateMachine.currentState == chasingState && Time.time - lastTimeShoot >= timeBetweenShoots)
        {
            stateMachine.ChangeState(firingState);
        }

        if(stateMachine.currentState != slashState && stateMachine.currentState != firingState && agent.enabled && Time.time - lastTimeFliped >= 0.6f)
        {
            if (agent.desiredVelocity.x > 0)
            {
                Vector3 aux = this.transform.localScale;
                aux.x = -Mathf.Abs(aux.x);
                this.transform.localScale = aux;
                lastTimeFliped = Time.time;
            }
            else if(agent.desiredVelocity.x < 0)
            {
                Vector3 aux = this.transform.localScale;
                aux.x = Mathf.Abs(aux.x);
                this.transform.localScale = aux;
                lastTimeFliped = Time.time;
            }
            else
            {
                if(stateMachine.currentState == chasingState)
                {
                    if(player.transform.position.x > this.transform.position.x)
                    {
                        Vector3 aux = this.transform.localScale;
                        aux.x = -Mathf.Abs(aux.x);
                        this.transform.localScale = aux;
                        lastTimeFliped = Time.time;

                    }
                    else if(player.transform.position.x < this.transform.position.x)
                    {
                        Vector3 aux = this.transform.localScale;
                        aux.x = Mathf.Abs(aux.x);
                        this.transform.localScale = aux;
                        lastTimeFliped = Time.time;

                    }
                }
            }

        }
        Debug.Log(doingAttack);
        //if(!isDead && !doingAttack && stateMachine.currentState != slashState && vectorToPlayer.magnitude <= 3 && (player.position.x >= transform.position.x + 2 || transform.position.x >= player.position.x + 2))
        //{
        //    stateMachine.ChangeState(slashState);
        //}
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
