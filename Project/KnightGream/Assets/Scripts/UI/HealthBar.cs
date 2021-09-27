using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{

    public Slider slider;

    public Gradient gradient;

    public Image Fill;
    public void SetMaxHealth(float maxhealth)
    {
        slider.maxValue = maxhealth;
        slider.value = maxhealth;

       Fill.color= gradient.Evaluate(1f);
    }
    public void SetHealth(float Health)
    {
        slider.value = Health;

        Fill.color= gradient.Evaluate(slider.normalizedValue);
    }

}
