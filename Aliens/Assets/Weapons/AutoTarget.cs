using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTarget : MonoBehaviour
{
    public GameObject SpottedEnemy;
    private LayerMask _enemyMask;

    void Start()
    {
        _enemyMask = LayerMask.GetMask("Enemy");
    }

    void Update()
    {
        LookForEnemy();
    }

    private void LookForEnemy()
    {
        float _camRayLength = 200f;
        RaycastHit cameraRayHitWithEnemy;
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(cameraRay.origin, cameraRay.direction * 200f, Color.green);
        if (Physics.Raycast(cameraRay, out cameraRayHitWithEnemy, _camRayLength, _enemyMask))
        {
            SpottedEnemy = cameraRayHitWithEnemy.transform.gameObject;
        }
        else
        {
            SpottedEnemy = null;
        }
    }
}
