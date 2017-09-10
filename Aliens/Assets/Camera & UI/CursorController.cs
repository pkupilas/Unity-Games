using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] private Texture2D _cursorTexture;
    private readonly Vector2 _hotspot = new Vector2(256, 750);

    void Start ()
    {
	    Cursor.SetCursor(_cursorTexture, _hotspot, CursorMode.Auto);	
	}
}