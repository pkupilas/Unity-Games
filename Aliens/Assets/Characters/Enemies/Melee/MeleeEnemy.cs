using System.Collections;
using Characters.Enemies;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    protected override IEnumerator AttackTarget()
    {
        isAttacking = true;
        player.TakeDamage((characterData as EnemyData).Damage);
        audioSource.clip = (characterData as EnemyData).AttackAudioClip;
        audioSource.Play();
        yield return new WaitForSeconds((characterData as EnemyData).AttackCooldown);
        isAttacking = false;
    }
}
