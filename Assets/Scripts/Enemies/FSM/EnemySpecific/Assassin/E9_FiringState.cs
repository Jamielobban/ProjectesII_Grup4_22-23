using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class E9_FiringState : FiringState
{
    Enemy9 enemy;
    bool animationDone = true;
    public E9_FiringState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_FiringState stateData, Enemy9 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (enemy.enemyVariant)
        {
            inRange = entity.vectorToPlayer.magnitude < enemy.enemyData.stopDistanceFromPlayer + 5;

            if (!inRange && animationDone)
            {
                stateMachine.ChangeState(enemy.chasingState);
            }
        }
        //else
        //{

        //}

        
    }

    public void StartAnimation()
    {
        animationDone = false;

        if (!enemy.enemyVariant)
        {
            //if(enemy.player.transform.position.x < enemy.transform.position.x)
            //{
            //    enemy.transform.DOMoveX(enemy.transform.position.x - 5, 0.5f);
            //}
            //else
            //{
            //    enemy.transform.DOMoveX(enemy.transform.position.x + 5, 0.5f);
            //}

            //enemy.player.position + (-enemy.vectorToPlayer.normalized * 6)
            enemy.transform.DOMove(enemy.transform.position + (enemy.vectorToPlayer.normalized * 5), 0.5f);
        }
        
    }

    public void ShootSlash()
    {
        if (enemy.enemyVariant)
        {
            GameObject bullet = GameObject.Instantiate(stateData.bulletType, enemy.GetComponent<Entity>().GetFirePointTransform().position, enemy.GetComponent<Entity>().GetFirePointTransform().rotation);

            //fireSoundKey = AudioManager.Instance.LoadSound(stateData.shootShound, enemy.GetComponent<Entity>().GetFirePointTransform().position);
            Quaternion aux2 = bullet.GetComponent<Transform>().rotation;
            aux2.z += Quaternion.Euler(0, 0, Random.Range(-8, 8f)).z;
            bullet.GetComponentInChildren<Transform>().rotation = aux2;

            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * bullet.GetComponentInChildren<EnemyProjectile>().bulletData.speed, ForceMode2D.Impulse);
            if (Random.Range(0, 2) == 0)
            {
                Vector3 aux = bullet.GetComponentInChildren<Transform>().localScale;
                aux.y *= -1;
                bullet.GetComponentInChildren<Transform>().localScale = aux;
            }

            Quaternion aux3 = bullet.GetComponentInChildren<Transform>().rotation;
            aux3.z += Quaternion.Euler(0, 0, Random.Range(-30, 30f)).z;
            bullet.GetComponentInChildren<Transform>().rotation = aux3;

            var color = bullet.GetComponentInChildren<SpriteRenderer>().color;
            color.a = 0;
            bullet.GetComponentInChildren<SpriteRenderer>().color = color;
            bullet.GetComponentInChildren<SpriteRenderer>().DOFade(1, 0.3f);
        }
        else
        {
            GameObject bullet = GameObject.Instantiate(stateData.bulletType, enemy.GetComponent<Entity>().GetFirePointTransform().position, enemy.GetComponent<Entity>().GetFirePointTransform().rotation);

            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * bullet.GetComponentInChildren<EnemyProjectile>().bulletData.speed, ForceMode2D.Impulse);

            var color = bullet.GetComponentInChildren<SpriteRenderer>().color;
            color.a = 0;
            bullet.GetComponentInChildren<SpriteRenderer>().color = color;
            bullet.GetComponentInChildren<SpriteRenderer>().DOFade(1, 0.3f);

            Transform t = bullet.GetComponentsInChildren<Transform>().Where(t => t.GetComponent<SpriteRenderer>()).ToArray()[0];
            t.localScale = Vector3.zero;
            t.DOScale(Vector3.one, 0.3f);

            enemy.anim.SetBool("waitingRestTime", true);
            

            FunctionTimer.Create(() =>
            {
                if (enemy != null && !enemy.GetIfIsDead())
                {
                    animationDone = true;
                }
            }, 3f);

            FunctionTimer.Create(() =>
            {
                if (enemy != null && !enemy.GetIfIsDead())
                {
                    enemy.anim.SetBool("waitingRestTime", false);

                    if (!inRange)
                    {
                        enemy.stateMachine.ChangeState(enemy.chasingState);
                    }
                }
            }, 3);
        }
       
    }

    public void EndAnimation()
    {
        if (enemy.enemyVariant)
        {
            animationDone = true;
        }        
    }
    

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (((angle < 90 && angle > -90) && enemy.transform.localScale.x < 0) || ((angle > 90 || angle < -90) && enemy.transform.localScale.x > 0) && animationDone)
        {
            enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);

        }

        enemy.GetComponentsInChildren<Transform>().Where(t => (t.gameObject.CompareTag("FirePoint"))).ToArray()[0].localRotation = Quaternion.Euler(0, 0, angle * Mathf.Sign(enemy.transform.localScale.x));

        //enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, enemy.player.position, enemy.enemyData.speed * Time.fixedDeltaTime);
        
    }
}
