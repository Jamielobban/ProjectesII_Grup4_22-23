using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Entity
{
    public E3_TravelState travelState { get; private set; }
    public E3_AppearState appearState { get; private set; }
    public E3_DisappearState disappearState { get; private set; }
    public E3_FiringState firingState { get; private set; }
    public E3_DeadState deadState { get; private set; }

    public GameObject hole;
    public List<Vector3> holesPositions = new List<Vector3>();

    [HideInInspector]
    public List<GameObject> holes = new List<GameObject>();
    [HideInInspector]
    public int actualHole;

    [SerializeField]
    private D_TravelState travelStateData;
    [SerializeField]
    private D_AppearState appearStateData;
    [SerializeField]
    private D_DisappearState disappearStateData;
    [SerializeField]
    private D_FiringState firingStateData;
    [SerializeField]
    private D_DeadState deadStateData;

    private BoxCollider2D bc;

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
        if(stateMachine.currentState != travelState)
        {
            base.GetDamage(damageHit, damageType, knockBackForce, bulletPosition, type);
        }
    }

    public override void Start()
    {
        base.Start();

        bc = GetComponent<BoxCollider2D>();

        travelState = new E3_TravelState(this, stateMachine, "travel", travelStateData, this);
        appearState = new E3_AppearState(this, stateMachine, "appear", appearStateData, this);
        disappearState = new E3_DisappearState(this, stateMachine, "disappear", disappearStateData, this);
        firingState = new E3_FiringState(this, stateMachine, "fire", firingStateData, this);
        deadState = new E3_DeadState(this, stateMachine, "dead", deadStateData, this);

        foreach(Vector3 holePos in holesPositions)
        {
            holes.Add(Object.Instantiate(hole, holePos, Quaternion.identity));
        }

        actualHole = Random.Range(0, holes.Count);

        //holes[actualHole].GetComponentInChildren<SpriteRenderer>().enabled = false;
        this.transform.position = holes[actualHole].transform.position;

        stateMachine.Initialize(appearState);
    }

    public override void Update()
    {
        base.Update();

        if (isDead && stateMachine.currentState != deadState)
        {
            stateMachine.ChangeState(deadState);
        }

        if(stateMachine.currentState == travelState)
        {
            bc.enabled = false;
        }
        else
        {
            bc.enabled = true;
        }
    }

    protected override void GetDamage(float damageHit)
    {
        base.GetDamage(damageHit);
    }

    public void SearchFunction(string funcName)
    {
        var exampleType = stateMachine.currentState.GetType();
        var exampleMethod = exampleType.GetMethod(funcName);
        exampleMethod.Invoke(stateMachine.currentState, null);
    }
}
