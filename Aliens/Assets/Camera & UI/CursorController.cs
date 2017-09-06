using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] private Texture2D _cursorTexture;
    [SerializeField] private Vector2 _hotspot = new Vector2(0, 0);

    void Start ()
    {
	    Cursor.SetCursor(_cursorTexture, _hotspot, CursorMode.Auto);	
	}
}
