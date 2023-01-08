using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_TravelState : TravelState
{
    Enemy3 enemy;
    Vector3 myCheck;
    float velocity;
    float timeTravel = 2;
    int? audioKey;
    public E3_TravelState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_TravelState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        velocity = Vector3.Distance(enemy.transform.position, enemy.holes[enemy.actualHole].transform.position);
        audioKey = AudioManager.Instance.LoadSound(stateData.travelSound, enemy.transform, 0, true);
    }

    public override void Exit()
    {
        base.Exit();
        if (audioKey.HasValue)
        {
            AudioManager.Instance.RemoveAudio(audioKey.Value);
        }


    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (enemy.transform.position == enemy.holes[enemy.actualHole].transform.position)
        {                      
            stateMachine.ChangeState(enemy.appearState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if(enemy.transform.position != enemy.holes[enemy.actualHole].transform.position)
        {
            myCheck = Vector3.MoveTowards(enemy.transform.position, enemy.holes[enemy.actualHole].transform.position, velocity * Time.deltaTime);
            enemy.transform.position = myCheck;
        }
        
    }
    
}
