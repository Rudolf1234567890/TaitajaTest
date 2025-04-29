using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public int playersOnPlates = 0;
    public bool alreadyTriggered = false;

    

    public GameObject targetToDeactivate; // e.g. a wall (must be active in scene)

    void OnTriggerEnter2D(Collider2D other)
    {
        if (alreadyTriggered) return;

        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            GameManager.Instance.playerOnPlates += 1;
            alreadyTriggered = true;
            print("1 on");
            CheckDeactivation();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (alreadyTriggered) return;

        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            GameManager.Instance.playerOnPlates -= 1;
            alreadyTriggered = false;
        }
    }

    void CheckDeactivation()
    {
        playersOnPlates = GameManager.Instance.playerOnPlates;
        print(playersOnPlates);
        if (playersOnPlates >= 2 && targetToDeactivate != null)
        {
            print(playersOnPlates);
            targetToDeactivate.SetActive(false);
            
        }
    }
}
