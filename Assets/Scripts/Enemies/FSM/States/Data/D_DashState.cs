using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDashStateData", menuName = "Data/DashState Data/Base Data")]
public class D_DashState : ScriptableObject
{
    public AudioClip dashSound;
    public GameObject fireExplosionPrefab;
}
