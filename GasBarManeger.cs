using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GasBarManeger : MonoBehaviour
{
    [SerializeField] private Slider barSlider;
    [SerializeField] private float maxValue;
    [SerializeField] private float decreaseValue;
    [SerializeField] private float increaseValue;

    private void Awake()
    {
        barSlider.maxValue = maxValue;
        barSlider.value = maxValue;
    }


    public void DecreaseBar()
    {
        barSlider.value -= decreaseValue;
    }

    public void IncreaseBar()
    {
        barSlider.value += increaseValue;
    }

    public float GetBarValue()
    {
        return barSlider.value;
    }

    public float GetBarMaxValue()
    {
        return maxValue;
    }
}
