using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockbackPlayer : MonoBehaviour
{

    public float thrust;
    public float knocktime;
 
     private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D player = other.GetComponent<Rigidbody2D>();
            if(player != null)
            {
                player.isKinematic = false;
                Vector2 difference = player.transform.position - transform.position; 
                difference = difference.normalized * thrust ;
                player.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(KnockCo(player));
            }
        }
    }

    private IEnumerator KnockCo(Rigidbody2D player)
    {
        if(player != null)
        {
            yield return new WaitForSeconds(knocktime);
            player.velocity = Vector2.zero;
            player.isKinematic = true;
        }
    }
}


