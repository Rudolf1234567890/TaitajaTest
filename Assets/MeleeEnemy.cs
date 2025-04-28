using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public float speed = 3f;
    public float stopDistance = 1.5f;
    public int damage = 1;
    public float damageInterval = 1f;

    private float damageTimer = 0f;
    private Transform targetPlayer;

    [Header("Health Settings")]
    public int currentHealth;
    public int maxHealth = 10;
    public GameObject bloodEffect;


    private void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        targetPlayer = FindNearestPlayer();
        if (targetPlayer == null) return;

        float distance = Vector2.Distance(transform.position, targetPlayer.position);

        if (distance > stopDistance)
        {
            Vector2 dir = (targetPlayer.position - transform.position).normalized;
            transform.Translate(dir * speed * Time.deltaTime);
        }
        else
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                targetPlayer.GetComponent<PlayerHealth>()?.TakeDamage(damage);
                damageTimer = 0f;
            }
        }
    }

    Transform FindNearestPlayer()
    {
        GameObject[] players1 = GameObject.FindGameObjectsWithTag("Player1");
        GameObject[] players2 = GameObject.FindGameObjectsWithTag("Player2");
        GameObject[] allPlayers = new GameObject[players1.Length + players2.Length];
        players1.CopyTo(allPlayers, 0);
        players2.CopyTo(allPlayers, players1.Length);

        Transform nearest = null;
        float minDist = Mathf.Infinity;
        Vector2 myPos = transform.position;

        foreach (GameObject p in allPlayers)
        {
            PlayerHealth health = p.GetComponent<PlayerHealth>();
            if (health != null && !health.isKnockedDown) // <-- use the variable, not a method
            {
                float dist = Vector2.Distance(myPos, p.transform.position);
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
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        print("Enemy took " + amount + " damage");
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        print("Enemy died!");
        Destroy(gameObject);
    }
}
