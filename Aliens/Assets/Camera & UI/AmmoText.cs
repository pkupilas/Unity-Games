using System.Collections;
using System.Collections.Generic;
using Assets.Weapons.Guns;
using Characters.Player;
using UnityEngine;
using Weapons.Guns.Blaster;
using UnityEngine.UI;

public class AmmoText : MonoBehaviour
{
    private Player _player;
    private Text _text;
    
	void Start ()
	{
	    _player = FindObjectOfType<Player>();
	    _text = GetComponent<Text>();
	}
	
	void LateUpdate ()
	{
	    UpdateAmmoText();
	}

    private void UpdateAmmoText()
    {
        var activeWeapon = _player.GetComponentInChildren<Weapon>();
        if (activeWeapon.GetComponent<EnergyWeapon>())
        {
            _text.text = "\u221E\n\u221E";
        }
        else if(activeWeapon.GetComponent<Firearm>())
        {
            var ammunition = activeWeapon.GetComponent<Ammunition>();
            _text.text = $"{ammunition.CurrentAmmoInMagazine}\n{ammunition.CurrentMagazinesCount}";
        }
    }
}
