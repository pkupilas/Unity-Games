using UnityEngine;
using UnityEngine.UI;

namespace Characters.Enemies
{
    public class EnemyHealthBar : MonoBehaviour
    {
        private RawImage _healthBarRawImage;
        private Enemy _enemy;

        // Use this for initialization
        void Start()
        {
            _enemy = GetComponentInParent<Enemy>();
            _healthBarRawImage = GetComponent<RawImage>();
        }

        // Update is called once per frame
        void Update()
        {
            var healthComponent = _enemy.GetComponent<Health>();
            float xValue = -(healthComponent.GetHealthAsPercentage() / 2f) - 0.5f;
            _healthBarRawImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
        }
    }
}
