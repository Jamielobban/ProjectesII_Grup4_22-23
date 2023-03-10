using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class AmmoUISystem : MonoBehaviour
{

    public RightHand rightHand;


    public GameObject ammoPrefab;

    public List<AmmoRifleImage> rifleAmmoArray = new List<AmmoRifleImage>();




    public TMP_Text ammoCounter;
    public TMP_Text magazineCounter;
    //public HealthHeart[] heartArray;
    //public List<AmmoRifleImage> heartArray = new List<AmmoRifleImage>();
    //public HealthHeart[] emptyHeartArray;
    //public List<AmmoRifleImage> emptyHeartArray = new List<AmmoRifleImage>();

    //public HealthHeart heartToChange;
    //public HealthHeart emptyHeartToFlash;

    // Start is called before the first frame update
    void Start()
    {        
        if(rightHand.weaponInHand != null)
        {
            DrawAmmo();
        }

    }
    

    public void DrawAmmo()
    {
        ammoPrefab.GetComponent<AmmoRifleImage>().fullAmmo = rightHand.weaponInHand.GetFullSprite();
        ammoPrefab.GetComponent<AmmoRifleImage>().emptyAmmo = rightHand.weaponInHand.GetEmptySprite();
        ammoPrefab.GetComponent<AmmoRifleImage>().flashAmmo = rightHand.weaponInHand.GetFlashSprite();
        ammoCounter.text = rightHand.weaponInHand.GetBulletsInMagazine().ToString();
        magazineCounter.text = rightHand.weaponInHand.GetCurrentMagazines().ToString();
        ClearAmmo();

        //Debug.Log("Update");

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
        //rifleAmmoArray[0].SetAmmoImage(AmmoRifleImage.AmmoStatus.Empty);
        //Debug.Log(rifleAmmoArray[0]);
        float timer = (time / (rightHand.weaponInHand.GetBulletsPerMagazine() + 1f));
        rifleAmmoArray[0].SetAmmoImage(AmmoRifleImage.AmmoStatus.Empty);
        ammoCounter.text = 0.ToString();
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < rightHand.weaponInHand.GetBulletsPerMagazine(); i++)
        {
            rifleAmmoArray[i].SetAmmoImage(AmmoRifleImage.AmmoStatus.Full);
            ammoCounter.text = (i + 1).ToString();
            yield return new WaitForSeconds(time / (rightHand.weaponInHand.GetBulletsPerMagazine() + 1.25f));
        }

        for (int i = rifleAmmoArray.Count - 1; i >= 0; i--)
        {
            rifleAmmoArray[i].transform.DOJump(rifleAmmoArray[i].transform.position, 10, 1, 0.3f, false);
            yield return new WaitForSeconds(1f / (rightHand.weaponInHand.GetBulletsPerMagazine()) - 0.05f);
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
