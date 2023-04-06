using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFire : MonoBehaviour
{
    [HideInInspector]
    public Transform player;
    [HideInInspector]
    public int id;
    float speed;
    public GameObject firePrefab;   // El prefab de fuego a instanciar
    public int firePerQuadrant;     // El número de instancias de fuego por cuadrante    
    CircleCollider2D circleCollider;
    List<GameObject> instances = new List<GameObject>();
    float counter = 0.0001f;
    public float spawnDelay = 1/50f;
    int iterations;
    bool damageAplied = false;

    void Start()
    {
        // Obtener el Collider2D circular del objeto
        circleCollider = GetComponent<CircleCollider2D>();

        // Obtener el centro del Collider2D
        Vector2 center = circleCollider.offset;

        // Obtener el radio del Collider2D
        float radius = circleCollider.radius * transform.localScale.x;

        // Obtener la posición del objeto en la escena
        Vector2 position = transform.position;

        // Obtener el ángulo de separación para cada instancia
        float angleStep = 90f / firePerQuadrant;

        StartCoroutine(SpawnFireInstances(center, radius, position, angleStep));
    }

    IEnumerator SpawnFireInstances(Vector2 center, float radius, Vector2 position, float angleStep)
    {
        // Iterar sobre los cuadrantes
        for (int i = 0; i < 4; i++)
        {
            // Calcular el ángulo inicial del cuadrante actual
            float startAngle = i * 90f;

            // Iterar sobre el número de instancias por cuadrante
            for (int j = 0; j < firePerQuadrant; j++)
            {
                // Calcular el ángulo de la instancia actual
                float angle = startAngle + j * angleStep;

                // Calcular la posición de la instancia actual
                Vector2 spawnPosition = position + new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * radius;

                // Crear la instancia de fuego en la posición calculada
                GameObject instance = Instantiate(firePrefab, spawnPosition, Quaternion.identity, this.transform);

                // Calcular la rotación de la instancia actual
                float rotationZ = angle - 90f;

                // Aplicar la rotación a la instancia actual
                instance.transform.localRotation = Quaternion.Euler(0, 0, rotationZ);

                // Añadir la instancia a la lista
                instances.Add(instance);

                if (j % 6 == 0)
                {
                    // Esperar un tiempo antes de crear la siguiente instancia
                    yield return new WaitForSeconds(spawnDelay);
                }

                iterations++;

            }
        }
    }

    void Update()
    {
        if(id == 1)
        {
            speed = 28;
        }
        else
        {
            speed = 17;
        }

        circleCollider.radius = counter;

        // Obtener el radio del Collider2D
        float radius = circleCollider.radius * transform.localScale.x;

        // Obtener la posición del objeto en la escena
        Vector2 position = transform.position;

        // Obtener el ángulo de separación para cada instancia
        float angleStep = 90f / firePerQuadrant;

        // Iterar sobre todas las instancias creadas
        for (int i = 0; i < instances.Count; i++)
        {
            // Calcular el ángulo del cuadrante actual
            int quadrant = Mathf.FloorToInt(instances[i].transform.rotation.eulerAngles.z / 90f);

            // Calcular el ángulo inicial del cuadrante actual
            float startAngle = quadrant * 90f;

            // Calcular el ángulo de la instancia actual
            float angle = startAngle + (i % firePerQuadrant) * angleStep;

            // Calcular la posición de la instancia actual
            Vector2 spawnPosition = position + new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * radius;

            // Actualizar la posición de la instancia actual
            instances[i].transform.position = spawnPosition;            
        }

        if(iterations >= firePerQuadrant*4)
            counter += Time.deltaTime * speed;

        Vector3 vectorRadiusPlayer = (player.transform.position - circleCollider.bounds.center).normalized;

        if(!damageAplied && (player.position - (circleCollider.bounds.center + vectorRadiusPlayer * circleCollider.radius)).magnitude <= 1.75f)
        {
            if (!player.GetComponent<PlayerMovement>().isDashing)
            {
                player.GetComponent<PlayerMovement>().GetDamage(1);
                damageAplied = true;
            }
        }
    }


    //void Start()
    //{
    //    // Obtener el Collider2D circular del objeto
    //    circleCollider = GetComponent<CircleCollider2D>();

    //    // Obtener el centro del Collider2D
    //    Vector2 center = circleCollider.offset;

    //    // Obtener el radio del Collider2D
    //    float radius = circleCollider.radius * transform.localScale.x;

    //    // Obtener la posición del objeto en la escena
    //    Vector2 position = transform.position;

    //    // Obtener el ángulo de separación para cada instancia
    //    float angleStep = 90f / firePerQuadrant;

    //    // Lista para almacenar las instancias creadas
    //    List<GameObject> instances = new List<GameObject>();

    //    // Iterar sobre los cuadrantes
    //    for (int i = 0; i < 4; i++)
    //    {
    //        // Calcular el ángulo inicial del cuadrante actual
    //        float startAngle = i * 90f;

    //        // Iterar sobre el número de instancias por cuadrante
    //        for (int j = 0; j < firePerQuadrant; j++)
    //        {
    //            // Calcular el ángulo de la instancia actual
    //            float angle = startAngle + j * angleStep;

    //            // Calcular la posición de la instancia actual
    //            Vector2 spawnPosition = position + new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * radius;

    //            // Crear la instancia de fuego en la posición calculada
    //            GameObject instance = Instantiate(firePrefab, spawnPosition, Quaternion.identity, this.transform);

    //            // Calcular la rotación de la instancia actual
    //            float rotationZ = angle - 90f;

    //            // Aplicar la rotación a la instancia actual
    //            instance.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

    //            // Añadir la instancia a la lista
    //            instances.Add(instance);
    //        }
    //    }
    //}



}
