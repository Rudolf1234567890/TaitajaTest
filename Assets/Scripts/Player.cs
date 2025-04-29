using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject rockPrefab;
    public Transform firePoint;
    public KeyCode shootKey;
    public bool hasRock = false;
    public GameObject rockIndicator; // Drag your RockIndicator here

    public float rockSpeed = 10f;
    public int rockDamage = 5;
    public Vector2 rockSize = new Vector2(0.5f, 1f);

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
        Rock rockScript = rock.GetComponent<Rock>();

        rockScript.speed = rockSpeed;  // Use your variables here
        rockScript.damage = rockDamage;

        rock.transform.localScale = new Vector3(rockSize.x, rockSize.y, 1f); // Rock is 2D but needs a 3D scale

        rockScript.Launch(GameManager.Instance.GetOtherPlayer(transform), gameObject.tag);
        hasRock = false;
        rockIndicator.SetActive(false);
    }

    public void GiveRock()
    {
        hasRock = true;
        rockIndicator.SetActive(true);
    }

    public void AddDamage()
    {
        rockDamage = rockDamage + rockDamage;
        GameManager.Instance.hideLevelUp();
    }
    public void AddSpeed()
    {
        rockSpeed += 5;
        GameManager.Instance.hideLevelUp();

    }
    public void AddSize()
    {
        rockSize = rockSize * 2;
        GameManager.Instance.hideLevelUp();

    }
}
