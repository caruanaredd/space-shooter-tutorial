using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ShieldSlider : MonoBehaviour
{
    // A reference to the slider component on this same object.
    private Slider _slider;

    // A reference to the slider's fill image.
    public Image sliderFill;

    // A gradient showing what colors to use for the health slider.
    public Gradient healthColor;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    // Will be used to set the slider value.
    public void SetValue(float value)
    {
        _slider.value = value;
        sliderFill.color = healthColor.Evaluate(_slider.value / _slider.maxValue);
    }
}
