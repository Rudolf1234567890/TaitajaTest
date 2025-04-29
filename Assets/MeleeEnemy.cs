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

    private Animator animator;
    private Vector2 lastPosition;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
    }

    void Update()
    {
        targetPlayer = FindNearestPlayer();
        if (targetPlayer == null) return;

        float distance = Vector2.Distance(transform.position, targetPlayer.position);
        Vector2 dirToPlayer = (targetPlayer.position - transform.position).normalized;

        // Calculate movement direction
        Vector2 movement = (Vector2)transform.position - lastPosition;
        lastPosition = transform.position;

        if (distance > stopDistance)
        {
            transform.Translate(dirToPlayer * speed * Time.deltaTime);

            // Walking animation based on movement direction
            if (movement.magnitude > 0.01f)
            {
                float moveAngle = Vector2.SignedAngle(Vector2.up, movement);

                if (moveAngle >= -45 && moveAngle <= 45)
                {
                    animator.Play("walk_backwards");
                }
                else if (moveAngle > 45 && moveAngle < 135)
                {
                    animator.Play("walks_sideways");
                    transform.localScale = new Vector3(2f, transform.localScale.y, transform.localScale.z);
                }
                else if (moveAngle < -45 && moveAngle > -135)
                {
                    animator.Play("walks_sideways");
                    transform.localScale = new Vector3(-2f, transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    animator.Play("vampire_walk_forward");
                }
            }
            else
            {
                animator.Play("vamp_idle");
            }
        }
        else
        {
            // Attack logic
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                targetPlayer.GetComponent<PlayerHealth>()?.TakeDamage(damage);
                damageTimer = 0f;

                // Attack animation based on direction to player
                float attackAngle = Vector2.SignedAngle(Vector2.up, dirToPlayer);

                if (attackAngle >= -45 && attackAngle <= 45)
                {
                    animator.Play("attack_backwards");
                }
                else if (attackAngle > 45 && attackAngle < 135)
                {
                    animator.Play("vampire_attack_sideways");
                    transform.localScale = new Vector3(2f, transform.localScale.y, transform.localScale.z);
                }
                else if (attackAngle < -45 && attackAngle > -135)
                {
                    animator.Play("vampire_attack_sideways");
                    transform.localScale = new Vector3(-2f, transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    animator.Play("vampire_attack_forward");
                }
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
            if (health != null && !health.isKnockedDown)
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
