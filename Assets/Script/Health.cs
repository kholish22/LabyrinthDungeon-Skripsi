using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int currentHealth, maxHealth;
    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    [SerializeField]
    private bool isDead = false;

    public healthBar healthBar;
    Animator animator;

    //Menginisialisasi Health
    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        healthBar.SetMaxHealth(maxHealth);
        isDead = false;
    }

    //Get Hit Logic
    public void GetHit(int amount, GameObject sender)
    {
         //isDead true maka tidak melakukan apa2
    if (isDead)
        return;

    //mencegah agar tidak hit diri sendiri di layer
    if (sender.layer == gameObject.layer)
        return;

    // Check if the sender is an enemy
    if (sender.CompareTag("Enemy"))
    {
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);

        if (currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
        }
        else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            Destroy(gameObject);
        }
    }
    }

    //Get Hit Logic Enemy
    public void GetHitEnemy(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            isDead = true;
            Defeated();
        }
    }

    // Method untuk menambahkan health pada player
    public void AddHealth(int healthBoost)
    {
        int health = Mathf.RoundToInt((float)currentHealth / maxHealth);
        int val = health + healthBoost;
        currentHealth = Mathf.Min(currentHealth + healthBoost, maxHealth);
        healthBar.SetHealth(currentHealth);
    }


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Defeated()
    {
        animator.SetTrigger("Defeated");
    }

    //Metode untuk mendeteksi ketika collider musuh masuk ke dalam collider GameObject
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //mendapatkan skrip Health dari musuh
            Health enemyHealth = other.GetComponent<Health>();

            //menyerang musuh dengan GetHitEnemy
            enemyHealth.GetHitEnemy(10);
        }
    }
}