using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallFunctionAnim : MonoBehaviour
{
    public void CallFunction (string functionName)
    {

        try
        {
            this.SendMessageUpwards("SearchFunction", functionName);
        }
        catch (System.Exception)
        {
            
        } 
    }
}
