using UnityEngine;

namespace Weapons.Bullets
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private BulletData _bulletData;

        public BulletData BulletData => _bulletData;
    }
}

