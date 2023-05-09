using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldUI : MonoBehaviour
{
    public static GoldUI instance; // Static instance of the GoldUI script, to allow access from other scripts
    public GameObject goldTextObject; // Reference to the game object that contains the Text Mesh Pro component
    public TextMeshProUGUI goldText; // Reference to the Text Mesh Pro component that will display the gold amount
    private int gold = 0; // The player's current gold amount

     void Awake()
    {
        // Set the instance variable to this script
        instance = this;
    }
    

    void Update()
    {
        // Update the gold text to show the current gold amount
        goldText.text = gold.ToString();
    }

    // Method to add gold to the player's inventory
    public void AddGold(int amount)
    {
        gold += amount;
    }
}