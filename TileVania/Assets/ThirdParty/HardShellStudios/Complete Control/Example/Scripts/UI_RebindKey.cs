using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HardShellStudios.CompleteControl;

[AddComponentMenu("Hard Shell Studios/Complete Control/UI Rebind Button")]
[RequireComponent(typeof(Button))]
public class UI_RebindKey : MonoBehaviour {

    public string uniqueName;
    public KeyTarget keyTarget = KeyTarget.PositivePrimary;
    public Text text;
    public bool constantUpdate = false;

    private string originalString;
    private bool isBinding = false;
    private Button button;
    private bool textSettled = false;

    private void Start()
    {
        originalString = text.text;
        button = GetComponent<Button>();
        button.onClick.AddListener(RebindKey);
    }

    private void Update()
    {
        if (isBinding)
        {
            KeyCode key = hInput.CurrentKeyDown();
            if (key != KeyCode.None)
            {
                if (key == hInput.RebindRemovalKey)
                    hInput.SetKey(uniqueName, KeyCode.None, keyTarget);
                else
                    hInput.SetKey(uniqueName, key, keyTarget);

                isBinding = false;
                button.interactable = true;
            }
        }
        else
        {
            if (!textSettled || constantUpdate)
            {
                if (originalString.Contains("{key}") || originalString.Contains("{name}"))
                {
                    text.text = originalString.Replace("{key}", hInput.DetailsFromKey(uniqueName, keyTarget).ToString());
                }
                else
                    text.text = originalString;
            }
        }
    }

    public void RebindKey()
    {
        text.text = "PRESS ANY KEY";
        textSettled = false;
        isBinding = true;
        button.interactable = false;
    }
}
