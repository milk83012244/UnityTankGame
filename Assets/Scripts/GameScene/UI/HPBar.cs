using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Slider hpSlider;

    public Gradient gradient;
    public Image fill;
    public void SetMaxHp(int maxHp)
    {
        hpSlider.maxValue = maxHp;
        hpSlider.value = maxHp;

        fill.color = gradient.Evaluate(1f);
    }
    public void SetHp(int hp)
    {
        hpSlider.value = hp;

        fill.color = gradient.Evaluate(hpSlider.normalizedValue);
    }
}
