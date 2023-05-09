using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collideble : MonoBehaviour
{
     
  
  public ContactFilter2D filter;
  private BoxCollider2D boxCollider;
  private Collider2D[] hits = new Collider2D[10];

  protected virtual void Start() {
    boxCollider = GetComponent<BoxCollider2D>();

  }

  protected virtual void Update()
  {
    if (boxCollider == null) return; // Null check added

    //collision work
    boxCollider.OverlapCollider(filter, hits);
    for (int i = 0; i < hits.Length; i++)
    {
        if (hits[i] == null)
            continue;

        OnCollider2D(hits[i]);

        hits[i] = null;
    }
  }
    
    protected virtual void OnCollider2D(Collider2D coll)
    {
       Health health;
    if(health = coll.GetComponent<Health>())
    {
      health.GetHit(1, transform.parent.gameObject);
    }
    }

  


}
