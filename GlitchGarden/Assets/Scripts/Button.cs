using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Button : MonoBehaviour
{
    public GameObject defenderPrefab;
    public static GameObject selectedDefender;

    private Button[] buttons;
    private Text costText;

	// Use this for initialization
	void Start ()
	{
	    buttons = FindObjectsOfType<Button>();
	    costText = GetComponentInChildren<Text>();

	    if (costText == null)
	    {
	        Debug.LogWarning(name + " has no cost text.");
	    }
	    else
	    {
	        costText.text = defenderPrefab.GetComponent<Defender>().starCost.ToString();
	    }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnMouseDown()
    {
        selectedDefender = defenderPrefab;
        Debug.Log("selectedDefender: " + selectedDefender.name);
        foreach (var _button in buttons)
        {
            _button.GetComponent<SpriteRenderer>().color = Color.black;
        }
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;

    }
}
