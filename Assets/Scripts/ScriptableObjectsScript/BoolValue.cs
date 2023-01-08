using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoolValue : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    bool initialValue;

    public bool InitialValue { get { return initialValue; } }


    [Header("Change just for ingame testing")]

    public bool RuntimeValue;
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
