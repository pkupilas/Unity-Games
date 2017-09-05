using UnityEngine;

[CreateAssetMenu(menuName = "Armory/Bullets")]
public class BulletData : ScriptableObject
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _velocity;
    [SerializeField] private float _damage;

    public GameObject BulletPrefab => _bulletPrefab;
    public float Velocity => _velocity;
    public float Damage => _damage;
}
