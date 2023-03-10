 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using System;

public class HeartSystem : MonoBehaviour
{
    public GameObject heartPrefab;
    public PlayerMovement player;
    public List<HealthHeart> hearts = new List<HealthHeart>();
    //public HealthHeart[] heartArray;
    public List<HealthHeart> heartArray = new List<HealthHeart>();
    //public HealthHeart[] emptyHeartArray;
    public List<HealthHeart> emptyHeartArray = new List<HealthHeart>();
    public HealthHeart heartToChange;
    public HealthHeart emptyHeartToFlash;

    bool isHalf;


    //Setup status to be
   


    private void Start()
    {
        DrawHearts();
    }
    public void DrawHearts()
    {
        //Debug.Log("draw");
        ClearHearts();

        float maxHealthRemainder = player.maxHearts % 2;
        int heartsToMake = (int)((player.maxHearts / 2) + maxHealthRemainder);
        for (int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }
        for (int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRemainder = (int)(Mathf.Clamp(player.currentHearts - (i * 2), 0, 2));
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }

        //heartArray = GetComponentsInChildren<HealthHeart>().Where(t => (t.GetComponent<HealthHeart>()._status != HeartStatus.Empty)).ToList();
        //emptyHeartArray = GetComponentsInChildren<HealthHeart>().Where(t => (t.GetComponent<HealthHeart>()._status == HeartStatus.Empty)).ToList();


        for (int i = 0; i < hearts.Count; i++)
        {
            if (hearts[i]._status != HeartStatus.Empty)
            {
                heartArray.Add(hearts[i]);
            }
            if ((player.maxHearts - player.currentHearts) <= 2)
            {
                emptyHeartToFlash = heartArray[heartArray.Count - 1];
            }
            if (hearts[i]._status == HeartStatus.Empty)
            {

                emptyHeartArray.Add(hearts[i]);
                emptyHeartToFlash = emptyHeartArray[0];
            }
        }

        heartToChange = heartArray[heartArray.Count - 1];

        Debug.Log(heartToChange.transform.position);
    }

    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }
    public void ClearHearts()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        heartArray = new List<HealthHeart>();
        emptyHeartArray = new List<HealthHeart>();
        hearts = new List<HealthHeart>();

    }

    public void DrawAllEmpty()
    {
        foreach (HealthHeart heart in heartArray)
        {
            heart.SetHeartImage(HeartStatus.Empty);
        }
    }


    public void ShakeHeart(HealthHeart heart)
    {
        heart.transform.DOJump(heart.transform.position, 10, 1, 0.3f, false);
        Debug.Log(heart.transform.position);

    }
}
