using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//using static UnityEditor.VersionControl.Asset;
//using UnityEditor.U2D.Path;

public class UIShakeTest : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerMovement myPlayer;
    private bool isShaking;
    void Start()
    {
        isShaking = false;
        myPlayer = FindObjectOfType<PlayerMovement>();
        Debug.Log("What is going"); 
     }
    // Update is called once per frame
    private void Update()
    {
        if (myPlayer.knockback)
        {
            if (!isShaking)
            {
                StartCoroutine(waitForUIShake(1.1f));
                this.transform.DOShakePosition(1f, (new Vector3(5f, 5f, 5f)), 50, 45f, false, true, ShakeRandomnessMode.Harmonic);
            }

        }
    }

    private IEnumerator waitForUIShake(float waitTime)
    {
        isShaking = true;
        yield return new WaitForSeconds(waitTime);
        isShaking = false;
    }
}
