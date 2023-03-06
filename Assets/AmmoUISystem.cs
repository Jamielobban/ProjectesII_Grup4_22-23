using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUISystem : MonoBehaviour
{

    public RightHand rightHand;


    public GameObject ammoPrefab;

    public List<AmmoRifleImage> rifleAmmoArray = new List<AmmoRifleImage>();
    //public HealthHeart[] heartArray;
    //public List<AmmoRifleImage> heartArray = new List<AmmoRifleImage>();
    //public HealthHeart[] emptyHeartArray;
    //public List<AmmoRifleImage> emptyHeartArray = new List<AmmoRifleImage>();

    //public HealthHeart heartToChange;
    //public HealthHeart emptyHeartToFlash;

    // Start is called before the first frame update
    void Start()
    {
        DrawAmmo();
    }


    public void DrawAmmo()
    {
        ClearAmmo();

        //Debug.Log("Update");
        ammoPrefab.GetComponent<AmmoRifleImage>().fullAmmo = rightHand.weaponInHand.GetFullSprite();
        ammoPrefab.GetComponent<AmmoRifleImage>().emptyAmmo = rightHand.weaponInHand.GetEmptySprite();
        ammoPrefab.GetComponent<AmmoRifleImage>().flashAmmo = rightHand.weaponInHand.GetFlashSprite();

        int maxAmmoToMake = rightHand.weaponInHand.GetBulletsPerMagazine();
        int ammoToMake = (int)(rightHand.weaponInHand.GetBulletsInMagazine());

        //float maxHealthRemainder = player.maxHearts % 2;
        //int heartsToMake = (int)((player.maxHearts / 2) + maxHealthRemainder);
        if (!rightHand.weaponInHand.GetReloadingState())
        {
            for (int i = 0; i < maxAmmoToMake; i++)
            {
                CreateEmptyAmmo();
            }
            for (int i = 0; i < rifleAmmoArray.Count; i++)
            {
                int ammoRemainder = (int)(Mathf.Clamp(rightHand.weaponInHand.GetBulletsInMagazine() - (i * 1), 0, 1));
                rifleAmmoArray[i].SetAmmoImage((AmmoRifleImage.AmmoStatus)ammoRemainder);
            }
        }
        else
        {
            for (int i = 0; i < maxAmmoToMake; i++)
            {
                CreateEmptyAmmo();
            }
        }
    }

    public IEnumerator ReloadAmmo(float time)
    {
        for (int i = 0; i < rightHand.weaponInHand.GetBulletsInMagazine(); i++)
        {
            rifleAmmoArray[0].SetAmmoImage(AmmoRifleImage.AmmoStatus.Full);
            rifleAmmoArray[i].SetAmmoImage(AmmoRifleImage.AmmoStatus.Full);
            yield return new WaitForSeconds(time / rightHand.weaponInHand.GetBulletsInMagazine());
        }
    }


    public void CreateEmptyAmmo()
    {
        GameObject newAmmo = Instantiate(ammoPrefab);
        newAmmo.transform.SetParent(transform);

        AmmoRifleImage ammoComponent = newAmmo.GetComponent<AmmoRifleImage>();
        ammoComponent.SetAmmoImage(AmmoRifleImage.AmmoStatus.Empty);
        rifleAmmoArray.Add(ammoComponent);
    }

    public void ClearAmmo()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        rifleAmmoArray = new List<AmmoRifleImage>();
    }

    public void AmmoTest()
    {
        Debug.Log("HAOSDPOKAS");
    }
}