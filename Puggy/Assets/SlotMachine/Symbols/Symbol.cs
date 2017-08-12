using System.Collections.Generic;
using UnityEngine;

namespace SlotMachine.Symbols
{
    public class Symbol : MonoBehaviour
    {
        [SerializeField] private List<SymbolData> _possibleDatas;
        private SymbolData _symbolData;
        private SpriteRenderer _spriteRenderer;

        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            //GenerateSymbolData();
        }
        
        private void UpdateSprite()
        {
            _spriteRenderer.sprite = _symbolData.GetSprite();
        }

        public void GenerateSymbolData()
        {
            int randomIndex = Random.Range(0, _possibleDatas.Count);
            _symbolData = _possibleDatas[randomIndex];
            UpdateSprite();
        }

        public void SetData(SymbolData data)
        {
            _symbolData = data;
            UpdateSprite();
        }

        public int GetId()
        {
            return _symbolData.GetId();
        }

        public float GetPrize()
        {
            return _symbolData.GetPrize();
        }
    }
}
