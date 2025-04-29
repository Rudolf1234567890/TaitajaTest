using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] public static int playersOnPlates = 0; // Shared counter
    public GameObject targetToActivate;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            playersOnPlates++;
            CheckActivation();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            playersOnPlates--;
            CheckActivation();
        }
    }

    void CheckActivation()
    {
        if (targetToActivate != null)
            targetToActivate.SetActive(playersOnPlates >= 2);
    }
}
