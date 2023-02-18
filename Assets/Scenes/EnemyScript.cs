using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public GameObject player;

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    public int healPower = 20;
    public int attackPower = 15;


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    // Attack the player
    public void Attack()
    {
        player.GetComponent<PlayerScript>().TakeDamage(10);
        // Animate using leanTween to the left then go back to the original position. Make it ease in and ease out.
        LeanTween.moveX(gameObject, -5, 0.3f).setEaseInOutQuad().setLoopPingPong(1);

        Debug.Log("Enemy attacked");
    }

    // Heal the enemy
    public void Heal(int heal)
    {
        // Check if the player is at max health, and if healing would go over max health set currentHealth to maxHealth
        if (currentHealth + heal > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += heal;
        }

        healthBar.SetHealth(currentHealth);
    }

    // Create a random move for the enemy, whether attack or heal, but if current health is 100, enemy will not heal
    public void RandomMove()
    {
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            player.GetComponent<PlayerScript>().TakeDamage(attackPower);
        }

        if (random == 1 && currentHealth < 100)
        {
            Heal(healPower);
            Attack();
        }
        else
        {
            Attack();
        }
    }
}
