using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    public float speed = 3f;
    public int currentHealth;
    public int MaxHealth = 5;
    public float splashRadius = 2f;
    public int splashDamage = 2;
    public float stopDistance = 1.5f;
    public float triggerRange = 5f; // New range for chasing
    public GameObject bloodEffect;

    public AudioSource audioSource;    
    public AudioClip hitSound;

    public GameObject dieEffect;

    private void Start()
    {
        currentHealth = MaxHealth;
    }

    void Update()
    {
        Transform nearestPlayer = FindNearestPlayer();
        if (nearestPlayer != null)
        {
            float distance = Vector2.Distance(transform.position, nearestPlayer.position);

            if (distance <= triggerRange && distance > stopDistance)
            {
                Vector2 dir = (nearestPlayer.position - transform.position).normalized;
                transform.Translate(dir * speed * Time.deltaTime);
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
            Die();
    }

    Transform FindNearestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player1");
        GameObject[] players2 = GameObject.FindGameObjectsWithTag("Player2");
        GameObject[] allPlayers = new GameObject[players.Length + players2.Length];
        players.CopyTo(allPlayers, 0);
        players2.CopyTo(allPlayers, players.Length);

        Transform nearest = null;
        float minDist = Mathf.Infinity;
        Vector2 slimePos = transform.position;

        foreach (GameObject p in allPlayers)
        {
            PlayerHealth health = p.GetComponent<PlayerHealth>();
            if (health != null && !health.isKnockedDown)
            {
                float dist = Vector2.Distance(slimePos, p.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = p.transform;
                }
            }
        }
        return nearest;
    }

    public void TakeDamage(int amount)
    {
        audioSource.PlayOneShot(hitSound);
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameManager.Instance.AddXP(1);
        Instantiate(dieEffect, transform.position, Quaternion.identity);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, splashRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player1") || hit.CompareTag("Player2"))
            {
                hit.GetComponent<PlayerHealth>().TakeDamage(splashDamage);
            }
        }
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, splashRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, triggerRange); // visualize trigger range
    }
}
