using UnityEngine;
using UnityEngine.UI;
using _Characters.CommonScripts;

namespace _Characters.Player
{
    [RequireComponent(typeof(Image))]
    public class PlayerHealthOrb : MonoBehaviour
    {
        private Image _healthImage;
        private PlayerControl _playerControl;
    
        void Start()
        {
            _playerControl = FindObjectOfType<PlayerControl>();
            _healthImage = GetComponent<Image>();
        }
    
        void Update()
        {
            _healthImage.fillAmount = _playerControl.GetComponent<Health>().HealthAsPercentage;
        }
    }
}