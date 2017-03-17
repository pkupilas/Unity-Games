using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{

    private GameObject beamWayHolder;

    // Use this for initialization
    void Awake ()
	{
        beamWayHolder = GameObject.Find("BeamWayHolder");
	}

    void Start()
    {
        CreateBeamWay();
    }

	// Update is called once per frame
	void Update () {
		
	}

    void CreateBeamWay()
    {
        var colorList = new List<Color> {Color.black, Color.blue, Color.green, Color.red, Color.yellow};
        bool nextUp = false;
        bool nextDown = false;
        for (int i = 0; i < 1000; i++)
        {

            var box = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (nextUp)
            {
                box.transform.position += Vector3.up;
                nextUp = false;
            }
            if (nextDown)
            {

                box.transform.position -= Vector3.up;
            }
            box.transform.SetParent(beamWayHolder.transform);
            box.transform.position = new Vector3(box.transform.position.x, box.transform.position.y, 30 + i);
            box.AddComponent<BoxCollider>();
            var randomIndex = Random.Range(0, colorList.Count);
            box.GetComponent<MeshRenderer>().materials[0].color = colorList[randomIndex];
            if (Random.value > 0.5f)
            {
                box.transform.position += Vector3.up;
                nextUp = true;
            }
            else
            {
                nextDown = true;
            }
        }
    }
}
