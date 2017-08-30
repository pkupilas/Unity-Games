using UnityEngine;

namespace Weapon.Ammunition
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private BulletData _bulletData;

        public BulletData BulletData => _bulletData;
    }
}
