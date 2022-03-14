using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HP條數值相關
/// </summary>
public class HPBar : MonoBehaviour
{
    public Slider hpSlider;

    public Gradient gradient;
    public Image fill;
    //設定最大血量
    public void SetMaxHp(int maxHp)
    {
        hpSlider.maxValue = maxHp;
        hpSlider.value = maxHp;
        //設定血條的顏色
        fill.color = gradient.Evaluate(1f);
    }
    //設定HP
    public void SetHp(int hp)
    {
        hpSlider.value = hp;
        //設定血條的顏色
        fill.color = gradient.Evaluate(hpSlider.normalizedValue);
    }
}
