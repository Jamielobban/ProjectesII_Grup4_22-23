using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class E1_Chasingtate : ChasingState
{
    private Enemy1 enemy;
    //private int? followSoundKey;
    int? stomp1Key;
    int? stomp2Key; 

    public E1_Chasingtate(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChaseState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.agent.enabled = true;

        //followSoundKey = AudioManager.Instance.LoadSound(stateData.followSounds, enemy.transform, 0.2f, true);
    }

    public override void Exit()
    {
        base.Exit();

        enemy.agent.enabled = false;
        //if (followSoundKey.HasValue)
        //{
        //    AudioManager.Instance.RemoveAudio(followSoundKey.Value);
        //}
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //Debug.Log(followSoundKey);


        inRange = entity.vectorToPlayer.magnitude < enemy.enemyData.stopDistanceFromPlayer;

        if (inRange)
        {
            stateMachine.ChangeState(enemy.firingState);
        }        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        //if (angle > 90 && angle < 270)
        //{
        //    if (enemy.transform.localScale.x > 0)
        //    {
        //        enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);
        //    }
        //}
        //else
        //{
        //    if (enemy.transform.localScale.x < 0)
        //    {
        //        enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);
        //    }
        //}
        if (((angle < 90 && angle > -90) && enemy.transform.localScale.x < 0) || ((angle > 90 || angle < -90) && enemy.transform.localScale.x > 0))
        {
            enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);

        }

        enemy.GetComponentsInChildren<Transform>().Where(t => (t.gameObject.CompareTag("FirePoint"))).ToArray()[0].localRotation = Quaternion.Euler(0, 0, angle * Mathf.Sign(enemy.transform.localScale.x));

        //enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, enemy.player.position, enemy.enemyData.speed * Time.fixedDeltaTime);
        enemy.agent.SetDestination(new Vector3(enemy.player.position.x, enemy.player.position.y, enemy.transform.position.z));

    }

    public void DoStomp1()
    {
        stomp1Key = AudioManager.Instance.LoadSound(stateData.Stomp1, enemy.transform);
    }

    public void DoStomp2()
    {
        stomp2Key = AudioManager.Instance.LoadSound(stateData.Stomp2, enemy.transform);
    }


}
