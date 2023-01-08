using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BulletHitInfo
{
    public Vector3 impactPosition;
    public float damage;
    public HealthStateTypes targetState;
    public bool bloodInFloor;
    public bool bloodParticles;
}
