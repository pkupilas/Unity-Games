using UnityEngine;
using UnityEngine.UI;
using _Characters.CommonScripts;

namespace _Characters.Player
{
    [RequireComponent(typeof(Image))]
    public class PlayerEnergyOrb : MonoBehaviour
    {
        private Image _energyImage;
        private PlayerControl _playerControl;

        void Start()
        {
            _playerControl = FindObjectOfType<PlayerControl>();
            _energyImage = GetComponent<Image>();
        }

        void Update()
        {
            _energyImage.fillAmount = _playerControl.GetComponent<SpecialAbilities>().EnergyAsPercentage;
        }
    }
}