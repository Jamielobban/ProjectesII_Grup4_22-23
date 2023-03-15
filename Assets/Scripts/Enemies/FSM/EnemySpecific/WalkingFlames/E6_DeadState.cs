using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class E6_DeadState : DeadState
{
    Enemy6 enemy;
    int? deadSoundKey;
    

    public E6_DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, Enemy6 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        

        enemy.GetComponentInChildren<SpriteRenderer>().enabled = false;
        GameObject.Instantiate(enemy.blood, enemy.transform.position, enemy.transform.rotation);
        GameObject.Instantiate(enemy.deadBlood, enemy.transform.position, enemy.transform.rotation);
        GameObject deadParticles = GameObject.Instantiate(stateData.deadParticles, entity.transform.position, entity.transform.rotation);
        //deadSoundKey = AudioManager.Instance.LoadSound(stateData.deadSound, enemy.transform.position);


        enemy.burningCircle.transform.DOScale(Vector3.zero, 0.25f);
        GameObject.Destroy(enemy.burningCircle, 0.25f);
        deadSoundKey = AudioManager.Instance.LoadSound(enemy.explosionSound, enemy.explosion.transform.position, 0.15f);
        FunctionTimer.Create(() =>
        {            
            GameObject explosion = GameObject.Instantiate(enemy.explosion, enemy.transform.position, Quaternion.identity);
            //if (probabilityOfHearth == 0)
            //{
                Object.Instantiate(stateData.bullets, enemy.transform.position, Quaternion.identity);
            //}
            Object.Instantiate(stateData.orbes, enemy.transform.position, Quaternion.identity);

            GameObject.Destroy(enemy.gameObject);


        }, 0.25f);
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

        
    }
    
}
