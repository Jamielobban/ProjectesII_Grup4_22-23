using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnObjectsInCircle : MonoBehaviour
{
    [HideInInspector]
    public Transform player;
    [HideInInspector]
    public int id;
    float speed;
    public GameObject objectPrefab;   // El prefab de fuego a instanciar
    public int objectPerQuadrant;     // El número de instancias de fuego por cuadrante    
    CircleCollider2D circleCollider;
    List<GameObject> instances = new List<GameObject>();
    float counter = 0.0001f;
    public float spawnDelay = 1/50f;
    int iterations;
    bool damageAplied = false;
    public int objectsBetweenWaits;
    public bool increaseRadius;
    public bool doScaleOnSpawn;
    public bool spawnsDone;
    public bool startWithRandomAngle = false;
    float randomAngle = 0;
    private void OnEnable()
    {
        if (startWithRandomAngle)
            randomAngle = Random.Range(0f, 90f);

        Debug.Log(randomAngle);
    }

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
        float angleStep = 90f / objectPerQuadrant;

        StartCoroutine(SpawnFireInstances(center, radius, position, angleStep));
    }

    IEnumerator SpawnFireInstances(Vector2 center, float radius, Vector2 position, float angleStep)
    {
        // Iterar sobre los cuadrantes
        for (int i = 0; i < 4; i++)
        {
            // Calcular el ángulo inicial del cuadrante actual
            float startAngle = i * 90f ;

            // Iterar sobre el número de instancias por cuadrante
            for (int j = 0; j < objectPerQuadrant; j++)
            {
                // Calcular el ángulo de la instancia actual
                float angle = startAngle + j * angleStep ;

                // Calcular la posición de la instancia actual
                Vector2 spawnPosition = position + new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * radius;

                // Crear la instancia de fuego en la posición calculada
                GameObject instance = Instantiate(objectPrefab, spawnPosition, Quaternion.identity, this.transform);

                // Calcular la rotación de la instancia actual
                float rotationZ = angle - 90f;

                // Aplicar la rotación a la instancia actual
                instance.transform.localRotation = Quaternion.Euler(0, 0, rotationZ);

                // Añadir la instancia a la lista
                instances.Add(instance);

                if (doScaleOnSpawn)
                {
                    instance.transform.localScale = Vector3.zero;
                    instance.transform.DOScale(1, 0.35f);
                }

                if (j % objectsBetweenWaits == 0)
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
        if (!increaseRadius && spawnsDone)
            return;

        spawnsDone = iterations >= objectPerQuadrant * 4;

        if(id == 1)
        {
            speed = 28;
        }
        else
        {
            speed = 17;
        }

        if(increaseRadius)
        circleCollider.radius = counter;

        // Obtener el radio del Collider2D
        float radius = circleCollider.radius * transform.localScale.x;

        // Obtener la posición del objeto en la escena
        Vector2 position = transform.position;

        // Obtener el ángulo de separación para cada instancia
        float angleStep = 90f / objectPerQuadrant;

        // Iterar sobre todas las instancias creadas
        for (int i = 0; i < instances.Count; i++)
        {
            // Calcular el ángulo del cuadrante actual
            int quadrant = Mathf.FloorToInt(instances[i].transform.rotation.eulerAngles.z / 90f);

            // Calcular el ángulo inicial del cuadrante actual
            float startAngle = quadrant * 90f;

            // Calcular el ángulo de la instancia actual
            float angle = startAngle + (i % objectPerQuadrant) * angleStep;

            // Calcular la posición de la instancia actual
            Vector2 spawnPosition = position + new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * radius;

            // Actualizar la posición de la instancia actual
            instances[i].transform.position = spawnPosition;            
        }

        if(iterations >= objectPerQuadrant*4)
            counter += Time.deltaTime * speed;

        Vector3 vectorRadiusPlayer = (player.transform.position - circleCollider.bounds.center).normalized;

        if(increaseRadius && !damageAplied && (player.position - (circleCollider.bounds.center + vectorRadiusPlayer * circleCollider.radius)).magnitude <= 1.75f)
        {
            if (!player.GetComponent<PlayerMovement>().isDashing)
            {
                player.GetComponent<PlayerMovement>().GetDamage(1);
                damageAplied = true;
            }
        }
    }   



}
