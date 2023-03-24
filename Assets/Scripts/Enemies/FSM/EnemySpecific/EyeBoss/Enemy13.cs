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
