using System.Collections;
using System.Collections.Generic;
using Characters.Player;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _weapons;
    [SerializeField] private GameObject _ammoPackPrefab;
    private float _timer;
    private float _cooldown = 5f;

    void Update ()
	{
	    if (IsWeaponSlotEmpty())
        {
            _timer += Time.deltaTime;
            if (!(_timer >= _cooldown)) return;

            _timer = 0;
            var randomWeapon = PickRandomWeapon();
            var newPosition = transform.position + Vector3.up;
            var spawnedAmmo = Instantiate(_ammoPackPrefab, newPosition, Quaternion.identity);
            spawnedAmmo.transform.parent = gameObject.transform;
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
