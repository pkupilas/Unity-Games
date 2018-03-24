using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewTurretConfig", menuName = "Configs/Turret")]
    public class TurretConfig : ScriptableObject
    {
        [SerializeField] private float _fireRate = 0.5f;
        [SerializeField] private float _range = 2f;
        [SerializeField] private GameObject _ammunitionType;

        public float FireRate => _fireRate;
        public float Range => _range;
        public GameObject AmmunitionTypes => _ammunitionType;
    }
}
