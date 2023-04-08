using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy14 : Entity
{
    public E14_DeadState deadState { get; private set; }
    public E14_IdleState idleState { get; private set; }
    public E14_FiringState firingState { get; private set; }    

    [SerializeField]
    private D_FiringState firingStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    private D_IdleState idleStateData;

    public GameObject flameWave;
    public GameObject multiSwords;

    public int mode = 2; //1
    public float waitBetweenAttacks = 1.5f; //2
    public float lastTimeExitState = 0;

    public Color colorMode1;
    public Color colorMode2;

    public GameObject sword4waves;
    public GameObject idleSwords;
    public GameObject attack2Swords;
    public GameObject missileSwords;

    public Vector3[] posibleSpawnPoints = new Vector3[5];

    [HideInInspector]
    public GameObject idleSwordsInstance;

    public override void Start()
    {
        base.Start();

        firingState = new E14_FiringState(this, stateMachine, "fire", firingStateData, this);
        deadState = new E14_DeadState(this, stateMachine, "dead", deadStateData, this);
        idleState = new E14_IdleState(this, stateMachine, "idle", idleStateData, this);

        stateMachine.Initialize(idleState);

        mode = 2; //1
        waitBetweenAttacks = 1.5f; //2
    }

    public override void Update()
    {
        base.Update();

        //if (enemyHealth >= 2000)
        //{
        //    mode = 1;
        //    sr.material.SetColor("_OutlineColor", colorMode1);
        //    waitBetweenAttacks = 2f;
        //}
        //else
        //{
        //    if (mode != 2 && !firingState.doingAttack)
        //        mode = 2;

        //    sr.material.SetColor("_OutlineColor", colorMode2);
        //    waitBetweenAttacks = 1.5f;
        //}

        if (isDead && stateMachine.currentState != deadState)
        {
            stateMachine.ChangeState(deadState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void GetDamage(float damageHit, HealthStateTypes damageType, float knockBackForce, Vector3 bulletPosition, TransformMovementType type)
    {
        base.GetDamage(damageHit, damageType, knockBackForce, bulletPosition, type);
    }

    protected override void GetDamage(float damageHit)
    {
        base.GetDamage(damageHit);
    }

    public override Transform GetBurnValues()
    {
        throw new System.NotImplementedException();
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
