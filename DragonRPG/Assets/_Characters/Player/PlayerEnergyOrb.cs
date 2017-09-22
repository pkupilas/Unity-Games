using UnityEngine;
using UnityEngine.UI;
using _Characters.CommonScripts;

namespace _Characters.Player
{
    [RequireComponent(typeof(Image))]
    public class PlayerEnergyOrb : MonoBehaviour
    {
        private Image _energyImage;
        private Player _player;

        void Start()
        {
            _player = FindObjectOfType<Player>();
            _energyImage = GetComponent<Image>();
        }

        void Update()
        {
            _energyImage.fillAmount = _player.GetComponent<SpecialAbilities>().EnergyAsPercentage;
        }
    }
}