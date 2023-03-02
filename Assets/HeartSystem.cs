 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class HeartSystem : MonoBehaviour
{
    public GameObject heartPrefab;
    public PlayerMovement player;
    List<HealthHeart> hearts = new List<HealthHeart>();
    public HealthHeart[] heartArray;
    public HealthHeart heartToChange;

    private void Start()
    {
        DrawHearts();
    }
    public void DrawHearts()
    {

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

        heartArray = GetComponentsInChildren<HealthHeart>().Where(t => (t.GetComponent<HealthHeart>()._status != HeartStatus.Empty)).ToArray();
        heartToChange = heartArray[heartArray.Length - 1];

        if (heartToChange._status == HeartStatus.Full)
        {
            Debug.Log("Full");
            return;
        }
        if (heartToChange._status == HeartStatus.Half)
        {
            Debug.Log("Half");
            return;
        }
        if (heartToChange._status == HeartStatus.Empty)
        {
            Debug.Log("Empty");
            return;
        }
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
        hearts = new List<HealthHeart>();
    }
}
