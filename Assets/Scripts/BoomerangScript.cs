using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using UnityEngine.AI;

public class BoomerangScript : MonoBehaviour
{
    public float velocityOnGoPlayer;
    public NavMeshAgent agent;
    public Transform enemy;
    //public Vector3 RestPos;
    float travelTime;
    float timeWhenThrowed;
    bool isGoing = true;
    Vector3 actualDestination;
    
    private void OnEnable()
    {        
        travelTime = GetComponentInParent<Enemy15>().vectorToPlayer.magnitude / velocityOnGoPlayer;
        agent.enabled = false;
        isGoing = true;

        ThrowBoomerang();
        enemy.GetComponent<Enemy15>().boomerangSoundKey = AudioManager.Instance.LoadSound(enemy.GetComponent<Enemy15>().boomerangSound, this.transform, 0, true);
        if (enemy.GetComponent<Enemy15>().boomerangSoundKey.HasValue)
            AudioManager.Instance.GetAudioFromDictionaryIfPossible(enemy.GetComponent<Enemy15>().boomerangSoundKey.Value).pitch = 3;
    }

    private void Update()
    {
        this.transform.rotation = Quaternion.identity;

        if (agent.enabled)
        {
            actualDestination = enemy.transform.position;
            agent.destination = actualDestination;

        }

        if (Time.time - timeWhenThrowed >= travelTime && isGoing)
        {
            agent.enabled = true;
            isGoing = false;
        }

        if (!isGoing)
        {
            Vector3 aux;
            aux.x = Mathf.Abs(this.transform.position.x - actualDestination.x);
            aux.y = Mathf.Abs(this.transform.position.y - actualDestination.y);
            aux.z = 0;
            Vector3 error = new Vector3(1f, 1f, 0);

            if (aux.x <= error.x && aux.y <= error.y)
            {
                agent.enabled = false;
                this.transform.parent = enemy.GetComponentInChildren<SpriteRenderer>().transform;
                this.transform.localPosition = Vector3.zero;
                enemy.GetComponent<Enemy15>().firingState.EndBoomerang();
            }
        }

        this.transform.rotation = Quaternion.identity;


    }

    public void ThrowBoomerang()
    {
        this.transform.parent = null;
        this.transform.DOLocalMove(enemy.GetComponent<Enemy15>().player.transform.position, travelTime);
        timeWhenThrowed = Time.time;
    }
}
