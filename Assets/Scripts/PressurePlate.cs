using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public static int playersOnPlates = 0;
    public static bool alreadyTriggered = false;

    public GameObject targetToDeactivate; // e.g. a wall (must be active in scene)

    void OnTriggerEnter2D(Collider2D other)
    {
        if (alreadyTriggered) return;

        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            playersOnPlates++;
            CheckDeactivation();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (alreadyTriggered) return;

        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            playersOnPlates--;
        }
    }

    void CheckDeactivation()
    {
        if (playersOnPlates >= 2 && targetToDeactivate != null)
        {
            targetToDeactivate.SetActive(false);
            alreadyTriggered = true;
        }
    }
}
