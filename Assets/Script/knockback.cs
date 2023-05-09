using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockback : MonoBehaviour
{
    
     [SerializeField] 
     private float thrust;
     public float delay = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
     if (collision.gameObject.CompareTag("Enemy"))
        {
         Rigidbody2D enemy = collision.GetComponent<Rigidbody2D>();
        if (enemy != null)
        {
         StartCoroutine(KnockCoroutine(enemy));
        }
         }
    }

         private IEnumerator KnockCoroutine(Rigidbody2D enemy)
    {
          
        Vector2 forceDirection = enemy.transform.position - transform.position;
        Vector2 force = forceDirection.normalized * thrust;

        enemy.velocity = force;
       
        yield return new WaitForSeconds(delay);

         if(enemy != null)
        {
        enemy.velocity = new Vector2();
        }
        
        
 
}
}
