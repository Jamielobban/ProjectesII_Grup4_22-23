using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoolTrue : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Animator parentAnim;

    public void SetTrue()
    {
        parentAnim.SetBool("isClosed", true);
    }
}
