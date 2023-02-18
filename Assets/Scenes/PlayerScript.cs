using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// TextMeshPro is a Unity package that allows you to use text in 3D space
using TMPro;

public class PlayerScript : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    public int maxMana = 10;
    public int currentMana;

    public HealthBar healthBar;

    // TextMeshProUGUI is a Unity package that allows you to use text in 3D space
    public TextMeshProUGUI manaText;

    void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        manaText.text = currentMana.ToString();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

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
}
