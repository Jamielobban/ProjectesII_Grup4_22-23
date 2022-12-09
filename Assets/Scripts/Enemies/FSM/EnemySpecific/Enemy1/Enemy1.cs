using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_Chasingtate chasingState { get; private set; }
    public E1_FiringState firingState { get; private set; }
    public E1_DeadState deadState { get; private set; }

    [SerializeField]
    private D_ChaseState chasingStateData;
    [SerializeField]
    private D_FiringState firingStateData;
    [SerializeField]
    private D_DeadState deadStateData;

    [SerializeField]
    private Enemy1Variants variant;

    
    //public Dictionary<Enemy1Variants, D_ChaseState> chaseStateDatasVariants;

    //public Dictionary<Enemy1Variants, D_FiringState> firingStateDatasVariants;

    public override void Start()
    {
        base.Start();

        //chasingStateData = chaseStateDatasVariants[variant];
        //firingStateData = firingStateDatasVariants[variant];

        chasingState = new E1_Chasingtate(this, stateMachine, "chase", chasingStateData, this);
        firingState = new E1_FiringState(this, stateMachine, "fire", firingStateData, this);
        deadState = new E1_DeadState(this, stateMachine, "dead", deadStateData, this);

        stateMachine.Initialize(chasingState);
    }

    public override void Update()
    {
        base.Update();

        if(isDead && stateMachine.currentState != deadState)
        {
            stateMachine.ChangeState(deadState);
        }
    }    

    public Enemy1Variants GetVariant()
    {
        return variant;
    }
}
