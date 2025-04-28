using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform player1;
    public Transform player2;

    void Awake()
    {
        Instance = this;
    }

    public Transform GetOtherPlayer(Transform current)
    {
        return current == player1 ? player2 : player1;
    }
}
