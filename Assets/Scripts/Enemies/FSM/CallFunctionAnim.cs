using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallFunctionAnim : MonoBehaviour
{
    public void CallFunction (string functionName)
    {
        this.SendMessageUpwards("SearchFunction",functionName);
    }
}
