using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class E4_DeadState : DeadState
{
    private Enemy4 enemy;
    int? deadSoundKey;

    public E4_DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, Enemy4 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        if (probabilityOfHearth == 0)
        {
            Object.Instantiate(stateData.hearth, enemy.transform.position, Quaternion.identity);
        }        
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
        deadSoundKey = AudioManager.Instance.LoadSound(stateData.deadSound, enemy.transform.position);
        GameObject deadParticles = GameObject.Instantiate(stateData.deadParticles, entity.transform.position, entity.transform.rotation);
        GameObject.Destroy(enemy.gameObject);
    }
}
