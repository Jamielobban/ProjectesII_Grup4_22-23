using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBlockStateData", menuName = "Data/BlockState Data/Base Data")]
public class D_BlockState : ScriptableObject
{
    public GameObject blockParticles;
    public AudioClip blockSound;
}
