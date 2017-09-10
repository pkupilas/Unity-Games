using System.Collections;
using UnityEngine;

namespace Characters.Enemies.Range
{
    public class RangeEnemy : Enemy
    {
        private ParticleSystem _particleSystem;

        protected override void Start()
        {
            base.Start();
            _particleSystem = GetComponent<ParticleSystem>();
        }

        protected override IEnumerator AttackTarget()
        {
            isAttacking = true;
            Shoot();

            yield return new WaitForSeconds((characterData as EnemyData).AttackCooldown);
            isAttacking = false;
        }

        private void Shoot()
        {
            if (player)
            {
                player.TakeDamage((characterData as EnemyData).Damage);
                _particleSystem.Play();
                SetAttackAudioClipAndPlay();
            }
        }
    }
}