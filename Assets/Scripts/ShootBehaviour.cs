using UnityEngine;

public class ShootBehaviour : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject rockPrefab;
    public GameObject rockIndicatorPrefab;
    
    public KeyCode player1ShootKey = KeyCode.Space;
    public KeyCode player2ShootKey = KeyCode.RightControl;

    private GameObject currentRock;
    private GameObject currentIndicator;
    private GameObject rockHolder;

    public float throwSpeed = 10f;

    private bool rockInTransit = false;

    void Start()
    {
        // Start by giving rock to Player 1
        GiveRockTo(player1);
    }

    void Update()
    {
        if (rockInTransit) return;

        if (rockHolder == player1 && Input.GetKeyDown(player1ShootKey))
        {
            ShootRock(player2);
        }
        else if (rockHolder == player2 && Input.GetKeyDown(player2ShootKey))
        {
            ShootRock(player1);
        }
    }

    void ShootRock(GameObject targetPlayer)
    {
        if (currentRock == null) return;

        rockInTransit = true;
        currentRock.transform.SetParent(null);  // Detach from holder
        Rigidbody2D rb = currentRock.GetComponent<Rigidbody2D>();
        rb.isKinematic = false;

        Vector2 direction = (targetPlayer.transform.position - currentRock.transform.position).normalized;
        rb.linearVelocity = direction * throwSpeed;

        if (currentIndicator != null)
        {
            Destroy(currentIndicator);
        }

        StartCoroutine(WaitForCatch(targetPlayer));
    }

    System.Collections.IEnumerator WaitForCatch(GameObject newHolder)
    {
        yield return new WaitForSeconds(0.3f); // Delay to allow movement

        // Wait until rock is close to target
        while (Vector2.Distance(currentRock.transform.position, newHolder.transform.position) > 0.5f)
        {
            yield return null;
        }

        Rigidbody2D rb = currentRock.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.isKinematic = true;

        GiveRockTo(newHolder);
        rockInTransit = false;
    }

    void GiveRockTo(GameObject newHolder)
    {
        rockHolder = newHolder;

        if (currentRock == null)
        {
            currentRock = Instantiate(rockPrefab);
            currentRock.AddComponent<Rigidbody2D>().gravityScale = 0f;
            currentRock.GetComponent<Rigidbody2D>().isKinematic = true;
        }

        currentRock.transform.SetParent(newHolder.transform);
        currentRock.transform.localPosition = new Vector3(0, 0.5f, 0);

        if (currentIndicator != null)
        {
            Destroy(currentIndicator);
        }

        currentIndicator = Instantiate(rockIndicatorPrefab, newHolder.transform);
        currentIndicator.transform.localPosition = new Vector3(0, 1.2f, 0);
    }
}
