using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AmmoScriptColider : MonoBehaviour
{
    [SerializeField]
    GameObject ammoParent;

    string parentWeaponName;

    private void Start()
    {
        parentWeaponName = ammoParent.GetComponent<AmmoScript>().weaponAmmoName;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            WeaponValues aux = ammoParent.GetComponent<AmmoScript>().weaponGenerator.GetComponent<WeaponGenerator>().weaponsValues.Where(wv => wv.WeaponName == parentWeaponName).ToArray()[0];
            aux.magazinesInWeapon.RuntimeValue += 1;
            if (collision.GetComponentInChildren<RightHand>().weaponInHand.GetWeaponName() == parentWeaponName)
            {
                collision.GetComponentInChildren<RightHand>().UpdateUIWeapons();
            }
            Destroy(ammoParent.gameObject);
            //collision.GetComponentInChildren<RightHand>().draw
        }
    }
}