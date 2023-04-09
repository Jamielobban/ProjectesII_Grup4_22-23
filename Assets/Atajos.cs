using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Atajos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            //Boss final
            PlayerPrefs.SetInt("Lado", 0);

            SceneManager.LoadScene("SalaBase");

        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            //Boss acido
            PlayerPrefs.SetInt("Lado", 0);

            SceneManager.LoadScene("SalaBossAcido");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            //Boss sangre
            PlayerPrefs.SetInt("Lado", 0);

            SceneManager.LoadScene("SalaSangre 17");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            //Mazmorra sangre
            PlayerPrefs.SetInt("Lado", 1);

            SceneManager.LoadScene("SalaSangre");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            //Mazmorra acido
            PlayerPrefs.SetInt("Lado", 3);

            SceneManager.LoadScene("SalaCheckpoints 2");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            RightHand generator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<RightHand>();

            if (generator != null)
            {
                generator.EquipWeaponModoInfinito("Sniper");
                generator.EquipWeaponModoInfinito("Metralleta");
                generator.EquipWeaponModoInfinito("Shotgun");
                generator.EquipWeaponModoInfinito("Pistol");
            }

        }

    }
}