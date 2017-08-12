using System.Collections.Generic;
using SlotMachine.Symbols;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SlotMachine.Rows
{
    public class Row : MonoBehaviour
    {
        [SerializeField] private GameObject[] _movingPoints;
        [SerializeField] private float _movingSpeed;
        [SerializeField] private List<SymbolData> _possibleDatas;
        [SerializeField] private GameObject _symbolPrefab;

        private int _mainSymbolIndex;
        private int _symbolsInRowCount;
        private const int MaxRowCapacity = 3;
        private float _interval;
        private const int _distance = 5;

        void Start()
        {
            _symbolsInRowCount = 0;
            PrintAllSymbols();
            //InitializeMainSymbolIndex();
            //InitializeSymbolsInRow();
        }

        private void PrintAllSymbols()
        {
            for (int i = 0; i < _possibleDatas.Count; i++)
            {
                var newSymbol = Instantiate(_symbolPrefab);
                newSymbol.GetComponent<Symbol>().SetData(_possibleDatas[i]);
                newSymbol.transform.parent = gameObject.transform;

                    
                float newSymbolXPosition = newSymbol.transform.localPosition.x;
                float newSymbolZPosition = newSymbol.transform.localPosition.z;

                newSymbol.transform.localPosition = new Vector3(0f, _interval, 0f);
                _interval += _distance;
            }
        }

        void Update()
        {
            SwitchOnOffSymbols();
            MoveSymbols();
            RenewPassedSymbols();
            //SpinSymbolsInRow();
        }

        private void RenewPassedSymbols()
        {
            foreach (Transform symbolTransform in transform)
            {
                if (symbolTransform.localPosition.y < -4)
                {
                    symbolTransform.localPosition = Vector3.up * (transform.childCount * _distance);
                }
            }
        }

        private void MoveSymbols()
        {
            foreach (Transform symbolTransform in transform)
            {
                symbolTransform.localPosition += Vector3.down/10;
            }
        }

        private void SwitchOnOffSymbols()
        {
            foreach (Transform symbolTransform in transform)
            {
                if (symbolTransform.localPosition.y <= 5 && symbolTransform.localPosition.y >= -4)
                {
                    symbolTransform.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                }
                else
                {
                    symbolTransform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }

        private void InitializeMainSymbolIndex()
        {
            _mainSymbolIndex = Random.Range(0, _possibleDatas.Count);
        }

        //private void InitializeSymbolsInRow()
        //{
        //    for (int i = _mainSymbolIndex; i < _possibleDatas.Count && IsRowNotFull(); i++)
        //    {
        //        Instantiate(_symbolPrefab);
        //        _symbolPrefab.transform.parent = gameObject.transform;
        //        _symbolPrefab.transform.localPosition = _movingPoints[0].transform.localPosition;
        //        _symbolsInRowCount++;
        //    }
        //    if (IsRowNotFull())
        //    {
        //        for (int i = 0; i < _mainSymbolIndex && IsRowNotFull(); i++)
        //        {
        //            Instantiate(_symbolPrefab);
        //            _symbolPrefab.transform.parent = gameObject.transform;
        //            _symbolPrefab.transform.localPosition = _movingPoints[0].transform.localPosition;
        //            _symbolsInRowCount++;
        //        }
        //    }
        //}

        //private bool IsRowNotFull()
        //{
        //    return _symbolsInRowCount <= MaxRowCapacity;
        //}

        //private void SpinSymbolsInRow()
        //{
        //    foreach (Transform symbolTransform in transform)
        //    {
        //        float step = _movingSpeed * Time.deltaTime;
        //        symbolTransform.position = Vector3.MoveTowards(symbolTransform.position, _movingPoints[1].transform.position, step);
        //    }
        //}
    }
}
