using System;
using UnityEngine;
using System.Collections;
using UnityEditor;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float velocity = 15f;
    public float width = 10f;
    public float height = 5f;
    public float spawnDelay = 0.5f;

    private Moving movingDirection = Moving.Stop;
    private float xmin;
    private float xmax;

    private enum Moving
    {
        Left,
        Right,
        Stop
    }

    // Use this for initialization
	void Start ()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        var leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, distance));
        var rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, distance));

        SpawnUntillFull();

        xmin = leftBoundary.x;
        xmax = rightBoundary.x;

    }

    private void SpawnEnemies()
    {
        foreach (Transform childTransform in transform)
        {
            GameObject enemyInstance = Instantiate(enemy, childTransform.position, Quaternion.identity) as GameObject;
            enemyInstance.transform.parent = childTransform;
        }
    }

    private void SpawnUntillFull()
    {
        Transform freePosition = GetNextFreeEnemyPosition();
        if (freePosition)
        {
            GameObject enemyInstance = Instantiate(enemy, freePosition.position, Quaternion.identity) as GameObject;
            enemyInstance.transform.parent = freePosition;
        }
        if (GetNextFreeEnemyPosition() != null)
        {
            Invoke("SpawnUntillFull", spawnDelay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (movingDirection == Moving.Right)
        {
            transform.position += Vector3.right * velocity * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * velocity * Time.deltaTime;
        }

        CheckChangeOfMovingDirection();
        if (CheckIfAllEnemiesAreDead())
        {
            Debug.Log("All enemies are dead.");
            SpawnUntillFull();
        }
    }

    Transform GetNextFreeEnemyPosition()
    {
        foreach (Transform childPosition in transform)
        {
            if (childPosition.childCount == 0)
            {
                return childPosition;
            }
        }

        return null;
    }

    private bool CheckIfAllEnemiesAreDead()
    {
        foreach (Transform childPosition in transform)
        {
            if (childPosition.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }

    private void CheckChangeOfMovingDirection()
    {
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);
        float rightEdgeOfFormation = transform.position.x + (0.5f * width);


        if (rightEdgeOfFormation > xmax)
        {
            movingDirection = Moving.Left;
        }
        else if (leftEdgeOfFormation < xmin)
        {
            movingDirection = Moving.Right;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));

    }
}
