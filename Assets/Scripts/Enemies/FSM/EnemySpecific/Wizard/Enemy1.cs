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

    public override Transform/*KeyValuePair<Transform, float>*/ GetBurnValues()
    {
        GameObject emptyGO = new GameObject();
        if (variant == Enemy1Variants.SINGLESHOT)
        {
            emptyGO.transform.position = new Vector3(0, 0.475f, 0);
            emptyGO.transform.rotation = Quaternion.identity;
            emptyGO.transform.localScale = new Vector3(0.325f, 0.375f, 1);//1.3f, 1.5f, 1)
            return /*new KeyValuePair<Transform, float>(*/emptyGO.transform/*, 0.09f)*/;
        }
        throw new System.NotImplementedException();

    }
}
