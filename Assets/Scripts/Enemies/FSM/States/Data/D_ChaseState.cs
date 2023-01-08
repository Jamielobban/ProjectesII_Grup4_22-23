using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newChaseStateData", menuName = "Data/ChaseState Data/Base Data")]
public class D_ChaseState : ScriptableObject
{
    public AudioClip followSounds;
    public AudioClip Stomp1;
    public AudioClip Stomp2;
}
