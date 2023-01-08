using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDeadStateData", menuName = "Data/DeadState Data/Base Data")]
public class D_DeadState : ScriptableObject
{
    public GameObject deadParticles;
    public AudioClip deadSound;
    public GameObject hearth;
}
