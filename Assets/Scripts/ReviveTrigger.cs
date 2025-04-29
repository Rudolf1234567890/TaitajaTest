using UnityEngine;

public class ReviveTrigger : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            PlayerHealth knockedPlayer = GetComponentInParent<PlayerHealth>();
            if (knockedPlayer != null && knockedPlayer.isKnockedDown)
            {
                knockedPlayer.StartRevive();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            PlayerHealth knockedPlayer = GetComponentInParent<PlayerHealth>();
            if (knockedPlayer != null)
            {
                knockedPlayer.StopRevive();
            }
        }
    }
}
