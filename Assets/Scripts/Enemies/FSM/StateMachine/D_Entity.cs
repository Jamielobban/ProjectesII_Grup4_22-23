using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyBaseData", menuName = "Data/Enemy Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public float stopDistanceFromPlayer;
    public float speed;
    public FloatValue enemyHealth;
    public GameObject hitParticles;
    public AudioClip hitSound;
    [HideInInspector]
    public Transform firePoint;    
}
