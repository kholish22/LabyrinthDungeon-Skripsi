using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : Collideble
{
    //damage
    public int damage;
    //public health Health;
    public float pushForce;
    
    private bool z_Interacted = false;
    private float delay = 0.3f;

    protected override void OnCollider2D(Collider2D coll) {
            
        if(!z_Interacted){
            if (coll.gameObject.tag == "Player")
            {
                z_Interacted = true;          
                Health health;
                    if(health = coll.GetComponent<Health>())
                    {
                    health.GetHit(damage, transform.gameObject);
                    }
                StartCoroutine(DelayDamage());
                
            }
        }    
    }

    private IEnumerator DelayDamage(){
        yield return new WaitForSeconds(delay);
        z_Interacted = false;
    }

}


