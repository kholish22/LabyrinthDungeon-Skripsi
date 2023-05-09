using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDefeated : MonoBehaviour
{
    //gold double the amount
    public int goldAmount = 0; // The amount of gold to grant to the player when the enemy is defeated
    public GameObject floatingPoints;

    void RemoveEnemy()
    {
        
        Destroy(gameObject);
        GameObject points = Instantiate(floatingPoints, transform.position, Quaternion.identity) as GameObject;
        points.transform.GetChild(0).GetComponent<TextMesh>().text = goldAmount + " gold!";
        // Grant the gold to the player and update the gold amount on the UI canvas
        GoldUI.instance.AddGold(goldAmount);
        
        
    }
}
