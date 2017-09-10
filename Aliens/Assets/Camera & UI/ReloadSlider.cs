using System;
using UnityEngine;
using UnityEngine.UI;

public class ReloadSlider : MonoBehaviour
{
    private Slider _slider;
    private bool  _isIncreasing;
    private float _increaseValue;

    void Start()
    {
        _slider = GetComponent<Slider>();
    }

    void Update()
    {
        if (_isIncreasing)
        {
            _slider.value += _increaseValue*Time.deltaTime;
        }

        if (Math.Abs(_slider.value - _slider.maxValue) < Mathf.Epsilon)
        {
            HideSlider();
        }
    }

    public void ShowSlider()
    {
        _slider.value = 0f;

        foreach (Transform childTransform in transform)
        {
            childTransform.gameObject.SetActive(true);
        }
    }

    public void HideSlider()
    {
        _isIncreasing = false;

        foreach (Transform childTransform in transform)
        {
            childTransform.gameObject.SetActive(false);
        }
    }

    public void IncreaseValueBy(float increase)
    {
        _increaseValue = increase;
        _isIncreasing = true;
    }
}
