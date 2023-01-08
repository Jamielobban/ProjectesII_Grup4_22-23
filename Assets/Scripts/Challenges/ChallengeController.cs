using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChallengeController
{
    public Challenge challenge;
    protected bool achived;

    public ChallengeController()
    {

    }

    public abstract void Start();
    public abstract bool Update();

    public bool GetIfAchived() { return achived; }    

        
}
