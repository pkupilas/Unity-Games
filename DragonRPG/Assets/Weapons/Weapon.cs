using UnityEngine;

[CreateAssetMenu(menuName = "RPG/Weapon")]
public class Weapon : ScriptableObject
{
    public Transform gripTransform;

    [SerializeField]
    private GameObject _weaponPrefab;
    [SerializeField]
    private AnimationClip _attackAnimationClip;

    public GameObject GetWeaponPrefab()
    {
        return _weaponPrefab;
    }
}
