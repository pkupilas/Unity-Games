using UnityEngine;
using UnityEngine.UI;
using _Characters.CommonScripts;

namespace _Characters.Player
{
    [RequireComponent(typeof(Image))]
    public class PlayerHealthOrb : MonoBehaviour
    {

        private Image _healthImage;
        private Health _health;
    
        void Start()
        {
            _health = FindObjectOfType<Health>();
            _healthImage = GetComponent<Image>();
        }
    
        void Update()
        {
            _healthImage.fillAmount = _health.HealthAsPercentage;
        }
    }
}