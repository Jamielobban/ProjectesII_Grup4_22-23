using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E12_DeadState : DeadState
{
    Enemy12 enemy;
    public E12_DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, Enemy12 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
}
