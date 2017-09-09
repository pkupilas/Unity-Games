using System.Collections;
using UnityEngine;

public class Ammunition : MonoBehaviour
{
    [SerializeField] private AmmunitionData _ammunitionData;
    [SerializeField] private ReloadSlider _slider;

    private int _currentAmmoInMagazine;
    private int _currentMagazinesCount;
    public bool IsReloading;
    private float _reloadTime;

    public AmmunitionData AmmunitionData => _ammunitionData;
    public int CurrentAmmoInMagazine => _currentAmmoInMagazine;
    public int CurrentMagazinesCount => _currentMagazinesCount;

    void Start()
    {
        _currentAmmoInMagazine = _ammunitionData.MagazineCapacity;
        _currentMagazinesCount = _ammunitionData.MagazinesCount;
        _reloadTime = _ammunitionData.ReloadTime;
        _slider = FindObjectOfType<ReloadSlider>();
    }

    void OnEnable()
    {
        IsReloading = false;
    }

    public void RemoveBulletFromMagazine()
    {
        if (_currentAmmoInMagazine > 0)
        {
            _currentAmmoInMagazine--;
        }
        else if (_currentMagazinesCount <= 0)
        {
            // Play no ammo sound;
        }
    }

    public void Reload()
    {
        if (!IsReloading)
        {
            StartCoroutine(ManageReload());
        }

    }

    private IEnumerator ManageReload()
    {
        if (_currentMagazinesCount > 0)
        {
            IsReloading = true;
            _slider.ShowSlider();
            _slider.GetComponent<ReloadSlider>().IncreaseValueBy(1 / _reloadTime);
            yield return new WaitForSeconds(_reloadTime);

            IsReloading = false;
            _currentAmmoInMagazine = _ammunitionData.MagazineCapacity;
            _currentMagazinesCount--;
        }
    }

    public bool IsMagazineEmpty()
    {
        return _currentAmmoInMagazine == 0;
    }

    public void AddMagazine()
    {
        _currentMagazinesCount++;
    }
}

