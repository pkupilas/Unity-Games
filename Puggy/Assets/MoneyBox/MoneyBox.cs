using UnityEngine;

namespace MoneyBox
{
    public class MoneyBox : MonoBehaviour
    {
        [SerializeField] private float _playerMoney;

        public float GetPlayerMoney()
        {
            return _playerMoney;
        }

        public void PayForRoll(float rollCost)
        {
            _playerMoney -= rollCost;
        }
    }
}
