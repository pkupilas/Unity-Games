using UnityEngine;
using System.Collections;

public class DefendersSpawner : MonoBehaviour
{
    private Camera gameCamera;
    private GameObject defendersParent;
    private StarDisplay starDisplay;

	// Use this for initialization
	void Start ()
	{
	    gameCamera = GameObject.Find("GameCamera").GetComponent<Camera>(); // null reference posibility
        defendersParent = GameObject.Find("Defenders") ?? CreateDefendersParent();
	    starDisplay = FindObjectOfType<StarDisplay>();
	}

    private GameObject CreateDefendersParent()
    {
        return new GameObject("Defenders");
    }

    private void OnMouseDown()
    {
        var defender = Button.selectedDefender;
        if (defender == null) return;
        int defenderCost = defender.GetComponent<Defender>().starCost;

        StarDisplay.Status defenderStatus = starDisplay.UseStars(defenderCost);

        if (defenderStatus != StarDisplay.Status.SUCCESS)
        {
            Debug.Log("Insufficient stars.");
            return;
        }

        var rawPos = CalculateWorldPointOfMouseClick();
        var roundedPos = SnapToGrid(rawPos);

        SpawnDefender(defender, roundedPos);
    }

    private void SpawnDefender(GameObject defender, Vector2 roundedPos)
    {
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
