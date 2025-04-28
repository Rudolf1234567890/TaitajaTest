using UnityEngine;
using System.Collections; 
public class SlimeEnemy : MonoBehaviour
{
    public float speed = 3f;
    public int currentHealth;
    public int MaxHealth = 5;
    public float splashRadius = 2f;
    public int splashDamage = 2;
    public float stopDistance = 1.5f; // how close it stops

    public Animator anim;
    public AudioSource audioSource;
    public AudioClip damageSound;

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
            if (distance > stopDistance)
            {
                Vector2 dir = (nearestPlayer.position - transform.position).normalized;
                transform.Translate(dir * speed * Time.deltaTime);
            }
        }

        if (Input.GetKeyDown(KeyCode.N)) // You can trigger the death manually with 'N' key for testing
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
            float dist = Vector2.Distance(slimePos, p.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = p.transform;
            }
        }
        return nearest;
    }

    public void TakeDamage(int amount)
    {
        print("Enemy took " + amount + " damage");
        audioSource.PlayOneShot(damageSound);
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameManager.Instance.AddXP(1);
        // Trigger death animation
        anim.SetTrigger("dead");

        // Start the Coroutine to wait for the animation to finish before destroying the object
        StartCoroutine(DieAfterAnimation());
    }

    private IEnumerator DieAfterAnimation()
    {
        // Get the length of the "dead" animation (this assumes the "dead" trigger uses a specific animation)
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length;

        // Wait for the animation to finish
        yield return new WaitForSeconds(animationDuration);

        // Apply splash damage to players in range
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, splashRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player1") || hit.CompareTag("Player2"))
            {
                hit.GetComponent<PlayerHealth>().TakeDamage(splashDamage);
            }
        }

        // Destroy the slime object after animation is complete
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, splashRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}
