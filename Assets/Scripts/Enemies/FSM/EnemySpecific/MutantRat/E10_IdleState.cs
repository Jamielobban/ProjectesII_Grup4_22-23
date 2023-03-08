using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E10_IdleState : IdleState
{
    Enemy10 enemy;
    float timeLastFlip;
    int flipsDone;
    Vector3 aux;
    float timeBetweenFlips;
    float timeToNextFlips;
    int? idleSoundsKey;

    public E10_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Enemy10 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        aux = enemy.transform.localScale;
        aux.x *= -1;
        enemy.transform.localScale = aux;

        timeLastFlip = Time.time;
        flipsDone = 1;

        timeBetweenFlips = Random.Range(1f, 3f);
        timeToNextFlips = Random.Range(3.5f, 7f);

        idleSoundsKey = AudioManager.Instance.LoadSound(stateData.idleSounds, enemy.transform, 0, true);
    }

    public override void Exit()
    {
        base.Exit();

        if (idleSoundsKey.HasValue)
        {
            AudioManager.Instance.RemoveAudio(idleSoundsKey.Value);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Mathf.Abs(enemy.vectorToPlayer.magnitude) <= stateData.rangeToPassToChasing)
        {
            stateMachine.ChangeState(enemy.chasingState);
        }

        if (flipsDone == 1)
        {
            if (Time.time - timeLastFlip >= timeBetweenFlips)
            {
                aux = enemy.transform.localScale;
                aux.x *= -1;
                enemy.transform.localScale = aux;

                timeLastFlip = Time.time;
                flipsDone = 0;

                timeBetweenFlips = Random.Range(1f, 3f);
            }
        }
        else if (flipsDone == 0)
        {
            if (Time.time - timeLastFlip >= timeToNextFlips)
            {
                aux = enemy.transform.localScale;
                aux.x *= -1;
                enemy.transform.localScale = aux;

                timeLastFlip = Time.time;
                flipsDone = 1;

                timeToNextFlips = Random.Range(3.5f, 7f);

            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void StartAnimation()
    {

    }

    public void EndAttack()
    {

    }

    public void Shoot()
    {

    }

}
