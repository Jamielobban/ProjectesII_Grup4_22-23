using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public D_EnemyBullet bulletData;
    int? projectileSoundKey;
    private CircleCollider2D _collider;
    private Rigidbody2D body2D;
    private Animator _animator;
    private void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
        body2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        projectileSoundKey = AudioManager.Instance.LoadSound(bulletData.projectileSound, this.transform, 0, true);
        Destroy(this.gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SendMessage("GetDamage", bulletData.damage);
            DestroyProjectile();
        }
        if (other.CompareTag("MapLimit"))
        {
            DestroyProjectile();
        }
    }

    public void DestroyProjectile()
    {
        _collider.enabled = false;
        body2D.constraints = RigidbodyConstraints2D.FreezeAll;
        _animator.SetBool("isDestroying", true);

        Destroy(gameObject,0.3f);
    }
}
