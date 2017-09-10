using UnityEngine;
using UnityEngine.UI;

namespace Characters.Enemies
{
    public class EnemyHealthBar : MonoBehaviour
    {
        private RawImage _healthBarRawImage;
        private Enemy _enemy;
        
        void Start()
        {
            _enemy = GetComponentInParent<Enemy>();
            _healthBarRawImage = GetComponent<RawImage>();
        }
        
        void Update()
        {
            var healthComponent = _enemy.GetComponent<Health>();
            float xValue = -(healthComponent.GetHealthAsPercentage() / 2f) - 0.5f;
            _healthBarRawImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
        }
    }
}
