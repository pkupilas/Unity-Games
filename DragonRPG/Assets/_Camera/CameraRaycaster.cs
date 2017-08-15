using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using _Characters;
using _Levels;

namespace _Camera
{
    public class CameraRaycaster : MonoBehaviour
    {
        [SerializeField] private Texture2D _walkCursorTexture;
        [SerializeField] private Texture2D _enemyCursorTexture;
        [SerializeField] private Vector2 _hotspot = new Vector2(0, 0);
        
        float maxRaycastDepth = 100f; // Hard coded value

        // Setup delegates for broadcasting layer changes to other classes
        public delegate void OnMouseOverTerrain(Vector3 destination);
        public event OnMouseOverTerrain onMouseOverTerrain;

        public delegate void OnMouseOverEnemy(Enemy enemy);
        public event OnMouseOverEnemy onMouseOverEnemy;

        void Update()
        {
            // Check if pointer is over an interactable UI element
            if (EventSystem.current.IsPointerOverGameObject())
            {
                //TODO: Impliment UI interaction
            }
            else
            {
                PerformRaycasts();
            }
        }

        private void PerformRaycasts()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (RaycastForEnemy(ray)) return;
            if (RaycastForWalkable(ray)) return;
        }

        private bool RaycastForEnemy(Ray ray)
        {
            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo, maxRaycastDepth);

            if (hitInfo.transform)
            {
                var hitGameObject = hitInfo.collider.gameObject;
                var enemy = hitGameObject.GetComponent<Enemy>();
                if (enemy)
                {
                    Cursor.SetCursor(_enemyCursorTexture, _hotspot, CursorMode.Auto);
                    onMouseOverEnemy(hitInfo.transform.GetComponent<Enemy>());
                    return true;
                }
            }

            return false;
        }

        private bool RaycastForWalkable(Ray ray)
        {
            RaycastHit hitInfo;
            LayerMask walkableLayerMask = 1 << Utilities.WalkableLayerNumber;

            bool isWalkableLayer = Physics.Raycast(ray, out hitInfo, maxRaycastDepth, walkableLayerMask);

            if (isWalkableLayer)
            {
                Cursor.SetCursor(_walkCursorTexture, _hotspot, CursorMode.Auto);
                onMouseOverTerrain(hitInfo.point);
                return true;
            }

            return false;
        }
    }
}