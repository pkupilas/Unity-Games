using System.Collections;
using UnityEngine;

namespace Weapons
{
    public class Ammunition : MonoBehaviour
    {
        [SerializeField] private AmmunitionData _ammunitionData;
        [SerializeField] private ReloadSlider _reloadSlider;

        private int _currentAmmoInMagazine;
        private int _currentMagazinesCount;
        private float _reloadTime;
        private bool _isReloading;

        public AmmunitionData AmmunitionData => _ammunitionData;
        public int CurrentAmmoInMagazine => _currentAmmoInMagazine;
        public int CurrentMagazinesCount => _currentMagazinesCount;
        public bool IsReloading => _isReloading;

        void Start()
        {
            SetAmmunitionParameters();
            _reloadSlider = FindObjectOfType<ReloadSlider>();
        }

        void OnEnable()
        {
            _isReloading = false;
        }

        private void SetAmmunitionParameters()
        {
            _currentAmmoInMagazine = _ammunitionData.MagazineCapacity;
            _currentMagazinesCount = _ammunitionData.MagazinesCount;
            _reloadTime = _ammunitionData.ReloadTime;
        }

        public void RemoveBulletFromMagazine()
        {
            if (_currentAmmoInMagazine > 0)
            {
                _currentAmmoInMagazine--;
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
                _isReloading = true;
                _reloadSlider.ShowSlider();
                _reloadSlider.GetComponent<ReloadSlider>().IncreaseValueBy(1 / _reloadTime);
                yield return new WaitForSeconds(_reloadTime);

                _isReloading = false;
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
}