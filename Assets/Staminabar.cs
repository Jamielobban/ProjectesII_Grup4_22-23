using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Staminabar : MonoBehaviour
{

    public Slider slider;



    public void SetMaxStamina(float maxStam)
    {
        slider.maxValue = maxStam;
        slider.value = maxStam;
    }
    public void SetStamina(float stamina)
    {
        slider.value = stamina;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
