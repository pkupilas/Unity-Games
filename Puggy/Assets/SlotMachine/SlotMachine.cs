using System.Collections.Generic;
using Machine.Symbols;
using UnityEngine;

namespace SlotMachine
{
    public class SlotMachine : MonoBehaviour
    {
        [SerializeField] private GameObject _symbolPrefab;
        [SerializeField] private GameObject _symbolsPosition;

        private static float nextSymbolPositionX = 0;
        private int _symbolsCount = 3;

        void Start()
        {
            for (int i = 0; i < _symbolsCount; i++)
            {
                var newSymbol = Instantiate(_symbolPrefab);
                newSymbol.transform.parent = _symbolsPosition.transform;

                float newSymbolPositionX = nextSymbolPositionX; 
                float newSymbolPositionY = 0f;
                float newSymbolPositionZ = 0f;

                newSymbol.transform.localPosition = new Vector3(newSymbolPositionX, newSymbolPositionY, newSymbolPositionZ);
                nextSymbolPositionX += 5f;
            }
        }

        public void Roll()
        {
            foreach (Transform symbol in _symbolsPosition.transform)
            {
                symbol.gameObject.GetComponent<Symbol>().GenerateSymbolData();
            }
        }

    }
}
