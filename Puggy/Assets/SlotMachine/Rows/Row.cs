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

        private bool _isSpinning;
        private int _mainSymbolIndex;
        private float _interval;
        private const float Distance = 3f;
        public static float MainSpeed = 1.0f;
        private List<SymbolData> _generatedSymbols;
        private float _stopTime;
        private List<float> _intervals;

        void Start()
        {
            MakeIntervalsList();
            InitializeMainSymbolIndex();
            GenerateStartingRow();
            PrintAllSymbols();
        }

        private void MakeIntervalsList()
        {
            _intervals = new List<float> {0};

            for (int i = 1; i < _possibleDatas.Count; i++)
            {
                _intervals.Add(Distance + _intervals[i - 1]);
            }
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
            const float upperBoundary = 8.5f;
            const float lowerBoundary = -0.4f;

            foreach (Transform symbolTransform in transform)
            {
                if (symbolTransform.localPosition.y <= upperBoundary && symbolTransform.localPosition.y >= lowerBoundary)
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
            if (!_isSpinning) return;

            foreach (Transform symbolTransform in transform)
            {
                symbolTransform.localPosition += Vector3.down;
            }
        }

        private void DecreaseTimer()
        {
            if (_stopTime <= 0) return;

            _stopTime -= Time.deltaTime;
        }

        private void RenewPassedSymbols()
        {
            foreach (Transform symbolTransform in transform)
            {
                if (symbolTransform.localPosition.y <= -Distance)
                {
                    symbolTransform.localPosition = Vector3.up * ((transform.childCount-1) * Distance);
                }
            }
        }

        private void StopIfTimePassed()
        {
            if (_stopTime > 0) return;

            _isSpinning = false;
            MoveTowardsClosestSpot();
        }

        //TODO: REFACTOR
        private void MoveTowardsClosestSpot()
        {
            foreach (Transform symbolTransform in transform)
            {
                float newYpos = symbolTransform.localPosition.y;
                float minDifference = 100;
                var endVector = Vector3.zero;

                if (symbolTransform.localPosition.y <= (-Distance/2))
                {
                    symbolTransform.localPosition = Vector3.up * ((transform.childCount - 1) * Distance);
                }
                else
                {
                    for (int i = 0; i < _intervals.Count; i++)
                    {
                        float newDifference = Mathf.Abs(Mathf.Abs(symbolTransform.localPosition.y) - _intervals[i]);
                        if (newDifference < minDifference)
                        {
                            minDifference = newDifference;
                            newYpos = _intervals[i];
                        }
                    }
                    endVector = new Vector3(0, newYpos, 0);
                    symbolTransform.localPosition = Vector3.MoveTowards(symbolTransform.localPosition, endVector, MainSpeed);
                }

            }
        }

        public void StartSpin()
        {
            _isSpinning = true;
        }

        public void SetStopTime(float time)
        {
            _stopTime = time;
        }
    }
}
