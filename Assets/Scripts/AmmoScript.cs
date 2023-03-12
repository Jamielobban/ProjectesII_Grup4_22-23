using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour
{
    [SerializeField]
    GameObject colider;
    [SerializeField]
    SpriteRenderer sr;

    bool hasAmmo = false;
    bool setupDone = false;
    [HideInInspector]
    public string weaponAmmoName;
    [HideInInspector]
    public Sprite weaponAmmoSprite;
    [HideInInspector]
    public GameObject weaponGenerator;

    private void Start()
    {
        float a = Random.Range(0, 100);
        if(a >= 49)
        {
            hasAmmo = true;
        }

    }
    private void Update()
    {
        if (!hasAmmo)
        {
            GameObject.Destroy(this.gameObject);
        }
        else if(!setupDone)
        {
            weaponGenerator = GameObject.FindGameObjectWithTag("WeaponGenerator");
            int listSize = weaponGenerator.GetComponent<WeaponGenerator>().weaponIndexOrder.Count;
            int indexInValues = weaponGenerator.GetComponent<WeaponGenerator>().weaponIndexOrder[Random.Range(0, listSize)];
            weaponAmmoName = weaponGenerator.GetComponent<WeaponGenerator>().weaponsValues[indexInValues].WeaponName;
            weaponAmmoSprite = weaponGenerator.GetComponent<WeaponGenerator>().weaponsValues[indexInValues].weaponSprite;
            sr.sprite = weaponAmmoSprite;
            colider.SetActive(true);
            setupDone = true;
        }
    }



}
