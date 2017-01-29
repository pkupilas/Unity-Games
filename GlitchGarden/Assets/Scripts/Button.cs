using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
    public GameObject defenderPrefab;

    private Button[] buttons;
    public static GameObject selectedDefender;

	// Use this for initialization
	void Start ()
	{
	    buttons = FindObjectsOfType<Button>();
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
