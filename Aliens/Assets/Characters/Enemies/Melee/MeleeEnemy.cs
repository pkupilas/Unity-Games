using System.Collections;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    protected override IEnumerator AttackTarget()
    {
        isAttacking = true;
        player.TakeDamage(enemyData.Damage);
        yield return new WaitForSeconds(enemyData.AttackCooldown);
        isAttacking = false;
    }
}
