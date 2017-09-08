using UnityEngine;

namespace Characters
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] protected CharacterData characterData;
        public CharacterData CharacterData => characterData;
    }
}
