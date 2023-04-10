using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E15_DeadState : DeadState
{
    Enemy15 enemy;
    public E15_DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, Enemy15 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        AudioManager.Instance.RemoveAudio(enemy.backThemeKey.Value);
        GameObject deadParticles = GameObject.Instantiate(enemy.deadBlood, entity.transform.position, entity.transform.rotation);
        AudioManager.Instance.LoadSound(enemy.rockDeadSound, enemy.transform.position);
        GameObject.Destroy(enemy.gameObject);
    }
}
