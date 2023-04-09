using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModoInfinito : MonoBehaviour
{
    RightHand generator;
    public Transform[] spawnPoints;
    public GameObject[] spawns;
    float time;
    public int tiempoAguantado;
    float tiempoInicial;

    bool dificultad1;
    bool dificultad2;
    float spawnTime;

    [SerializeField]
    TMP_Text RecordTimer;

    [SerializeField]
    TMP_Text CurrentTimer;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = 1.5f;
        dificultad1 = false;
        dificultad2 = false;

        tiempoInicial = Time.time;
        time = Time.time;
        generator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<RightHand>();
        Invoke("unlock", 0.2f);

        RecordTimer.SetText(PlayerPrefs.GetInt("Infinito", 0).ToString());
        CurrentTimer.SetText(0.ToString());

    }
    void unlock()
    {
        generator.EquipWeaponModoInfinito("Sniper");
        generator.EquipWeaponModoInfinito("Metralleta");
        generator.EquipWeaponModoInfinito("Shotgun");
        generator.EquipWeaponModoInfinito("Pistol");

    }

    // Update is called once per frame
    void Update()
    {

        tiempoAguantado = (int)(Time.time -tiempoInicial);   
        CurrentTimer.SetText(tiempoAguantado.ToString());

        if(PlayerPrefs.GetInt("Infinito", 0) < tiempoAguantado)
        {
            //
        }
        if(!dificultad1&& tiempoAguantado > 60)
        {
            dificultad1 = true;
            spawnTime = 1;
        }
        if (!dificultad2 && tiempoAguantado > 120)
        {
            dificultad2 = true;
            spawnTime = 0.5f;
        }

        if (time + spawnTime < Time.time)
        {
            time = Time.time;

            int enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (enemies < 10)
            {


                int random1 = (int)Random.Range(0, spawnPoints.Length - 0.01f);
                int random2 = (int)Random.Range(0, 60);

                if (random2 < 20)
                {
                    //normales
                    int variantes = (int)Random.Range(0, 1);

                    Instantiate(spawns[variantes], spawnPoints[random1].position, spawnPoints[random1].rotation);


                }
                else if (random2 >= 20 && random2 < 25)
                {
                    //escopeta
                    int variantes = 2 + (int)Random.Range(0, 1);
                    Instantiate(spawns[variantes], spawnPoints[random1].position, spawnPoints[random1].rotation);


                }
                else if (random2 >= 25 && random2 < 35)
                {
                    //snipers
                    int variantes = 5 + (int)Random.Range(0, 1);
                    Instantiate(spawns[variantes], spawnPoints[random1].position, spawnPoints[random1].rotation);


                }
                else if (random2 >= 35 && random2 < 40)
                {
                    //torretas
                    int variantes = 7 + (int)Random.Range(0, 2);
                    Instantiate(spawns[variantes], spawnPoints[random1].position, spawnPoints[random1].rotation);


                }
                else if (random2 >= 40 && random2 < 45)
                {
                    //assasin
                    int variantes = 9 + (int)Random.Range(0, 1);
                    Instantiate(spawns[variantes], spawnPoints[random1].position, spawnPoints[random1].rotation);


                }
                else if (random2 >= 45 && random2 < 50)
                {
                    //fuego
                    int variantes = 11 + (int)Random.Range(0, 2);
                    Instantiate(spawns[variantes], spawnPoints[random1].position, spawnPoints[random1].rotation);


                }
                else if (random2 >= 50 && random2 < 55)
                {
                    //acido
                    int variantes = 14 + (int)Random.Range(0, 1);
                    Instantiate(spawns[variantes], spawnPoints[random1].position, spawnPoints[random1].rotation);


                }
                else if (random2 >= 55 && random2 <= 60)
                {
                    //otros
                    int variantes = 16 + (int)Random.Range(0, 1);
                    Instantiate(spawns[variantes], spawnPoints[random1].position, spawnPoints[random1].rotation);


                }
            }

            //PlayerPrefs.GetInt("Infinito",0);
            //tiempoAguantado
        }
    }
}
