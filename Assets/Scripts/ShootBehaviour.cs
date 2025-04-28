using UnityEngine;

public class ShootBehaviour : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject rockPrefab; // The rock prefab to spawn
    public GameObject rockIndicatorPrefab; // Visual indicator for who has the rock

    public KeyCode player1ShootKey = KeyCode.Space;
    public KeyCode player2ShootKey = KeyCode.RightControl;

    public float throwForce = 10f;
    public float catchDistance = 0.5f; // Distance to catch the rock
    public float rockSpawnRadius = 1f; // The radius around the player where the rock will spawn

    private GameObject currentRock;
    private GameObject currentIndicator;

    private GameObject rockHolder;

    private bool rockInTransit = false;

    void Start()
    {
        // Player 1 starts with the rock
        GiveRockTo(player1);
    }

    void Update()
    {
        if (rockInTransit) return;

        // Player 1 throws the rock towards Player 2
        if (rockHolder == player1 && Input.GetKeyDown(player1ShootKey))
        {
            ShootRock(player2);
        }
        // Player 2 throws the rock towards Player 1
        else if (rockHolder == player2 && Input.GetKeyDown(player2ShootKey))
        {
            ShootRock(player1);
        }
    }

    void ShootRock(GameObject targetPlayer)
    {
        if (currentRock == null) return;

        rockInTransit = true;

        // Detach the rock from the holder
        currentRock.transform.SetParent(null);
        Bullet bullet = currentRock.GetComponent<Bullet>(); // Reference to the Bullet script
        bullet.StartFlying(targetPlayer.transform.position, throwForce); // Start slinging the rock

        // Remove the indicator for the player who no longer has the rock
        if (currentIndicator != null)
        {
            Destroy(currentIndicator);
        }

        // Wait until the rock is caught by the other player
        StartCoroutine(WaitForCatch(targetPlayer));
    }

    System.Collections.IEnumerator WaitForCatch(GameObject newHolder)
    {
        yield return new WaitForSeconds(0.3f); // Short delay before checking the catch

        // Wait until the rock is near the target player (within catch distance)
        while (Vector2.Distance(currentRock.transform.position, newHolder.transform.position) > catchDistance)
        {
            yield return null;
        }

        // Once caught, stop the rock and set it to be kinematic again
        Rigidbody2D rb = currentRock.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero; // Stop the rock
        rb.bodyType = RigidbodyType2D.Kinematic; // Disable physics, make the rock stationary

        GiveRockTo(newHolder);
        rockInTransit = false; // The rock is no longer in transit
    }

    public void GiveRockTo(GameObject newHolder)
    {
        rockHolder = newHolder;

        // Only instantiate the rock if it's not already present
        if (currentRock == null)
        {
            currentRock = Instantiate(rockPrefab);
            currentRock.GetComponent<Bullet>().InitializeBullet(player1.transform.position, rockSpawnRadius); // Initialize the rock spawn
            currentRock.GetComponent<Rigidbody2D>().gravityScale = 0f;
            currentRock.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }

        currentRock.transform.SetParent(newHolder.transform);
        currentRock.transform.localPosition = new Vector3(0, 0.5f, 0); // Position the rock above the player

        // Add a visual indicator
        if (currentIndicator != null)
        {
            Destroy(currentIndicator);
        }

        currentIndicator = Instantiate(rockIndicatorPrefab, newHolder.transform);
        currentIndicator.transform.localPosition = new Vector3(0, 1.2f, 0);
    }
}
