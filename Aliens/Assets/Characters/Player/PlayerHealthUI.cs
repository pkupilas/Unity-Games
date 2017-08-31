using UnityEngine;
using UnityEngine.UI;

namespace Characters.Player
{
    public class PlayerHealthUI : MonoBehaviour
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
            if (_player)
            {
                var healthComponent = _player.GetComponent<Health>();
                _healthImage.fillAmount = Mathf.Clamp(0.0075f * healthComponent.CurrentHealth, 0f, 0.75f);
            }
        }
    }
}
