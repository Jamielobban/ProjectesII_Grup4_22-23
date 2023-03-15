using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSlashStateData", menuName = "Data/SlashState Data/Base Data")]
public class D_SlashState : ScriptableObject
{
    public float timeBetwenSlashes;
    public AudioClip slashSound;
    public AudioClip hitSound;
}
