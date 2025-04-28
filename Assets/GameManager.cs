using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance;

    // Player references
    public Transform player1;
    public Transform player2;

    // GameObject for level-up UI
    public GameObject levelUpCanvas;  // Now it's a GameObject

    // Experience points
    public int exPoints = 0;

    // Awake is used to ensure there's only one instance of GameManager
    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to get the other player
    public Transform GetOtherPlayer(Transform current)
    {
        return current == player1 ? player2 : player1;
    }

    // Static method to add XP and handle level-up
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

    void Update()
    {
        // Example input for leveling up (you can replace it with your actual level-up logic)
        if (Input.GetKeyDown(KeyCode.L)) // Press 'L' to simulate leveling up
        {
            AddXP(1);  // Add 1 XP for testing
        }
    }
}
