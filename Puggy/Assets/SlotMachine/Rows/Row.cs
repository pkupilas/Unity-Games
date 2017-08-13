using System;
using System.Collections.Generic;
using SlotMachine.Symbols;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SlotMachine.Rows
{
    public class Row : MonoBehaviour
    {
        [SerializeField] private List<SymbolData> _possibleDatas;
        [SerializeField] private GameObject _symbolPrefab;

        private float _movingSpeed;
        private int _mainSymbolIndex;
        private float _interval;
        private const int Distance = 3;
        private const float DownBoundary = -0.75f;
        private const float MainSpeed = 10.0f;
        private List<SymbolData> _generatedSymbols;
        private float _stopTime;

        void Start()
        {
            InitializeMainSymbolIndex();
            GenerateStartingRow();
            PrintAllSymbols();
        }

        void Update()
        {
            SwitchOnOffSymbols();
            MoveSymbols();
            DecreaseTimer();
            RenewPassedSymbols();
            StopIfTimePassed();
        }

        private void InitializeMainSymbolIndex()
        {
            _mainSymbolIndex = Random.Range(0, _possibleDatas.Count);
        }

        private void GenerateStartingRow()
        {
            _generatedSymbols = new List<SymbolData>();

            for (int i = _mainSymbolIndex; i < _possibleDatas.Count; i++)
            {
                _generatedSymbols.Add(_possibleDatas[i]);
            }

            for (int i = 0; i < _mainSymbolIndex; i++)
            {
                _generatedSymbols.Add(_possibleDatas[i]);
            }
        }

        private void PrintAllSymbols()
        {
            foreach (var symbolData in _generatedSymbols)
            {
                var newSymbol = Instantiate(_symbolPrefab);
                newSymbol.GetComponent<Symbol>().SetData(symbolData);
                newSymbol.transform.parent = gameObject.transform;
                newSymbol.transform.localPosition = new Vector3(0f, _interval, 0f);
                _interval += Distance;
            }
        }

        private void SwitchOnOffSymbols()
        {
            foreach (Transform symbolTransform in transform)
            {
                if (symbolTransform.localPosition.y <= 8.5 && symbolTransform.localPosition.y >= -0.4)
                {
                    symbolTransform.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                }
                else
                {
                    symbolTransform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }

        private void MoveSymbols()
        {
            if (_movingSpeed <= 0) return;

            foreach (Transform symbolTransform in transform)
            {
                float step = _movingSpeed;
                symbolTransform.localPosition += Vector3.down / step;
            }
        }

        private void DecreaseTimer()
        {
            _stopTime -= Time.deltaTime;
        }

        private void CorrectPosition()
        {
            foreach (Transform symbolTransform in transform)
            {

                float symbolTransformX = symbolTransform.localPosition.x;
                float symbolTransformY = symbolTransform.localPosition.y;
                float symbolTransformZ = symbolTransform.localPosition.z;

                int fixedSymbolTransformY = (int) symbolTransformY;

                float newSymbolTransformY = (symbolTransformY - fixedSymbolTransformY) > 0.5f
                    ? Mathf.Floor(symbolTransformY)
                    : Mathf.Ceil(symbolTransformY);
                symbolTransform.localPosition = new Vector3(symbolTransformX, newSymbolTransformY, symbolTransformZ);
            }
        }

        private void RenewPassedSymbols()
        {
            foreach (Transform symbolTransform in transform)
            {
                if (symbolTransform.localPosition.y < DownBoundary)
                {
                    symbolTransform.localPosition = Vector3.up * (transform.childCount * Distance);
                }
            }
        }

        private void StopIfTimePassed()
        {
            if (_stopTime < 0)
            {
                _movingSpeed = 0f;
                //CorrectPosition();
            }

        }

        public void StartSpin()
        {
            _movingSpeed = Distance;
        }

        public void SetStopTime(float time)
        {
            _stopTime = time;
        }
    }
}
