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
    public float waitBetweenAttacks = 3;
    protected float angle;
    public int direction;
    public float lastTimeExitState = 0;


    public bool flip = true;

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

        
        firingState = new E13_FiringState(this, stateMachine, "fire", firingStateData, this);
        deadState = new E13_DeadState(this, stateMachine, "dead", deadStateData, this);
        idleState = new E13_IdleState(this, stateMachine, "idle", idleStateData, this);
        blockState = new E13_BlockState(this, stateMachine, "block", blockStateData, this);

        stateMachine.Initialize(firingState);

        mode = 1;
        waitBetweenAttacks = 3;
    }

    public override void Update()
    {
        base.Update();

        angle = Mathf.Atan2(vectorToPlayer.y, vectorToPlayer.x) * Mathf.Rad2Deg;

        if (enemyHealth >= enemyData.health - enemyData.health / 3)
        {
            mode = 1;
            waitBetweenAttacks = 3;
        }
        else if (enemyHealth >= enemyData.health - enemyData.health / 2)
        {
            mode = 2;
            waitBetweenAttacks = 2.3f;

        }
        else
        {
            mode = 3;
            waitBetweenAttacks = 1.5f;
        }

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
        else
        {
            aux.x = Mathf.Abs(aux.x);
        }

        transform.localScale = aux;

        anim.SetInteger("direction", direction);
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
