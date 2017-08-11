using Machine.Symbols;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace SlotMachine
{
    public class SlotMachine : MonoBehaviour
    {
        [SerializeField] private GameObject _symbolPrefab;
        [SerializeField] private GameObject _symbolsPosition;
        [SerializeField] private float _rollCost;
        [SerializeField] private Text _moneyText;

        private MoneyBox.MoneyBox _playerMoneyBox;
        private static float _nextSymbolPositionX = 0;
        private int _symbolsCount = 3;

        void Start()
        {
            _playerMoneyBox = FindObjectOfType<MoneyBox.MoneyBox>();

            InitializeSymbols();
            UpdateMoneyText();
        }

        private void InitializeSymbols()
        {
            for (int i = 0; i < _symbolsCount; i++)
            {
                var newSymbol = Instantiate(_symbolPrefab);
                newSymbol.transform.parent = _symbolsPosition.transform;

                float newSymbolPositionX = _nextSymbolPositionX;
                float newSymbolPositionY = 0f;
                float newSymbolPositionZ = 0f;

                newSymbol.transform.localPosition = new Vector3(newSymbolPositionX, newSymbolPositionY, newSymbolPositionZ);
                _nextSymbolPositionX += 5f;
            }
        }

        public void Roll()
        {
            if (_playerMoneyBox.GetPlayerMoney() > 0)
            {
                _playerMoneyBox.PayForRoll(_rollCost);
                UpdateMoneyText();
                foreach (Transform symbol in _symbolsPosition.transform)
                {
                    symbol.gameObject.GetComponent<Symbol>().GenerateSymbolData();
                }
            }
        }

        private void UpdateMoneyText()
        {
            _moneyText.text = $"Your credits:\n{_playerMoneyBox.GetPlayerMoney()}";
        }
    }
}
