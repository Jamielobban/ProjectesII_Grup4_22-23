using UnityEngine;

public class Enemy12 : Entity
{
    public E12_TravelState travelState { get; private set; }
    public E12_DeadState deadState { get; private set; }
    public E12_IdleState idleState { get; private set; }

    [SerializeField]
    private D_TravelState travelStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    private D_IdleState idleStateData;

    [HideInInspector]
    public SlimePathManager pathManager;
    public Vector3 actualDestination;
    public Vector3 error;

    public float velocity = 2;
    public int zoneID;

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

        travelState = new E12_TravelState(this, stateMachine, "travel", travelStateData, this);
        deadState = new E12_DeadState(this, stateMachine, "dead", deadStateData, this);
        idleState = new E12_IdleState(this, stateMachine, "idle", idleStateData, this);

        stateMachine.Initialize(travelState);        

        error = new Vector3(0.1f, 0.1f, 0);
    }

    public override void Update()
    {
        base.Update();

        if (isDead && stateMachine.currentState != deadState)
        {
            stateMachine.ChangeState(deadState);
        }
    }

    protected override void GetDamage(float damageHit)
    {
        base.GetDamage(damageHit);
    }
}
