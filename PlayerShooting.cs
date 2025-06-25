using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public AudioClip fireSound; // 🔊 Fire sound clip

    private AudioSource audioSource;

    void Start()
    {
        // 🔊 Get or add AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Left mouse click
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // 🔹 Spawn Bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = firePoint.forward * bulletSpeed;

        // 🔊 Play Fire Sound
        if (fireSound != null)
        {
            audioSource.PlayOneShot(fireSound);
        }
    }
}
