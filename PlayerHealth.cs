using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    private float lerpTimer;
    public Image frontHealthBar;
    public Image backHealthBar;

    public float damageAmount = 10f; // Damage per interval
    public float healAmount = 15f;   // Healing amount
    public float damageInterval = 1f; // Damage every 1 second while inside hazard zone

    private bool isInHazard = false; // Track if player is inside hazard zone
    private float hazardTimer = 0f;  // Timer to track damage interval

    public DamageScreenEffect damageEffect; // Reference to the script

    // Sound variables
    public AudioClip deathSound;
    private AudioSource audioSource;

    void Start()
    {
        health = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();

        if (isInHazard)
        {
            hazardTimer += Time.deltaTime;
            if (hazardTimer >= damageInterval)
            {
                TakeDamage(damageAmount);
                hazardTimer = 0f;
            }
        }
    }

    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;

        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, hFraction, percentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;

        if (damageEffect != null)
        {
            damageEffect.ShowDamageEffect();
        }

        if (health <= 0)
        {
            Die();
        }

        Debug.Log("Player took damage! Current health: " + health);
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
        Debug.Log("Player healed! Current health: " + health);
    }

    private void Die()
    {
        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        Debug.Log("Player has died!");
        // Add any other death logic here (e.g., game over screen, respawn, etc.)
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hazard"))
        {
            isInHazard = true;
            hazardTimer = 0f;
        }
        else if (other.CompareTag("HealthPickup"))
        {
            RestoreHealth(healAmount);
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hazard"))
        {
            isInHazard = false;
        }
    }
}
