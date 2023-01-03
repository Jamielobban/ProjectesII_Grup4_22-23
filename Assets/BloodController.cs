using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodController : MonoBehaviour
{
    // Start is called before the first frame update

    private PlayerMovement playerpos;
    private CompositeCollider2D bloodcollider;
     
    void Start()
    {
        playerpos = FindObjectOfType<PlayerMovement>();
        bloodcollider = GetComponent<CompositeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerpos.moveSpeed = 5000;
            playerpos.rollSpeed = 65f;
            bloodcollider.geometryType = CompositeCollider2D.GeometryType.Polygons;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerpos.moveSpeed = 10000;
            playerpos.rollSpeed = 90f;
            bloodcollider.geometryType = CompositeCollider2D.GeometryType.Outlines;
        }
    }
}
