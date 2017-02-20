using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Base : MonoBehaviour
{
    public Text helthText;
    public float health = 100;

	// Use this for initialization
	void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        helthText.text = health.ToString();
    }

    public void ApplyDamage(float damage)
    {
        health -= damage;
    }

}
