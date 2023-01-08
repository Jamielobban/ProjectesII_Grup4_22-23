using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newFiringStateData", menuName = "Data/FiringState Data/Base Data")]
public class D_FiringState : ScriptableObject
{
    public int angleOfCone;
    public int numberOfBursts;
    public float timeBetweenShoots;
    public GameObject bulletType;
    public AudioClip shootShound;
    //public AudioClip shootShound2;
    public AudioClip attackSounds;
    public float stateDuration;
}
