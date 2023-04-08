using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModoInfinito : MonoBehaviour
{
    RightHand generator;
    public Transform[] spawnPoints;
    public GameObject[] spawns;
    float time;
    public int tiempoAguantado;
    float tiempoInicial;

    // Start is called before the first frame update
    void Start()
    {
        tiempoInicial = Time.time;
        time = Time.time;
        generator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<RightHand>();
        Invoke("unlock", 0.2f);
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
        if (time + 0.5f < Time.time)
        {
            time = Time.time;

            int enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (enemies < 15)
            {


                int random1 = (int)Random.Range(0, spawnPoints.Length - 0.01f);
                int random2 = (int)Random.Range(0, 75);

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


        }
    }
}
