using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newIdleStateData", menuName = "Data/IdleState Data/Base Data")]
public class D_IdleState : ScriptableObject
{
    public float rangeToPassToChasing;
    public float timeBetweenFlips;
    public float timeToNextFlips;
    public AudioClip idleSounds;
}
