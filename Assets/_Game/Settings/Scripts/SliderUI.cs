using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text valueText;

    [SerializeField] private float valueMultiplier = 1;
    [SerializeField] private bool roundToInt;

    private void Awake()
    {
        AddListener(SetValue);
    }

    public void SetValue(float newValue)
    {
        slider.value = newValue;

        float finalValue = newValue * valueMultiplier;

        valueText.text = roundToInt ? Mathf.RoundToInt(finalValue).ToString() : finalValue.ToString("F2");
    }

    public void AddListener(UnityAction<float> unityAction)
    {
        slider.onValueChanged.AddListener(unityAction);
    }

    public void RemoveListener(UnityAction<float> unityAction)
    {
        slider.onValueChanged.RemoveListener(unityAction);
    }

    public void RemoveAllListeners()
    {
        slider.onValueChanged.RemoveAllListeners();
    }

    private void OnDestroy()
    {
        RemoveListener(SetValue);
    }
}
