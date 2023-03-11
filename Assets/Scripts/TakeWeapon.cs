using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeWeapon : MonoBehaviour
{
    [SerializeField]
    string weapoName;

    bool took = false;

    private void Start()
    {
        if(PlayerPrefs.GetInt(weapoName + "Desbloqueada") == 1)
        {
            this.gameObject.SetActive(false);

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !took)
        {            
            collision.GetComponentInChildren<RightHand>().EquipWeapon(weapoName);
            Destroy(this.transform.parent.gameObject);
        }
    }
}
