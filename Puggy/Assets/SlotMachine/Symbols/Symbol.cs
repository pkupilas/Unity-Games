using System.Collections.Generic;
using UnityEngine;

namespace Machine.Symbols
{
    public class Symbol : MonoBehaviour
    {
        [SerializeField] private List<SymbolData> _possibleDatas;
        private SymbolData _symbolData;
        private SpriteRenderer _spriteRenderer;

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            GenerateSymbolData();
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
    }
}
