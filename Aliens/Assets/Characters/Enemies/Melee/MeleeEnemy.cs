using System.Collections;
using UnityEngine;

namespace Characters.Enemies.Melee
{
    public class MeleeEnemy : Enemy
    {
        protected override IEnumerator AttackTarget()
        {
            isAttacking = true;
            player.TakeDamage((characterData as EnemyData).Damage);
            SetAttackAudioClipAndPlay();

            yield return new WaitForSeconds((characterData as EnemyData).AttackCooldown);
            isAttacking = false;
        }
    }
}
