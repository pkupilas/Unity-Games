using UnityEngine;

namespace Machine.Symbols
{
    [CreateAssetMenu(menuName = "SymbolData")]
    public class SymbolData : ScriptableObject
    {
        [SerializeField] private float _prize;
        [SerializeField] private Sprite _sprite;

        public float GetPrize()
        {
            return _prize;
        }

        public Sprite GetSprite()
        {
            return _sprite;
        }

        public void SetSprite(Sprite sprite)
        {
            _sprite = sprite;
        }
    }
}
