using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class AmmoSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _weapons;
    [SerializeField] private GameObject _ammoPackPrefab;
    [SerializeField] private GameObject _ammoPackGripPrefab;

    private float _timer;
    private const float MinCooldown = 10f;
    private const float MaxCooldown = 20f;
    private float _cooldown;

    void Start()
    {
        _cooldown = GenerateCooldown();
    }

    private float GenerateCooldown()
    {
        return Random.Range(MinCooldown, MaxCooldown);
    }

    void Update ()
	{
	    if (IsWeaponSlotEmpty())
        {
            _timer += Time.deltaTime;
            if (!(_timer >= _cooldown)) return;

            _timer = 0;
            _cooldown = GenerateCooldown();
            var randomWeapon = PickRandomWeapon();
            var newPosition = transform.position + Vector3.up;
            var spawnedAmmo = Instantiate(_ammoPackPrefab, newPosition, Quaternion.identity);
            spawnedAmmo.transform.parent = gameObject.transform;
            spawnedAmmo.transform.localPosition = _ammoPackGripPrefab.transform.localPosition;
            spawnedAmmo.transform.localRotation = _ammoPackGripPrefab.transform.localRotation;
            spawnedAmmo.transform.localScale = _ammoPackGripPrefab.transform.localScale;
            spawnedAmmo.GetComponent<AmmoPack>().weaponType = randomWeapon;
        }
    }

    private GameObject PickRandomWeapon()
    {
        int randomIndex = Random.Range(0, _weapons.Count);
        return _weapons[randomIndex];
    }

    private bool IsWeaponSlotEmpty()
    {
        return transform.childCount == 0;
    }
}
