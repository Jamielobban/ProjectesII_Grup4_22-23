using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_DisappearState : DisappearState
{
    Enemy3 enemy;
    float enterTime;
    bool audioPlayed;
    public E3_DisappearState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DisappearState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enterTime = Time.time;
        audioPlayed = false;

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time - enterTime >= 0.4f && !audioPlayed)
        {
            AudioManager.Instance.PlaySound(stateData.disappearSound, enemy.transform.position);
            audioPlayed = true;
        }

        if (Time.time - enterTime >= stateData.timeDisappearDuration && !enemy.GetIfIsDead())
        {
            enemy.holes[enemy.actualHole].GetComponentInChildren<SpriteRenderer>().enabled = true;
            int lastHole = enemy.actualHole;
            do { enemy.actualHole = Random.Range(0, enemy.holes.Count); }while(lastHole == enemy.actualHole);
            stateMachine.ChangeState(enemy.travelState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
