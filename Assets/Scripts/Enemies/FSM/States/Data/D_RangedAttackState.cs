using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newRangedAttackStateData", menuName = "Data/State Data/Ranged Attack State")]
public class D_RangedAttackState : ScriptableObject
{
    public GameObject bullet;
    public float projectileDamage = 10f;
    public float projectileSpeed = 12f;
    public float projectileTravelDistance;

    //protected float bulletDamage;
    //protected float bulletSpeedMetresPerSec;
    //protected float bulletRangeInMetres;
    //protected float bulletRadius;
    //protected float _damageMultiplier; 
}
