using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePool : MonoBehaviour
{
    public GameObject firePrefab;
    List<GameObject> instances = new List<GameObject>();
    public int firePerQuadrant;
    public float spawnDelay = 1 / 50f;

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnFireInstances(Vector2 center, float radius, Vector2 position, float angleStep)
    {
        // Iterar sobre los cuadrantes
        for (int i = 0; i < 4; i++)
        {
            // Calcular el �ngulo inicial del cuadrante actual
            float startAngle = i * 90f;

            // Iterar sobre el n�mero de instancias por cuadrante
            for (int j = 0; j < firePerQuadrant; j++)
            {
                // Calcular el �ngulo de la instancia actual
                float angle = startAngle + j * angleStep;

                // Calcular la posici�n de la instancia actual
                Vector2 spawnPosition = position + new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * radius;

                // Crear la instancia de fuego en la posici�n calculada
                GameObject instance = Instantiate(firePrefab, spawnPosition, Quaternion.identity, this.transform);

                // Calcular la rotaci�n de la instancia actual
                float rotationZ = angle - 90f;

                // Aplicar la rotaci�n a la instancia actual
                instance.transform.localRotation = Quaternion.Euler(0, 0, rotationZ);

                // A�adir la instancia a la lista
                instances.Add(instance);

                if (j % 2 == 0)
                {
                    // Esperar un tiempo antes de crear la siguiente instancia
                    yield return new WaitForSeconds(spawnDelay);
                }

            }
        }
    }

}
