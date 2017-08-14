using System;
using System.Collections.Generic;
using SlotMachine.Symbols;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SlotMachine.Columns
{
    public class Column : MonoBehaviour
    {
        [SerializeField] private List<SymbolData> _possibleDatas;
        [SerializeField] private GameObject _symbolPrefab;

        private bool _isSpinning;
        private bool _isAlreadyStopped;
        private float _stopTime;
        private int _startingSymbolIndex;
        private List<SymbolData> _generatedSymbolDatas;
        private List<float> _snapPositionsY;
        private SlotMachine _slotMachine;

        private const float Distance = 3.0f;

        void Start()
        {
            _isAlreadyStopped = true;
            _slotMachine = FindObjectOfType<SlotMachine>();
            MakeIntervalsList();
            InitializeStartingSymbolIndex();
            GenerateStartingColumnData();
            CreateColumn();
        }

        void Update()
        {
            ManagePrintingSymbols();
            MoveSymbols();
            DecreaseTimer();
            PullUpPassedSymbols();
            StopIfTimePassed();
        }
        
        private void MakeIntervalsList()
        {
            _snapPositionsY = new List<float> { 0 };

            for (int i = 1; i < _possibleDatas.Count; i++)
            {
                _snapPositionsY.Add(Distance + _snapPositionsY[i - 1]);
            }
        }

        private void InitializeStartingSymbolIndex()
        {
            _startingSymbolIndex = Random.Range(0, _possibleDatas.Count);
        }

        private void GenerateStartingColumnData()
        {
            _generatedSymbolDatas = new List<SymbolData>();

            for (int i = _startingSymbolIndex; i < _possibleDatas.Count; i++)
            {
                _generatedSymbolDatas.Add(_possibleDatas[i]);
            }

            for (int i = 0; i < _startingSymbolIndex; i++)
            {
                _generatedSymbolDatas.Add(_possibleDatas[i]);
            }
        }

        private void CreateColumn()
        {
            float intervalPosition = 0;

            foreach (var symbolData in _generatedSymbolDatas)
            {
                var newSymbol = Instantiate(_symbolPrefab);
                newSymbol.GetComponent<Symbol>().SetData(symbolData);
                newSymbol.transform.parent = gameObject.transform;
                newSymbol.transform.localPosition = new Vector3(0f, intervalPosition, 0f);
                intervalPosition += Distance;
            }
        }

        private void ManagePrintingSymbols()
        {
            const float upperBoundary = 8.5f;
            const float lowerBoundary = -0.4f;

            foreach (Transform symbolTransform in transform)
            {
                var spriteRenderer = symbolTransform.gameObject.GetComponent<SpriteRenderer>();
                spriteRenderer.enabled = symbolTransform.localPosition.y <= upperBoundary &&
                                         symbolTransform.localPosition.y >= lowerBoundary;
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

        private void PullUpPassedSymbols()
        {
            if (!_isSpinning) return;

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

            if (!_isAlreadyStopped)
            {
                _isSpinning = false;
                MoveTowardsClosestSpot();

                var drawedSymbol = FindDrawedSymbol();
                _slotMachine.AddDrawedSymbol(drawedSymbol);
                _isAlreadyStopped = true;
            }
        }

        private void MoveTowardsClosestSpot()
        {
            foreach (Transform symbolTransform in transform)
            {
                if (symbolTransform.localPosition.y <= -Distance/2)
                {
                    symbolTransform.localPosition = Vector3.up * ((transform.childCount - 1) * Distance);
                }
                else
                {
                    float fixedSymbolPositionY = FindClosestPositionY(symbolTransform);
                    var endVector = new Vector3(0, fixedSymbolPositionY, 0);
                    const float speed = 1.0f;

                    symbolTransform.localPosition = Vector3.MoveTowards(symbolTransform.localPosition, endVector, speed);
                }
            }
        }

        private GameObject FindDrawedSymbol()
        {
            const float middleSymbolPositionY = 3.0f;
            const float eps = 0.001f;

            foreach (Transform symbolTransform in transform)
            {
                if (Math.Abs(symbolTransform.localPosition.y - middleSymbolPositionY) < eps)
                {
                    return symbolTransform.gameObject;
                }
            }
            
            return null;
        }

        private float FindClosestPositionY(Transform symbolTransform)
        {
            float minDifference = float.MaxValue;
            float fixedSymbolPositionY = symbolTransform.localPosition.y;

            foreach (float snapPosition in _snapPositionsY)
            {
                float newDifference = Mathf.Abs(Mathf.Abs(symbolTransform.localPosition.y) - snapPosition);
                if (newDifference < minDifference)
                {
                    minDifference = newDifference;
                    fixedSymbolPositionY = snapPosition;
                }
            }

            return fixedSymbolPositionY;
        }

        public void StartSpin()
        {
            _isSpinning = true;
            _isAlreadyStopped = false;
        }

        public void SetStopTime(float time)
        {
            _stopTime = time;
        }
    }
}
