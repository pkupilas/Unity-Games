using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using System.Linq;

public class ChangeWeaponUI : MonoBehaviour
{
    private int _currentWeaponIndex = 0;
    
	// Use this for initialization
	void Start ()
    {
        InitializeUIWeaponImages();
    }

    private void InitializeUIWeaponImages()
    {
        ColorWeaponImages();
    }

    // Update is called once per frame

    void Update()
    {
        HandleScrollWheel();
        ColorWeaponImages();
    }

    private void ColorWeaponImages()
    {
        foreach (Transform weaponImageTransform in transform)
        {
            weaponImageTransform.GetComponent<Image>().color = Color.black;

        }
        transform.GetChild(_currentWeaponIndex).GetComponent<Image>().color = Color.red;
    }

    private void HandleScrollWheel()
    {
        var scrollWheel = CrossPlatformInputManager.GetAxis("Mouse ScrollWheel");
        if (scrollWheel==0) return;
        
        _currentWeaponIndex = scrollWheel > 0 ? _currentWeaponIndex + 1 : _currentWeaponIndex - 1;
        _currentWeaponIndex = _currentWeaponIndex >= transform.childCount ? 0 : _currentWeaponIndex;
        _currentWeaponIndex = _currentWeaponIndex < 0 ? transform.childCount-1 : _currentWeaponIndex;
    }
}
