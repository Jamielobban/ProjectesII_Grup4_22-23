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

    public override void Enter()
    {
        base.Enter();

        if (!enemy.player.GetComponent<PlayerMovement>().infinito)
            Object.Instantiate(stateData.bullets, enemy.transform.position, Quaternion.identity);

        Object.Instantiate(stateData.orbes, enemy.transform.position, Quaternion.identity);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        GameObject deadParticles = GameObject.Instantiate(stateData.deadParticles, entity.transform.position, entity.transform.rotation);
        GameObject.Destroy(enemy.gameObject);
       
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void DoJump()
    {
        enemy.jumpSoundKey = AudioManager.Instance.LoadSound(enemy.jumpSound, enemy.transform, 0, false, true, 0.5f);
    }
}
