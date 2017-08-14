using UnityEngine;
using UnityEngine.UI;
using CameraUI.LevelManager;
using SlotMachine.Rows;

namespace SlotMachine
{
    public class SlotMachinePrototype : MonoBehaviour
    {
        [SerializeField] private float _rollCost;
        [SerializeField] private Text _moneyText;
        [SerializeField] private GameObject _rows;

        private MoneyBox.MoneyBox _playerMoneyBox;
        private LevelManager _levelManager;
        private float RowSpinTime = 2f;

        void Start()
        {
            _playerMoneyBox = FindObjectOfType<MoneyBox.MoneyBox>();
            _levelManager = FindObjectOfType<LevelManager>();
            UpdateMoneyText();
        }

        void Update()
        {
            CheckIfGameShouldEnd();
        }

        private void CheckIfGameShouldEnd()
        {
            if (_playerMoneyBox.GetPlayerMoney() < _rollCost)
            {
                _levelManager.LoadNextLevel();
            }
        }

        //private float CountComboCredit()
        //{
        //    float credit = 0f;
        //    var generatedSymbols = new List<Symbol>();
        //    var generatedSymbolsIds = new List<int>();

        //    foreach (Transform symbol in _symbolsPosition.transform)
        //    {
        //        var symbolToAdd = symbol.gameObject.GetComponent<Symbol>();
        //        generatedSymbols.Add(symbolToAdd);
        //        generatedSymbolsIds.Add(symbolToAdd.GetId());
        //    }

        //    if (generatedSymbolsIds[0] == generatedSymbolsIds[1])
        //    {
        //        credit += generatedSymbols[0].GetPrize();
        //        if (generatedSymbolsIds[0] == generatedSymbolsIds[2])
        //        {
        //            credit += generatedSymbols[0].GetPrize();
        //        }
        //    }
        //    else if (generatedSymbolsIds[1] == generatedSymbolsIds[2])
        //    {
        //        credit += generatedSymbols[1].GetPrize();
        //    }
        //    else if (generatedSymbolsIds[0] == generatedSymbolsIds[2])
        //    {
        //        credit += generatedSymbols[1].GetPrize();
        //    }

        //    return credit;
        //}

        private void UpdateMoneyText()
        {
            _moneyText.text = $"Your credits:\n{_playerMoneyBox.GetPlayerMoney()}";
        }

        public void Spin()
        {
            if (_playerMoneyBox.GetPlayerMoney() > 0)
            {
                _playerMoneyBox.PayForSpin(_rollCost);
                SpinAllRows();
                UpdateMoneyText();
            }
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
    }
}
