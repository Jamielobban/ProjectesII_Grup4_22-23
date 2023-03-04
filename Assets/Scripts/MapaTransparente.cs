using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapaTransparente : MonoBehaviour
{
    public int layerAbajo;
    public int layerArriba;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.GetChild(0).gameObject.GetComponent<TilemapRenderer>().sortingOrder = layerArriba;

    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            this.gameObject.transform.GetChild(0).gameObject.GetComponent<TilemapRenderer>().sortingOrder = layerAbajo;
            Tilemap color = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Tilemap>();

            color.color = new Color(color.color.r, color.color.g, color.color.b, 0.5f);


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.gameObject.transform.GetChild(0).gameObject.GetComponent<TilemapRenderer>().sortingOrder = layerArriba;
            Tilemap color = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Tilemap>();

            color.color = new Color(color.color.r, color.color.g, color.color.b, 1f);
        }
    }
}
