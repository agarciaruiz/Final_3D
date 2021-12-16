using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private float baseValue;
    private float maxValue;

    [SerializeField] private Image fill;
    [SerializeField] private Text amount;

    public void SetValues(float _baseValue, float _maxValue)
    {
        baseValue = _baseValue;
        maxValue = _maxValue;

        amount.text = baseValue.ToString();

        CalcFillAmount();
    }

    public void CalcFillAmount()
    {
        float fillAmount = baseValue / maxValue;
        fill.fillAmount = fillAmount;
    }
}
