using UnityEngine;
using UnityEngine.UI;
using _Characters.CommonScripts;

namespace _Characters.Enemies
{
    public class EnemyHealthBar : MonoBehaviour
    {
        private RawImage _healthBarRawImage;
        private Health _health;
        
        void Start()
        {
            _health = GetComponentInParent<Health>();
            _healthBarRawImage = GetComponent<RawImage>();
        }
        
        void Update()
        {
            float xValue = -(_health.HealthAsPercentage / 2f) - 0.5f;
            _healthBarRawImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
        }
    }
}
