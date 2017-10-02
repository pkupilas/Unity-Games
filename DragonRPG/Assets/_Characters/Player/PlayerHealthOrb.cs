using UnityEngine;
using UnityEngine.UI;
using _Characters.CommonScripts;

namespace _Characters.Player
{
    [RequireComponent(typeof(Image))]
    public class PlayerHealthOrb : MonoBehaviour
    {
        private Image _healthImage;
        private PlayerMovement _playerMovement;
    
        void Start()
        {
            _playerMovement = FindObjectOfType<PlayerMovement>();
            _healthImage = GetComponent<Image>();
        }
    
        void Update()
        {
            _healthImage.fillAmount = _playerMovement.GetComponent<Health>().HealthAsPercentage;
        }
    }
}