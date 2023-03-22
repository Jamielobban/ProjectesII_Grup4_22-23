using UnityEngine;

public class Enemy12 : Entity
{
    public E12_TravelState travelState { get; private set; }
    public E12_DeadState deadState { get; private set; }

    [SerializeField]
    private D_TravelState travelStateData;
    [SerializeField]
    private D_DeadState deadStateData;

    


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

        stateMachine.Initialize(travelState);

    }

    public override void Update()
    {
        base.Update();
    }

    protected override void GetDamage(float damageHit)
    {
        base.GetDamage(damageHit);
    }
}
