

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AmmoScriptColider : MonoBehaviour
{
    [SerializeField]
    GameObject ammoParent;

    string parentWeaponName;

    public bool recoger;
    private void Start()
    {
        recoger = false;
        parentWeaponName = ammoParent.GetComponent<AmmoScript>().weaponAmmoName;
    }

    public void recogerAmmo()
    {
        WeaponValues aux = ammoParent.GetComponent<AmmoScript>().weaponGenerator.GetComponent<WeaponGenerator>().weaponsValues.Where(wv => wv.WeaponName == parentWeaponName).ToArray()[0];
        if (aux.magazinesInWeapon.RuntimeValue == 0 && aux.bulletsInMagazine.RuntimeValue == 0)
        {
            aux.bulletsInMagazine.RuntimeValue = aux.bulletsInMagazine.InitialValue;
            aux.outOfAmmo.RuntimeValue = false;
        }
        else
        {
            aux.magazinesInWeapon.RuntimeValue += 1;
        }
        if (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<RightHand>().weaponInHand.GetWeaponName() == parentWeaponName)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<RightHand>().UpdateUIWeapons();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !recoger)
        {
            WeaponValues aux = ammoParent.GetComponent<AmmoScript>().weaponGenerator.GetComponent<WeaponGenerator>().weaponsValues.Where(wv => wv.WeaponName == parentWeaponName).ToArray()[0];
            if (aux.magazinesInWeapon.RuntimeValue == 0 && aux.bulletsInMagazine.RuntimeValue == 0)
            {
                aux.bulletsInMagazine.RuntimeValue = aux.bulletsInMagazine.InitialValue;
                aux.outOfAmmo.RuntimeValue = false;
            }
            else
            {
                aux.magazinesInWeapon.RuntimeValue += 1;
            }
            if (collision.GetComponentInChildren<RightHand>().weaponInHand.GetWeaponName() == parentWeaponName)
            {
                collision.GetComponentInChildren<RightHand>().UpdateUIWeapons();
            }
            Destroy(ammoParent.gameObject);
            //collision.GetComponentInChildren<RightHand>().draw
        }
        else if(collision.CompareTag("GanchoRecoger"))
        {
            recoger = true;

            this.transform.parent.transform.SetParent(collision.transform);
        }
    }
}

