using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject targetPlayer;

    private Vector3 spawnPosition;
    private float spawnRadius;

    private bool isFlying = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Initialize the bullet's spawn and its radius
    public void InitializeBullet(Vector3 playerPosition, float radius)
    {
        spawnPosition = playerPosition;
        spawnRadius = radius;

        // Spawn the rock in a random position around the player within the radius
        Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;
        transform.position = playerPosition + new Vector3(randomPosition.x, randomPosition.y, 0);
    }

    // Start flying towards the target player
    public void StartFlying(Vector3 targetPosition, float force)
    {
        targetPlayer = GameObject.FindWithTag("Player2"); // Assume Player2 is the target (or dynamically set)
        Vector2 direction = (targetPosition - transform.position).normalized;
        rb.bodyType = RigidbodyType2D.Dynamic; // Enable physics
        rb.gravityScale = 0f; // Disable gravity

        // Apply force to sling the rock to the other player
        rb.AddForce(direction * force, ForceMode2D.Impulse);
        isFlying = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If the rock collides with the player, destroy it and change the holder
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject); // Destroy the rock

            // Change the owner of the rock
            ShootBehaviour shootBehaviour = collision.gameObject.GetComponent<ShootBehaviour>();
            shootBehaviour.GiveRockTo(collision.gameObject); // Pass the rock to the other player
        }
    }
}
