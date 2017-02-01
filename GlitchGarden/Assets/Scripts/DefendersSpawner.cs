using UnityEngine;
using System.Collections;

public class DefendersSpawner : MonoBehaviour
{
    private Camera gameCamera;
    private GameObject defendersParent;

	// Use this for initialization
	void Start ()
	{
	    gameCamera = GameObject.Find("GameCamera").GetComponent<Camera>(); // null reference posibility
        defendersParent = GameObject.Find("Defenders") ?? CreateDefendersParent();
	}

    private GameObject CreateDefendersParent()
    {
        return new GameObject("Defenders");
    }

    private void OnMouseDown()
    {
        var defender = Button.selectedDefender;
        if (defender == null) return;

        var rawPos = CalculateWorldPointOfMouseClick();
        var roundedPos = SnapToGrid(rawPos);
        var zeroRotation = Quaternion.identity;
        var newDefender = Instantiate(defender, roundedPos, zeroRotation) as GameObject;

        if (newDefender == null) return;
        newDefender.transform.parent = defendersParent.transform;

    }

    private Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = Mathf.RoundToInt(rawWorldPos.y);

        return new Vector2(newX, newY);
    }


    private Vector2 CalculateWorldPointOfMouseClick()
    {
        if (gameCamera == null) return new Vector2(0,0);
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;
        float distanceFromCamera = 10f;

        var tripletOfPosition = new Vector3(mouseX,mouseY, distanceFromCamera);
        Vector2 position2D = gameCamera.ScreenToWorldPoint(tripletOfPosition);

        return position2D;
    }
}
