using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnMapColision : MonoBehaviour
{
     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MapLimit"))
        {
            // Si el objeto que colisiona tiene el tag "MapLimit", destruye este objeto
            Destroy(this.gameObject);
        }
    }
}
