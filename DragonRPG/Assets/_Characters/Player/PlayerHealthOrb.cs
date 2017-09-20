using UnityEngine;
using UnityEngine.UI;

namespace _Characters.Player
{
    [RequireComponent(typeof(Image))]
    public class PlayerHealthOrb : MonoBehaviour
    {

        private Image _healthImage;
        private Player _player;
    
        void Start()
        {
            _player = FindObjectOfType<Player>();
            _healthImage = GetComponent<Image>();
        }
    
        void Update()
        {
            _healthImage.fillAmount = _player.HealthAsPercentage;
        }
    }
}