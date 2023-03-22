using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E12_TravelState : TravelState
{
    Enemy12 enemy;
    public E12_TravelState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_TravelState stateData, Enemy12 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
}
