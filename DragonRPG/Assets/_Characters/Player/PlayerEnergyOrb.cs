using UnityEngine;
using UnityEngine.UI;
using _Characters.CommonScripts;

namespace _Characters.Player
{
    [RequireComponent(typeof(Image))]
    public class PlayerEnergyOrb : MonoBehaviour
    {
        private Image _energyImage;
        private PlayerMovement _playerMovement;

        void Start()
        {
            _playerMovement = FindObjectOfType<PlayerMovement>();
            _energyImage = GetComponent<Image>();
        }

        void Update()
        {
            _energyImage.fillAmount = _playerMovement.GetComponent<SpecialAbilities>().EnergyAsPercentage;
        }
    }
}