using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_Chasingtate chasingState { get; private set; }
    public E1_FiringState firingState { get; private set; }

    [SerializeField]
    private D_ChaseState chasingStateData;
    [SerializeField]
    private D_FiringState firingStateData;

    [SerializeField]
    private Enemy1Variants variant;

    [SerializeField]
    Dictionary<Enemy1Variants, D_ChaseState> chaseStateDatasVariants;
    [SerializeField]
    Dictionary<Enemy1Variants, D_FiringState> firingStateDatasVariants;

    public override void Start()
    {
        base.Start();

        chasingStateData = chaseStateDatasVariants[variant];
        firingStateData = firingStateDatasVariants[variant];

        chasingState = new E1_Chasingtate(this, stateMachine, "chase", chasingStateData, this);
        firingState = new E1_FiringState(this, stateMachine, "fire", firingStateData, this);
    }

    
}
