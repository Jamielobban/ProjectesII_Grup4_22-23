using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyKillsInTime : ChallengeController
{
    protected int numberEnemiesToKill;
    int numberKills; 
    int numberKillsSaved;
    int lastManagerId;
    float startTime;
    List<GameObject> roomManagers = new List<GameObject>();
    GameObject roomManager;

    public EnemyKillsInTime()
    {
        challenge.challengeText = "Kill  enemies in a maximum of  seconds";        
    }

    public override void Start()
    {
        startTime = Time.time;   
    }

    public override bool Update()
    {
        //Debug.Log(numberKills);
        //AddToList(GameObject.FindGameObjectsWithTag("RoomManager"));
        //roomManagers = (List<GameObject>)GameObject.FindGameObjectsWithTag("RoomManager").Where(rm => rm.GetComponent<RoomManager>().currentRoom);
        ////Borrar els managers de ssales no actuals
        //for (int i = 0; i < roomManagers.Count; i++)
        //{
        //    if (!roomManagers[i].GetComponent<RoomManager>().currentRoom)
        //    {
        //        roomManagers.RemoveAt(i);               
        //    }            
        //}

        //Mirem si el player trepitja més duna sala a la vegada
        //if(roomManagers.Count == 1)
        //{
        //    //Si no es el cas guardem les kills de la sala actual
        //    lastManagerId = roomManagers[0].GetInstanceID();
        //    if (roomManagers[0].GetComponent<RoomManager>().kills >= numberKills)
        //    {
        //        numberKills = roomManagers[0].GetComponent<RoomManager>().kills;

        //    }
        //    else
        //    {
        //        numberKillsSaved += numberKills;
        //        numberKills = roomManagers[0].GetComponent<RoomManager>().kills;
        //    }
        //}
        //else
        //{
        //    //Si es el cas ens quedem només amb la mes nova (si esta a més de 2 a la vegada no funcionara)
        //    for(int i = 0; i < roomManagers.Count; i++)
        //    {
        //        if (roomManagers[i].GetInstanceID() == lastManagerId)
        //        {
        //            roomManagers.RemoveAt(i);
        //        }
        //    }
        //}

        //roomManagers.Clear();
        roomManager = GameObject.FindGameObjectWithTag("RoomManager");


        if (roomManager.GetComponent<RoomManager>().kills >= numberKills)
        {
            numberKills = roomManager.GetComponent<RoomManager>().kills;
        }
        else
        {
            numberKillsSaved += numberKills;
            numberKills = roomManager.GetComponent<RoomManager>().kills;
        }

        //Si sacaba el temps tornem true pq ja hem acabat i achived a false pq no ho hem aconseguit
        if (Time.time - startTime >= challenge.durationChallengeTime)
        {
            achived = false;
            return true;
        }
        else if(numberKillsSaved + numberKills >= numberEnemiesToKill) 
        {
            //Si matem el limit necessari tornem true pq hem acabat i achived true pq ho hem aconseguit

            achived = true;
            return true;
        }

        //Si no sha acabat tornem false
        return false;
    }

    protected void SetTextTimeAndNumberenemies()
    {
        challenge.challengeText = challenge.challengeText.Insert(30, challenge.durationChallengeTime.ToString());
        challenge.challengeText = challenge.challengeText.Insert(5, numberEnemiesToKill.ToString());

        //Debug.Log(challenge.challengeText);
    }

    // Function with a variable number of parameters
    void AddToList(params GameObject[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            roomManagers.Add(list[i]);
        }
    }
}
