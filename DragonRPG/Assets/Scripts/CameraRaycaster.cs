using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{

    [SerializeField] private float distanceToBackground = 100f;
    private Camera _viewCamera;
    private RaycastHit _hit;
    private Layer _layerHit;

    public delegate void OnLayerChange(Layer newLayer);
    public event OnLayerChange onLayerChangeObserver;

    public Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };

    public RaycastHit Hit
    {
        get { return _hit; }
    }

    public Layer LayerHit
    {
        get { return _layerHit; }
    }

    void Start()
    {
        _viewCamera = Camera.main;
    }
    
    void Update()
    {
        // Look for and return priority layer Hit
        foreach (Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);
            if (hit.HasValue)
            {
                _hit = hit.Value;
                if (_layerHit != layer)
                {
                    _layerHit = layer;
                    onLayerChangeObserver(_layerHit);
                }
                return;
            }
        }

        // Otherwise return background Hit
        _hit.distance = distanceToBackground;
        if (_layerHit != Layer.RaycastEndStop)
        {
            _layerHit = Layer.RaycastEndStop;
            onLayerChangeObserver(_layerHit);
        }
    }

    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer;
        Ray ray = _viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit; // used as an out parameter
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
        if (hasHit)
        {
            return hit;
        }
        return null;
    }
}
