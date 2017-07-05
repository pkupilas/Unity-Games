using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{

    public Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };

    [SerializeField] float distanceToBackground = 100f;
    Camera viewCamera;

    RaycastHit m_hit;
    public RaycastHit Hit
    {
        get { return m_hit; }
    }

    Layer _layerHit;
    public Layer LayerHit
    {
        get { return _layerHit; }
    }

    void Start()
    {
        viewCamera = Camera.main;
    }

    void Update()
    {
        // Look for and return priority layer Hit
        foreach (Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);
            if (hit.HasValue)
            {
                m_hit = hit.Value;
                _layerHit = layer;
                return;
            }
        }

        // Otherwise return background Hit
        m_hit.distance = distanceToBackground;
        _layerHit = Layer.RaycastEndStop;
    }

    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer;
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit; // used as an out parameter
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
        if (hasHit)
        {
            return hit;
        }
        return null;
    }
}
