using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E10_DeadState : DeadState
{
    Enemy10 enemy;

    public E10_DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, Enemy10 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        //if (probabilityOfHearth == 0)
        //{
            Object.Instantiate(stateData.bullets, enemy.transform.position, Quaternion.identity);
        //}
        Object.Instantiate(stateData.orbes, enemy.transform.position, Quaternion.identity);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        //deadSoundKey = AudioManager.Instance.LoadSound(stateData.deadSound, enemy.transform.position);
        GameObject deadParticles = GameObject.Instantiate(stateData.deadParticles, entity.transform.position, entity.transform.rotation);
        GameObject.Destroy(enemy.gameObject);
    }
}
