using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public Transform healthBar; // drag your healthbar object here
    private Vector3 originalScale;

    void Start()
    {
        currentHealth = maxHealth;
        originalScale = healthBar.localScale;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Debug.Log(gameObject.name + " died!");
            // Add death logic if you want
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(2);
        }
    }

    void UpdateHealthBar()
    {
        float healthPercent = (float)currentHealth / maxHealth;
        healthBar.localScale = new Vector3(originalScale.x * healthPercent, originalScale.y, originalScale.z);
    }
}
