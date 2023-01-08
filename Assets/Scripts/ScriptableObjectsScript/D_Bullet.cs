using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBulletData", menuName = "Data/Bullet Data/Base Data")]
public class D_Bullet : ScriptableObject
{
    public float minRangeTransform, maxRangeTransform;    
    public float minRangeVelocity, maxRangeVeclocity;    
    public float bulletDamage;
    public float bulletSpeedMetresPerSec;
    public float bulletRangeInMetres;
    public float bulletRadius;
    public float _damageMultiplier;
    public bool ApplyShootForce;
}
