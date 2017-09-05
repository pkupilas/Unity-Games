using UnityEngine;

public class Ammunition : MonoBehaviour
{
    [SerializeField] private AmmunitionData _ammunitionData;
    private int _currentAmmoInMagazine;
    private int _currentMagazinesCount;

    public AmmunitionData AmmunitionData => _ammunitionData;
    public int CurrentAmmoInMagazine => _currentAmmoInMagazine;
    public int CurrentMagazinesCount => _currentMagazinesCount;

    void Start()
    {
        _currentAmmoInMagazine = _ammunitionData.MagazineCapacity;
        _currentMagazinesCount = _ammunitionData.MagazinesCount;
    }

    public void RemoveBulletFromMagazine()
    {
        if (_currentAmmoInMagazine > 0)
        {
            _currentAmmoInMagazine--;
        }
        else if (_currentAmmoInMagazine == 0 && _currentMagazinesCount > 0)
        {
            Reload();
        }
        else if (_currentMagazinesCount < 0)
        {
            // Play no ammo sound;
        }
    }

    public void Reload()
    {
        if (_currentMagazinesCount > 0)
        {
            _currentAmmoInMagazine = _ammunitionData.MagazineCapacity;
            _currentMagazinesCount--;
        }
    }

    public bool IsMagazineEmpty()
    {
        return _currentAmmoInMagazine == 0;
    }
}

