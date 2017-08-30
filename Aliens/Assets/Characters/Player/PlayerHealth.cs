using UnityEngine;
using UnityEngine.UI;

namespace Characters.Player
{
    public class PlayerHealth : MonoBehaviour
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
            _healthImage.fillAmount = Mathf.Clamp(0.0075f * _player.CurrentHealth, 0f, 0.75f);
        }
    }
}
