using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dashCooldown : MonoBehaviour
{

    public Slider dashCountdown;

    public void SetMaxDashTimer(float dashCooldown)
    {
        dashCountdown.maxValue = dashCooldown;
        dashCountdown.value = dashCooldown;
    }
    public void SetDashTimer(float dashCooldown)
    {
        dashCountdown.value = dashCooldown;
    }
}