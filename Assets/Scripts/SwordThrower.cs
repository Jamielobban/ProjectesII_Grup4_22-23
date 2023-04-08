using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SwordThrower : MonoBehaviour
{
    public float interval = 0.05f; // tiempo en segundos para recoger espadas
    private float timer = 0f; // temporizador para contar el tiempo transcurrido
    public Transform player;
    public bool finished = false;

    private void Update()
    {
        timer += Time.deltaTime; // aumenta el temporizador por el tiempo transcurrido

        if (timer >= interval)
        {
            timer = 0f; // reinicia el temporizador
            ThrowSwords();
        }
    }

    private void ThrowSwords()
    {
        // Obtén todos los hijos del objeto actual
        Transform[] children = GetComponentsInChildren<Rigidbody2D>().Select(rb => rb.transform).ToArray();

        if (children.Length <= 0)
        {
            finished = true;
            return;
        }

        // Selecciona el primer y último hijo
        Transform child1 = children[Random.Range(0, children.Length)];
        Transform child2;
        do { child2 = children[Random.Range(0, children.Length)]; } while (child2 == child1);


        // Desasocia los hijos del padre actual
        child1.parent = null;
        child2.parent = null;

        child1.gameObject.AddComponent<SwordMovement>();
        child1.GetComponent<SwordMovement>().player = player;
        child2.gameObject.AddComponent<SwordMovement>();
        child2.GetComponent<SwordMovement>().player = player;



    }
}
