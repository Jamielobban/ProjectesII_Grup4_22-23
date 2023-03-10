using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_DeadState : DeadState
{
    Enemy2 enemy;
    int? deadSoundKey;
    public E2_DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        deadSoundKey = AudioManager.Instance.LoadSound(stateData.deadSound, enemy.transform.position);
        if (probabilityOfHearth == 0)
        {
            Object.Instantiate(stateData.bullets, enemy.transform.position, Quaternion.identity);
        }
        Object.Instantiate(stateData.orbes, enemy.transform.position, Quaternion.identity);
    }

    public override void Exit()
    {
        base.Exit();
        if (deadSoundKey.HasValue)
        {
            AudioManager.Instance.RemoveAudio(deadSoundKey.Value);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        GameObject deadParticles = GameObject.Instantiate(stateData.deadParticles, entity.transform.position, entity.transform.rotation);
        GameObject.Destroy(enemy.gameObject);
    }
}
