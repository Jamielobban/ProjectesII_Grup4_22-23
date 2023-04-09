using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAttack : MonoBehaviour
{
    public int idTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (idTrigger == 0)
            {
                GetComponentInParent<Enemy15>().firingState.playerInsidepunchArea = true;
            }
            else
            {
                GetComponentInParent<Enemy15>().firingState.playerInsideGasArea = true;
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (idTrigger == 0)
            {
                GetComponentInParent<Enemy15>().firingState.playerInsidepunchArea = false;
            }
            else
            {
                GetComponentInParent<Enemy15>().firingState.playerInsideGasArea = false;
            }
        }
    }
}
