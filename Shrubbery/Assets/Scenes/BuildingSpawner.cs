using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    [SerializeField] private GameObject turretPrefab;
    private bool isBuilding = false;
    private GameObject buildingToPlace;
    
    private void Update()
    {
        var mousePositionIn3D = GetWorldPositionFromCursor();

        if (isBuilding)
        {
            if (Input.GetMouseButton(0))
            {
                isBuilding = false;
                // place building
            }
            else
            {
                buildingToPlace.transform.position = mousePositionIn3D;
            }
        }
        else if (Input.GetKey(KeyCode.T))
        {
            isBuilding = true;
            buildingToPlace = Instantiate(turretPrefab, mousePositionIn3D, Quaternion.identity);
        }
    }

    public Vector3 GetWorldPositionFromCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        float maxDistance = 1000.0f;
        var result = Physics.Raycast(ray, out hitInfo, maxDistance, LayerMask.GetMask("Walkable"));

        return result ? hitInfo.point : Vector3.zero;
    }
}
