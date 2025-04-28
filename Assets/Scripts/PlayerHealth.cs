using UnityEngine;
using TMPro; // Needed for TextMeshPro if you want countdown text

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public Transform healthBar;
    private Vector3 originalScale;

    public bool isKnockedDown = false;
    private bool isBeingRevived = false;
    private float reviveTimer = 3f;
    private float reviveCountdown;

    public TextMeshProUGUI reviveText; // Drag your TMP text here
    public GameObject downPanel;
    public PlayerMovement pm;

    void Start()
    {
        currentHealth = maxHealth;
        originalScale = healthBar.localScale;
        reviveText.gameObject.SetActive(false);
        downPanel.SetActive(false);
    }

    void Update()
    {
        if (isBeingRevived && isKnockedDown)
        {
            reviveCountdown -= Time.deltaTime;
            reviveText.text = "" + Mathf.Ceil(reviveCountdown).ToString();

            if (reviveCountdown <= 0f)
            {
                Revive();
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if (isKnockedDown) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            KnockDown();
        }
    }

    void UpdateHealthBar()
    {
        float healthPercent = (float)currentHealth / maxHealth;
        healthBar.localScale = new Vector3(originalScale.x * healthPercent, originalScale.y, originalScale.z);
    }

    void KnockDown()
    {
        pm.enabled = false;
        isKnockedDown = true;
        downPanel.SetActive(true);
        reviveText.gameObject.SetActive(false);
        Debug.Log(gameObject.name + " is knocked down!");
        // Optional: disable movement script here
    }

    public void StartRevive()
    {
        if (isKnockedDown)
        {
            isBeingRevived = true;
            reviveCountdown = reviveTimer;
            reviveText.gameObject.SetActive(true);
        }
    }

    public void StopRevive()
    {
        isBeingRevived = false;
        reviveText.gameObject.SetActive(false);
    }

    void Revive()
    {
        pm.enabled = true;
        isKnockedDown = false;
        downPanel.SetActive(false);
        isBeingRevived = false;
        currentHealth = maxHealth / 2; // Revive with half health
        UpdateHealthBar();
        reviveText.gameObject.SetActive(false);
        Debug.Log(gameObject.name + " has been revived!");
        // Optional: enable movement script again
    }
}
