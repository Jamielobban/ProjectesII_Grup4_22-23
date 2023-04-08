using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E15_FiringState : FiringState
{
    Enemy15 enemy;
    public bool doingAttack;

    float lastTimeGas = 0;
    float timeInGas = 8;
    bool doingGas = false;

    public E15_FiringState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_FiringState stateData, Enemy15 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        switch (enemy.mode)
        {
            case 1:
                if (!doingGas && Time.time - enemy.lastTimeExitState >= enemy.waitBetweenAttacks)
                {
                    int random = Random.Range(1, 11);

                    if(random <= 10)
                    {

                    }

                }
                break;
            case 2:
                if (!doingGas && Time.time - enemy.lastTimeExitState >= enemy.waitBetweenAttacks)
                {
                    int random = Random.Range(1, 11);

                    if (random <= 10)
                    {

                    }

                }
                break;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
