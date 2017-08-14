using System.Collections.Generic;
using CameraUI;
using UnityEngine;
using UnityEngine.UI;
using CameraUI.LevelManager;
using SlotMachine.Columns;
using SlotMachine.Symbols;

namespace SlotMachine
{
    public class SlotMachine : MonoBehaviour
    {
        [SerializeField] private Text _moneyText;
        [SerializeField] private GameObject _columns;
        [SerializeField] private GameObject _sirens;
        [SerializeField] private GameObject _comboUIHolder;
        [SerializeField] private GameObject _playButton;
        [SerializeField] private AudioClip _comboSound;
        [SerializeField] private AudioClip _spinSound;

        private MoneyBox.MoneyBox _playerMoneyBox;
        private LevelManager _levelManager;
        private AudioSource _audioSource;
        private List<GameObject> _drawedSymbols;

        private float ColumnSpinTime = 2.0f;
        private float _rollCost = 10.0f;

        void Start()
        {
            InitializeDrawedSymbolList();
            _audioSource = GetComponent<AudioSource>();
            _playerMoneyBox = FindObjectOfType<MoneyBox.MoneyBox>();
            _levelManager = FindObjectOfType<LevelManager>();
            UpdateMoneyText();
        }

        void Update()
        {
            CountCombos();
        }

        private void UpdateMoneyText()
        {
            _moneyText.text = $"Credits:\n{_playerMoneyBox.GetPlayerMoney()}";
        }

        private void CountCombos()
        {
            int columnsCount = _columns.transform.childCount;

            if (_drawedSymbols.Count != columnsCount) return;

            float wonCredit = CountWonCredit();

            if (wonCredit > 0)
            {
                _playerMoneyBox.AddMoney(wonCredit);
                LaunchSirens();
                BlinkComboUI();
            }

            UpdateMoneyText();
            ActivatePlayButton();
            CheckIfGameShouldEnd();
            InitializeDrawedSymbolList();
        }
        
        private float CountWonCredit()
        {
            float wonCredit = 0f;
            var drawedSymbolsIds = new List<int>();

            foreach (GameObject symbol in _drawedSymbols)
            {
                drawedSymbolsIds.Add(symbol.GetComponent<Symbol>().GetId());
            }

            if (drawedSymbolsIds[0] == drawedSymbolsIds[1])
            {
                wonCredit += _drawedSymbols[0].GetComponent<Symbol>().GetPrize();
                if (drawedSymbolsIds[0] == drawedSymbolsIds[2])
                {
                    wonCredit += _drawedSymbols[0].GetComponent<Symbol>().GetPrize();
                }
            }
            else if (drawedSymbolsIds[1] == drawedSymbolsIds[2])
            {
                wonCredit += _drawedSymbols[1].GetComponent<Symbol>().GetPrize();
            }
            else if (drawedSymbolsIds[0] == drawedSymbolsIds[2])
            {
                wonCredit += _drawedSymbols[1].GetComponent<Symbol>().GetPrize();
            }

            return wonCredit;
        }

        private void LaunchSirens()
        {
            foreach (Transform sirenTransform in _sirens.transform)
            {
                sirenTransform.GetComponent<Animator>().SetTrigger("ComboTrigger");
                PlaySound(_comboSound);
            }
        }

        private void BlinkComboUI()
        {
            foreach (Transform uiTransform in _comboUIHolder.transform)
            {
                var tmp = uiTransform.gameObject;
                tmp.GetComponent<BlinkUI>().TurnOnBlinking();
            }
        }

        private void ActivatePlayButton()
        {
            _playButton.SetActive(true);
        }

        private void CheckIfGameShouldEnd()
        {
            if (_playerMoneyBox.GetPlayerMoney() < _rollCost)
            {
                _levelManager.LoadNextLevel();
            }
        }

        private void InitializeDrawedSymbolList()
        {
            _drawedSymbols = new List<GameObject>();
        }

        public void HandlePlayButton()
        {
            if (_playerMoneyBox.GetPlayerMoney() > 0)
            {
                DisableUIIfIsBlinking();
                DeactivatePlayButton();
                PlaySound(_spinSound);
                _playerMoneyBox.PayForSpin(_rollCost);
                UpdateMoneyText();
                SpinAllColumns();
            }
        }

        private void DisableUIIfIsBlinking()
        {
            foreach (Transform uiTransform in _comboUIHolder.transform)
            {
                uiTransform.gameObject.GetComponent<BlinkUI>().TurnOffBlinking();
            }
        }

        private void DeactivatePlayButton()
        {
            _playButton.SetActive(false);
        }

        private void PlaySound(AudioClip clip)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
        
        private void SpinAllColumns()
        {
            float stopTime = 0f;
            foreach (Transform columnTransform in _columns.transform)
            {
                stopTime += ColumnSpinTime;
                var column = columnTransform.gameObject.GetComponent<Column>();
                column.SetStopTime(stopTime);
                columnTransform.gameObject.GetComponent<Column>().StartSpin();
            }
        }
        
        public void AddDrawedSymbol(GameObject drawedSymbol)
        {
            if (drawedSymbol != null)
            {
                _drawedSymbols.Add(drawedSymbol);
            }
        }
    }
}
