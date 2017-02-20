using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PointsManager : MonoBehaviour
{

    private GameObject pointsGameObject;
    private Text pointsText;
    private float points = 0f;
    private 
	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
        UpdatePointsGameObjectIfNull();
	    UpdatePoints();
	}

    private void UpdatePointsGameObjectIfNull()
    {
        if (pointsGameObject == null)
        {
            pointsGameObject = GameObject.Find("Points");
        }
        if (pointsGameObject != null)
        {
            pointsText = pointsGameObject.GetComponent<Text>();
        }
    }

    public void AddPoints(float bonus)
    {
        points += bonus;
    }

    private void UpdatePoints()
    {
        CheckIfErasePoints();
        if (pointsText != null)
        {
            pointsText.text = points.ToString();
        }
    }

    private void CheckIfErasePoints()
    {
        if (Application.loadedLevel == 0)
        {
            points = 0f;
        }
    }
}
