using UnityEngine;
using UnityEngine.UI;

namespace _Characters.Enemies
{
    public class EnemyHealthBar : MonoBehaviour
    {
        private RawImage _healthBarRawImage;
        private Enemy _enemy;
        
        void Start()
        {
            _enemy = GetComponentInParent<Enemy>(); // Different to way player's health bar finds player
            _healthBarRawImage = GetComponent<RawImage>();
        }
        
        void Update()
        {
            float xValue = -(_enemy.HealthAsPercentage / 2f) - 0.5f;
            _healthBarRawImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
        }
    }
}
