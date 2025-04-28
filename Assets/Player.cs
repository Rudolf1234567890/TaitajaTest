using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject rockPrefab;
    public Transform firePoint;
    public KeyCode shootKey;
    public bool hasRock = false;
    public GameObject rockIndicator; // Drag your RockIndicator here

    void Start()
    {
        rockIndicator.SetActive(hasRock);
    }

    void Update()
    {
        if (hasRock && Input.GetKeyDown(shootKey))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject rock = Instantiate(rockPrefab, firePoint.position, Quaternion.identity);
        rock.GetComponent<Rock>().Launch(GameManager.Instance.GetOtherPlayer(transform), gameObject.tag);
        hasRock = false;
        rockIndicator.SetActive(false);
    }

    public void GiveRock()
    {
        hasRock = true;
        rockIndicator.SetActive(true);
    }
}
