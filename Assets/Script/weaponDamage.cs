using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponDamage : Collideble
{
    private bool z_Interacted = false;
    private float delay = 0.3f;
    public int damage = 1;


    protected override void OnCollider2D(Collider2D coll)
    {
        if (!z_Interacted)
        {
            // Cek apakah object coll memiliki tag "Enemy"
            if (coll.CompareTag("Enemy"))
            {
                z_Interacted = true;

                // Cek apakah object coll memiliki komponen Health
                Health health = coll.GetComponent<Health>();
                if (health != null)
                {
                    health.GetHitEnemy(damage);
                }

                StartCoroutine(DelayDamage());
            }
        }
    }

    private IEnumerator DelayDamage()
    {
        yield return new WaitForSeconds(delay);
        z_Interacted = false;
    }
}