using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using SlotMachine.Symbols;

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

        private void GenerateNewSymbols()
        {
            foreach (Transform symbol in _symbolsPosition.transform)
            {
                symbol.gameObject.GetComponent<Symbol>().GenerateSymbolData();
            }

            _playerMoneyBox.AddMoney(CountComboCredit());
        }

        private float CountComboCredit()
        {
            float credit = 0f;
            var generatedSymbols = new List<Symbol>();
            var generatedSymbolsIds = new List<int>();

            foreach (Transform symbol in _symbolsPosition.transform)
            {
                var symbolToAdd = symbol.gameObject.GetComponent<Symbol>();
                generatedSymbols.Add(symbolToAdd);
                generatedSymbolsIds.Add(symbolToAdd.GetId());
            }

            if (generatedSymbolsIds[0] == generatedSymbolsIds[1])
            {
                credit += generatedSymbols[0].GetPrize();
                if (generatedSymbolsIds[0] == generatedSymbolsIds[2])
                {
                    credit += generatedSymbols[0].GetPrize();
                }
            }
            else if (generatedSymbolsIds[1] == generatedSymbolsIds[2])
            {
                credit += generatedSymbols[1].GetPrize();
            }
            else if (generatedSymbolsIds[0] == generatedSymbolsIds[2])
            {
                credit += generatedSymbols[1].GetPrize();
            }

            return credit;
        }

        private void UpdateMoneyText()
        {
            _moneyText.text = $"Your credits:\n{_playerMoneyBox.GetPlayerMoney()}";
        }

        public void Roll()
        {
            if (_playerMoneyBox.GetPlayerMoney() > 0)
            {
                _playerMoneyBox.PayForRoll(_rollCost);
                GenerateNewSymbols();
                UpdateMoneyText();
            }
        }
    }
}
