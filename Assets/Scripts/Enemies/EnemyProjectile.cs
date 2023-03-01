using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public D_EnemyBullet bulletData;
    int? projectileSoundKey;
    private void Start()
    {
        projectileSoundKey = AudioManager.Instance.LoadSound(bulletData.projectileSound, this.transform, 0, true);
        Destroy(this.gameObject, 50f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SendMessage("GetDamage", bulletData.damage);
            //DestroyProjectile();
        }
        if (other.CompareTag("MapLimit"))
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
        
}
