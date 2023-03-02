using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class E7_SlashState : SlashState
{
    Enemy7 enemy;
    public E7_SlashState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_SlashState stateData, Enemy7 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (!enemy.GetComponentInChildren<PlayerDetetctorArea>().playerInside && !enemy.doingAttack)
        {
            enemy.stateMachine.ChangeState(enemy.chasingState);
        }


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (((angle < 90 && angle > -90) && enemy.transform.localScale.x > 0) || ((angle > 90 || angle < -90) && enemy.transform.localScale.x < 0))
        {
            enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);

        }

        enemy.GetComponentsInChildren<Transform>().Where(t => (t.gameObject.CompareTag("FirePoint"))).ToArray()[0].localRotation = Quaternion.Euler(0, 0, angle * Mathf.Sign(enemy.transform.localScale.x));
    }

    public void AnimationStarted()
    {
        enemy.doingAttack = true;
    }
    public void AnimationEnded()
    {
        enemy.doingAttack = false;
    }
}
