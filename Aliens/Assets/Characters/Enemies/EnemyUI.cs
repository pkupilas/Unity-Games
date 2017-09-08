﻿using UnityEngine;

namespace _Characters
{
    public class EnemyUI : MonoBehaviour {

        // Works around Unity 5.5's lack of nested prefabs
        [Tooltip("The UI canvas prefab")]
        [SerializeField]
        GameObject enemyCanvasPrefab = null;

        Camera cameraToLookAt;

        // Use this for initialization 
        void Start()
        {
            cameraToLookAt = Camera.main;
            Instantiate(enemyCanvasPrefab, transform.position, Quaternion.identity, transform);
        }

        // Update is called once per frame 
        void LateUpdate()
        {
            transform.LookAt(cameraToLookAt.transform);
        }
    }
}