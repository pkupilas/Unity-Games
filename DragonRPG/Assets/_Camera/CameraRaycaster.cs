using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using _Levels;

namespace _Camera
{
    public class CameraRaycaster : MonoBehaviour
    {
        // INSPECTOR PROPERTIES RENDERED BY CUSTOM EDITOR SCRIPT
        [SerializeField] int[] layerPriorities;

        [SerializeField] private Texture2D _walkTexture = null;
        [SerializeField] private Vector2 _hotspot = new Vector2(0, 0);


        float maxRaycastDepth = 100f; // Hard coded value
        int topPriorityLayerLastFrame = -1; // So get ? from start with Default layer terrain

        // Setup delegates for broadcasting layer changes to other classes
        public delegate void OnMouseOverTerrain(Vector3 destination);
        public event OnMouseOverTerrain onMouseOverTerrain;
        
        public delegate void OnCursorLayerChange(int newLayer); // declare new delegate type
        public event OnCursorLayerChange notifyLayerChangeObservers; // instantiate an observer set

        public delegate void OnClickPriorityLayer(RaycastHit raycastHit, int layerHit);// declare new delegate type
        public event OnClickPriorityLayer notifyMouseClickObservers; // instantiate an observer set

        public delegate void OnRightClick(RaycastHit raycastHit, int layerHit); // declare new delegate type
        public event OnRightClick notifyRightClickObservers; // instantiate an observer set


        void Update()
        {
            // Check if pointer is over an interactable UI element
            if (EventSystem.current.IsPointerOverGameObject())
            {
                //TODO: Impliment UI interaction
            }
            else
            {
                // Raycast to max depth, every frame as things can move under mouse
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //if (RaycastForEnemy(ray)) return;
                if (RaycastForWalkable(ray)) return;

                ToRefactor();
            }
        }

        private bool RaycastForEnemy(Ray ray)
        {
            throw new System.NotImplementedException();
        }

        private bool RaycastForWalkable(Ray ray)
        {
            RaycastHit hitInfo;
            LayerMask walkableLayerMask = 1 << Utilities.WalkableLayerNumber;

            bool isWalkableLayer = Physics.Raycast(ray, out hitInfo, maxRaycastDepth, walkableLayerMask);

            if (isWalkableLayer)
            {
                Cursor.SetCursor(_walkTexture, _hotspot, CursorMode.Auto);
                onMouseOverTerrain(hitInfo.point);
                return true;
            }

            return false;
        }

        private void ToRefactor()
        {
            // Raycast to max depth, every frame as things can move under mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] raycastHits = Physics.RaycastAll(ray, maxRaycastDepth);

            RaycastHit? priorityHit = FindTopPriorityHit(raycastHits);
            if (!priorityHit.HasValue) // if hit no priority object
            {
                NotifyObserersIfLayerChanged(0); // broadcast default layer
                return;
            }

            // Notify delegates of layer change
            var layerHit = priorityHit.Value.collider.gameObject.layer;
            NotifyObserersIfLayerChanged(layerHit);

            // Notify delegates of highest priority game object under mouse when clicked
            if (Input.GetMouseButton(0))
            {
                notifyMouseClickObservers(priorityHit.Value, layerHit);
            }

            if (Input.GetMouseButtonDown(1))
            {
                notifyRightClickObservers(priorityHit.Value, layerHit);
            }
        }

        void NotifyObserersIfLayerChanged(int newLayer)
        {
            if (newLayer != topPriorityLayerLastFrame)
            {
                topPriorityLayerLastFrame = newLayer;
                notifyLayerChangeObservers (newLayer);
            }
        }

        RaycastHit? FindTopPriorityHit (RaycastHit[] raycastHits)
        {
            // Form list of layer numbers hit
            List<int> layersOfHitColliders = new List<int> ();
            foreach (RaycastHit hit in raycastHits)
            {
                layersOfHitColliders.Add (hit.collider.gameObject.layer);
            }

            // Step through layers in order of priority looking for a gameobject with that layer
            foreach (int layer in layerPriorities)
            {
                foreach (RaycastHit hit in raycastHits)
                {
                    if (hit.collider.gameObject.layer == layer)
                    {
                        return hit; // stop looking
                    }
                }
            }
            return null; // because cannot use GameObject? nullable
        }
    }
}