using System.Collections;
using UnityEngine;

public class RangeEnemy : Enemy
{
    private Ray _shootRay;
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
        yield return new WaitForSeconds(enemyData.AttackCooldown);
        isAttacking = false;
        _lineRenderer.enabled = false;
    }

    private void Shoot()
    {
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, transform.position + Vector3.up);

        _shootRay.origin = transform.position;
        _shootRay.direction = transform.forward;

        if (player)
        {
            _lineRenderer.SetPosition(1, player.transform.position + Vector3.up);
            player.TakeDamage(enemyData.Damage);
        }
    }
}