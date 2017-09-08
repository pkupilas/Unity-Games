using UnityEngine;

namespace Characters.Player
{
    [CreateAssetMenu(menuName = "Characters/Player")]
    public class PlayerData : CharacterData
    {
        [SerializeField] private AnimationClip _attackAnimationClip;

        public AnimationClip AttackAnimationClip
        {
            get
            {
                _attackAnimationClip.events = new AnimationEvent[0];
                return _attackAnimationClip;
            }
        }
    }
}
