using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    [SerializeField]
    private Text label;

    [SerializeField]
    private Image bar;


	public void UpdateHealthBar (float value, float ratio) {
        label.text = value.ToString("0");
        bar.rectTransform.localScale = new Vector3(ratio, 1, 1);
	}
}
