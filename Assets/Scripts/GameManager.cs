using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform player1;
    public Transform player2;
    public GameObject gameOverPanel; // Drag your GameOver panel here

    private PlayerHealth player1Health;
    private PlayerHealth player2Health;

    public GameObject levelUpCanvas;  // Now it's a GameObject

    // Experience points
    public int exPoints = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        player1Health = player1.GetComponent<PlayerHealth>();
        player2Health = player2.GetComponent<PlayerHealth>();

        gameOverPanel.SetActive(false); // Hide panel at start
    }

    void Update()
    {
        if (BothPlayersKnocked())
        {
            GameOver();
        }

        // Example input for leveling up (you can replace it with your actual level-up logic)
        if (Input.GetKeyDown(KeyCode.L)) // Press 'L' to simulate leveling up
        {
            AddXP(1);  // Add 1 XP for testing
        }
    }

    public Transform GetOtherPlayer(Transform current)
    {
        return current == player1 ? player2 : player1;
    }

    bool BothPlayersKnocked()
    {
        return player1Health.isKnockedDown && player2Health.isKnockedDown;
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void AddXP(int amount)
    {
        Instance.exPoints += amount;  // Access instance of GameManager to modify XP

        // Check if XP is enough to level up
        if (Instance.exPoints >= 5)
        {
            Instance.exPoints -= 5;  // Deduct XP after level-up
            Instance.TriggerLevelUp();  // Trigger level-up UI
        }
    }

    // Method to trigger the level-up UI
    void TriggerLevelUp()
    {
        // Enable the levelUpCanvas (GameObject)
        levelUpCanvas.SetActive(true);
    }
    public void hideLevelUp()
    {
        // Enable the levelUpCanvas (GameObject)
        levelUpCanvas.SetActive(false);
    }
}
