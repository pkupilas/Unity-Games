using UnityEngine;
using UnityEngine.UI;

namespace _Characters
{
    [RequireComponent(typeof(RawImage))]
    public class PlayerHealthBar : MonoBehaviour
    {

        private RawImage healthBarRawImage;
        private Player player;
    
        void Start()
        {
            player = FindObjectOfType<Player>();
            healthBarRawImage = GetComponent<RawImage>();
        }
    
        void Update()
        {
            float xValue = -(player.HealthAsPercentage / 2f) - 0.5f;
            healthBarRawImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
        }
    }
}
