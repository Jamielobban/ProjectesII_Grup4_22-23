using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntValue : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    int initialValue;

    public int InitialValue { get { return initialValue; } }


    [Header("Change just for ingame testing")]

    public int RuntimeValue;
    public void OnAfterDeserialize()
    {
        RuntimeValue = initialValue;
    }
    public void OnBeforeSerialize()
    {
    }

    public void RestartValues()
    {
        RuntimeValue = initialValue;
    }
}
