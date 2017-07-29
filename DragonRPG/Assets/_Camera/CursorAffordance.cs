using UnityEngine;
using _Levels;

namespace _Camera
{
    [RequireComponent(typeof(CameraRaycaster))]
    public class CursorAffordance : MonoBehaviour
    {

        private CameraRaycaster _cameraRaycaster;

        [SerializeField] private Texture2D _walkTexture = null;
        [SerializeField] private Texture2D _attackTexture = null;
        [SerializeField] private Texture2D _questionTexture = null;
        [SerializeField] private Vector2 _hotspot = new Vector2(0, 0);
    

        void Start ()
        {
            _cameraRaycaster = GetComponent<CameraRaycaster>();
            _cameraRaycaster.notifyLayerChangeObservers += OnLayerChangedHandler;
        }
	
        void OnLayerChangedHandler(int newLayer)
        {   
            switch (newLayer)
            {
                case Utilities.WalkableLayerNumber:
                    Cursor.SetCursor(_walkTexture, _hotspot, CursorMode.Auto);
                    break;
                case Utilities.EnemyLayerNumber:
                    Cursor.SetCursor(_attackTexture, _hotspot, CursorMode.Auto);
                    break;
                default:
                    Cursor.SetCursor(_questionTexture, _hotspot, CursorMode.Auto);
                    break;
            }
        }
    }
}
