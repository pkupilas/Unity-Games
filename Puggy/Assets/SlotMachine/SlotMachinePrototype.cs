using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CameraUI.LevelManager;
using SlotMachine.Rows;
using SlotMachine.Symbols;

namespace SlotMachine
{
    public class SlotMachinePrototype : MonoBehaviour
    {
        [SerializeField] private float _rollCost;
        [SerializeField] private Text _moneyText;
        [SerializeField] private GameObject _rows;
        [SerializeField] private GameObject _sirens;
        [SerializeField] private GameObject _comboUI;
        [SerializeField] private GameObject _playButton;

        private MoneyBox.MoneyBox _playerMoneyBox;
        private LevelManager _levelManager;
        private float RowSpinTime = 2f;
        private List<GameObject> _drawedSymbols;

        void Start()
        {
            _drawedSymbols = new List<GameObject>();
            _playerMoneyBox = FindObjectOfType<MoneyBox.MoneyBox>();
            _levelManager = FindObjectOfType<LevelManager>();
            UpdateMoneyText();
        }

        void Update()
        {
            CountCombos();
            CheckIfGameShouldEnd();
        }

        private void CheckIfGameShouldEnd()
        {
            if (_playerMoneyBox.GetPlayerMoney() < _rollCost)
            {
                _levelManager.LoadNextLevel();
            }
        }

        private float CountComboCredit()
        {
            float credit = 0f;
            var drawedSymbolsIds = new List<int>();

            foreach (GameObject symbol in _drawedSymbols)
            {
                drawedSymbolsIds.Add(symbol.GetComponent<Symbol>().GetId());
            }

            if (drawedSymbolsIds[0] == drawedSymbolsIds[1])
            {
                credit += _drawedSymbols[0].GetComponent<Symbol>().GetPrize();
                if (drawedSymbolsIds[0] == drawedSymbolsIds[2])
                {
                    credit += _drawedSymbols[0].GetComponent<Symbol>().GetPrize();
                }
            }
            else if (drawedSymbolsIds[1] == drawedSymbolsIds[2])
            {
                credit += _drawedSymbols[1].GetComponent<Symbol>().GetPrize();
            }
            else if (drawedSymbolsIds[0] == drawedSymbolsIds[2])
            {
                credit += _drawedSymbols[1].GetComponent<Symbol>().GetPrize();
            }

            return credit;
        }

        private void UpdateMoneyText()
        {
            _moneyText.text = $"Your credits:\n{_playerMoneyBox.GetPlayerMoney()}";
        }

        public void Spin()
        {
            if (_playerMoneyBox.GetPlayerMoney() > 0)
            {
                DisableBlinkingOnComboUI();
                DeactivatePlayButton();
                _playerMoneyBox.PayForSpin(_rollCost);
                UpdateMoneyText();
                SpinAllRows();
            }
        }

        private void CountCombos()
        {
            const int middleRowSymbolsCount = 3;
            if (_drawedSymbols.Count != middleRowSymbolsCount) return;

            float wonCredit = CountComboCredit();
            _playerMoneyBox.AddMoney(wonCredit);

            if (wonCredit > 0)
            {
                LaunchSirens();
                BlinkComboUI();
            }

            UpdateMoneyText();
            ActivatePlayButton();
            _drawedSymbols = new List<GameObject>();
        }

        private void SpinAllRows()
        {
            float stopTime = 0f;
            foreach (Transform rowTransform in _rows.transform)
            {
                stopTime += RowSpinTime;
                var row = rowTransform.gameObject.GetComponent<Row>();
                row.SetStopTime(stopTime);
                rowTransform.gameObject.GetComponent<Row>().StartSpin();
            }
        }

        public void AddDrawedSymbol(GameObject drawedSymbol)
        {
            if (drawedSymbol != null)
            {
                _drawedSymbols.Add(drawedSymbol);
            }
        }

        private void LaunchSirens()
        {
            foreach (Transform sirenTransform in _sirens.transform)
            {
                sirenTransform.GetComponent<Animator>().SetTrigger("ComboTrigger");
            }
        }

        private void BlinkComboUI()
        {
            foreach (Transform uiTransform in _comboUI.transform)
            {
                var tmp = uiTransform.gameObject;
                tmp.GetComponent<BlinkUI>().TurnOnBlinking();
            }
        }

        private void DisableBlinkingOnComboUI()
        {
            foreach (Transform uiTransform in _comboUI.transform)
            {
                var tmp = uiTransform.gameObject;
                tmp.GetComponent<BlinkUI>().TurnOffBlinking();
            }
        }

        private void ActivatePlayButton()
        {
            _playButton.SetActive(true);   
        }
        private void DeactivatePlayButton()
        {
            _playButton.SetActive(false);
        }
    }
}
