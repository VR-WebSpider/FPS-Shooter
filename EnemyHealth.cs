using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthBar; // Assign in Inspector

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar(); // Set initial value
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth / maxHealth;
        }
        else
        {
            Debug.LogWarning("Health Bar is not assigned in the Inspector!");
        }
    }

    void Die()
    {
        Debug.Log("Enemy Died");
        Destroy(gameObject);
        FindFirstObjectByType<EnemySpawner>()?.EnemyDied();
    }
}
