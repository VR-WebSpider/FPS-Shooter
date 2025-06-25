using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    public Transform player;
    public Transform shootPoint;
    public GameObject bulletPrefab;
    public float detectionRange = 15f;
    public float attackRange = 2f;
    public float safeZoneRadius = 5f;
    public Transform safeZone;
    public float shootCooldown = 2f;
    public float visionAngle = 60f;
    public int bulletDamage = 20;
    public float bulletSpeed = 20f;
    public int enemyHealth = 100;

    private NavMeshAgent agent;
    private int currentPatrolIndex;
    private bool isChasing = false;
    private float lastShootTime;

    // Audio variables
    public AudioClip fireSound;
    public AudioClip deathSound;
    private AudioSource audioSource;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();

        // Auto-add AudioSource if missing
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        GoToNextPatrolPoint();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (safeZone == null)
        {
            safeZone = GameObject.FindGameObjectWithTag("SafeZone").transform;
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        float distanceToSafeZone = Vector3.Distance(player.position, safeZone.position);

        if (distanceToSafeZone <= safeZoneRadius)
        {
            isChasing = false;
            GoToNextPatrolPoint();
        }
        else if (CanSeePlayer() && distanceToPlayer <= detectionRange)
        {
            isChasing = true;
            agent.SetDestination(player.position);
            ShootPlayer();
        }
        else if (distanceToPlayer <= attackRange)
        {
            isChasing = true;
        }
        else if (isChasing)
        {
            isChasing = false;
            GoToNextPatrolPoint();
        }

        if (!isChasing && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint();
        }
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    bool CanSeePlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if (angle < visionAngle / 2)
        {
            if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, detectionRange))
            {
                return hit.collider.CompareTag("Player");
            }
        }
        return false;
    }

    void ShootPlayer()
    {
        if (Time.time - lastShootTime >= shootCooldown)
        {
            lastShootTime = Time.time;
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.linearVelocity = (player.position - shootPoint.position).normalized * bulletSpeed;

            // Play fire sound
            if (fireSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(fireSound);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy death triggered!");
        if (deathSound != null && audioSource != null)
        {
            Debug.Log("Playing enemy death sound...");
            audioSource.PlayOneShot(deathSound);
            agent.isStopped = true;
            GetComponent<Collider>().enabled = false;
            this.enabled = false;

            Destroy(gameObject, deathSound.length);
        }
        else
        {
            Debug.Log("No death sound found! Destroying enemy immediately.");
            Destroy(gameObject);
        }
    }

}
