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
    Vector3 error;

    public E3_TravelState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_TravelState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        velocity = Vector3.Distance(enemy.transform.position, enemy.holes[enemy.actualHole].transform.position);
        audioKey = AudioManager.Instance.LoadSound(stateData.travelSound, enemy.transform, 0, true);
        error = new Vector3(0.1f, 0.1f, 0);

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

        Vector3 aux;
        aux.x = Mathf.Abs(enemy.transform.position.x - enemy.holes[enemy.actualHole].transform.position.x);
        aux.y = Mathf.Abs(enemy.transform.position.y - enemy.holes[enemy.actualHole].transform.position.y);
        aux.z = 0;

        if (aux.x <= error.x && aux.y <= error.y)
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
