using Characters;
using UnityEngine;

namespace Weapons.Guns.Blaster
{
    public class Blaster : Weapon
    {
        private float _shotDuration;
        private float _damage;
        private float _range;

        private Ray _shootRay;
        private RaycastHit _raycastHit;
        private LineRenderer _lineRenderer;

        protected override void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            InitializeBlasterStats();
        }

        protected override void Update()
        {
            base.Update();
            if (timer >= weaponData.AttackCooldown * _shotDuration)
            {
                _lineRenderer.enabled = false;
            }
        }

        private void InitializeBlasterStats()
        {
            var blasterData = weaponData as BlasterData;
            if (blasterData)
            {
                _shotDuration = blasterData.ShotDuration;
                _damage = blasterData.Damage;
                _range = blasterData.MaxAttackRange;
            }
        }

        protected override void Shoot()
        {
            timer = 0f;

            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, transform.position);

            _shootRay.origin = transform.position;
            _shootRay.direction = transform.forward;

            if (Physics.Raycast(_shootRay, out _raycastHit, _range))
            {
                var enemy = _raycastHit.collider.GetComponent<Enemy>();
                if (enemy)
                {
                    var healthComponent = enemy.GetComponent<Health>();
                    healthComponent.TakeDamage(_damage);
                }

                _lineRenderer.SetPosition(1, _raycastHit.point);
            }
            else
            {
                _lineRenderer.SetPosition(1, _shootRay.origin + _shootRay.direction * _range);
            }
        }
    }
}