using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyBulletData", menuName = "Data/EnemyBullet Data/Base Data")]
public class D_EnemyBullet : ScriptableObject
{
    public float speed;
    public float damage;
    public AudioClip projectileSound;
    public AudioClip[] projectileSounds;
}
