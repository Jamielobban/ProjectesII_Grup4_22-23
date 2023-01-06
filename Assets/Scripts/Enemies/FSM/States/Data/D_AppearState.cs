using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAppearStateData", menuName = "Data/AppearState Data/Base Data")]
public class D_AppearState : ScriptableObject
{
    public float timeAppearDuration;
    public float maxDistanceToAppear;
    public AudioClip appearSound;

}
