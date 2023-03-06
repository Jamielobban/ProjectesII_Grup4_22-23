using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyProjectile : MonoBehaviour
{
    public D_EnemyBullet bulletData;
    public bool destroyOnTourchPlayer = true;
    public bool movePlayer = false;
    int? projectileSoundKey;
    public GameObject hangPlayer;
    
    private void Start()
    {
        projectileSoundKey = AudioManager.Instance.LoadSound(bulletData.projectileSound, this.transform, 0, true);
        Destroy(this.gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SendMessage("GetDamage", bulletData.damage);
            if (destroyOnTourchPlayer)
            {
                DestroyProjectile();
            }
            else if (movePlayer)
            {
                MovePlayer(other);
            }

        }
        if (other.CompareTag("MapLimit"))
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        FreeChilds();

        Transform[] parents = GetComponentsInParent<Transform>().Where(t => (t.GetComponent<Rigidbody2D>())).ToArray();
        if(parents.Length > 0)
        {
            Destroy(parents[0].gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void FreeChilds()
    {
        Transform[] childs = hangPlayer.GetComponentsInChildren<Transform>().Where(t => (t.GetComponent<PlayerMovement>())).ToArray();
        if (childs.Length > 0)
        {
            childs[0].parent = null;
            childs[0].GetComponent<PlayerMovement>().canDash = true;
            childs[0].GetComponent<PlayerMovement>().canMove = true;
        }
    }
        
    void MovePlayer(Collider2D other)
    {
        other.GetComponent<PlayerMovement>().canDash = false;
        other.GetComponent<PlayerMovement>().canMove = false;


        other.GetComponent<Transform>().parent = hangPlayer.transform;
       

    }
}
