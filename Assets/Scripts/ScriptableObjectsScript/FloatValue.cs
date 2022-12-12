using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    float initialValue;

    public float InitialValue { get { return initialValue; } }

    [Header("Change just for ingame testing")]

    public float RuntimeValue;
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
    public void SetInit(float init)
    {
        initialValue = init;
        //RuntimeValue = init;
    }
}
