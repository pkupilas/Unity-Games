using System.Collections;
using UnityEngine;

namespace Characters.Enemies.Range
{
    public class RangeEnemy : Enemy
    {
        private LineRenderer _lineRenderer;

        protected override void Start()
        {
            base.Start();
            _lineRenderer = GetComponent<LineRenderer>();
        }

        protected override IEnumerator AttackTarget()
        {
            isAttacking = true;
            Shoot();
            yield return new WaitForSeconds((characterData as EnemyData).AttackCooldown);
            isAttacking = false;
            _lineRenderer.enabled = false;
        }

        private void Shoot()
        {
            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, transform.position + Vector3.up);
        
            if (player)
            {
                _lineRenderer.SetPosition(1, player.transform.position + Vector3.up);
                player.TakeDamage((characterData as EnemyData).Damage);
            }
        }
    }
}