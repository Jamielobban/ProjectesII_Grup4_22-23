using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModoInfinito : MonoBehaviour
{
    RightHand generator;
    public Transform[] spawnPoints;
    public GameObject[] spawns;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = Time.time;
        generator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<RightHand>();
        Invoke("unlock", 0.2f);
    }
    void unlock()
    {
        generator.EquipWeapon("Sniper");
        generator.EquipWeapon("metralleta");
        generator.EquipWeapon("Shotgun");
        generator.EquipWeapon("Pistol");

    }

    // Update is called once per frame
    void Update()
    {
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
                else if (random2 >= 20 && random2 < 30)
                {
                    //escopeta
                    int variantes = 2 + (int)Random.Range(0, 2);
                    Instantiate(spawns[variantes], spawnPoints[random1].position, spawnPoints[random1].rotation);


                }
                else if (random2 >= 30 && random2 < 40)
                {
                    //snipers
                    int variantes = 5 + (int)Random.Range(0, 1);
                    Instantiate(spawns[variantes], spawnPoints[random1].position, spawnPoints[random1].rotation);


                }
                else if (random2 >= 40 && random2 < 50)
                {
                    //torretas
                    int variantes = 7 + (int)Random.Range(0, 1);
                    Instantiate(spawns[variantes], spawnPoints[random1].position, spawnPoints[random1].rotation);


                }
                else if (random2 >= 50 && random2 < 55)
                {
                    //assasin
                    int variantes = 9 + (int)Random.Range(0, 1);
                    Instantiate(spawns[variantes], spawnPoints[random1].position, spawnPoints[random1].rotation);


                }
                else if (random2 >= 55 && random2 < 60)
                {
                    //fuego
                    int variantes = 11 + (int)Random.Range(0, 2);
                    Instantiate(spawns[variantes], spawnPoints[random1].position, spawnPoints[random1].rotation);


                }
                else if (random2 >= 60 && random2 < 65)
                {
                    //acido
                    int variantes = 14 + (int)Random.Range(0, 1);
                    Instantiate(spawns[variantes], spawnPoints[random1].position, spawnPoints[random1].rotation);


                }
                else if (random2 >= 65 && random2 <= 75)
                {
                    //otros
                    int variantes = 16 + (int)Random.Range(0, 1);
                    Instantiate(spawns[variantes], spawnPoints[random1].position, spawnPoints[random1].rotation);


                }
            }


        }
    }
}
