using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// From https://www.youtube.com/watch?v=BLfNP4Sc_iA&ab_channel=Brackeys.
public class HealthBar : MonoBehaviour
{

    // Declare variables.
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    // Method to set the max health for the health bar.
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    // Method to fill the heath on the health bar. 
    public void SetHealth(float health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
