using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Ammunition")]
public class AmmunitionData : ScriptableObject
{
    [SerializeField] private BulletData _bulletData;
    [SerializeField] private int _magazineCapacity;
    [SerializeField] private int _magazinesCount;
    [SerializeField] private float _reloadTime;

    public BulletData BulletData => _bulletData;
    public int MagazineCapacity => _magazineCapacity;
    public int MagazinesCount => _magazinesCount;
    public float ReloadTime => _reloadTime;
}

