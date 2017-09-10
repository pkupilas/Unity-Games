using UnityEngine;
using UnityEngine.UI;

public class GunImage : MonoBehaviour
{
    [SerializeField] private WeaponData _weaponData;
    private Image _image;

    void Start()
    {
        _image = GetComponent<Image>();
        _image.sprite = _weaponData.WeaponSprite;
    }

    public WeaponData WeaponData => _weaponData;
}
