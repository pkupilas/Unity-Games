using UnityEngine;

namespace Weapons.Ammunition
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _velocity;

        public float Velocity => _velocity;
    }
}
