using UnityEngine;

[CreateAssetMenu(menuName = "Ammunition")]
public class AmmunitionData : ScriptableObject
{
    [SerializeField] private BulletData _bulletData;
    [SerializeField] private int _magazineCapacity;
    [SerializeField] private int _magazinesCount;

    public BulletData BulletData => _bulletData;
    public int MagazineCapacity => _magazineCapacity;
    public int MagazinesCount => _magazinesCount;
}

