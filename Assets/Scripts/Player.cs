using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject rockPrefab;
    public Transform firePoint;
    public KeyCode shootKey;
    public bool hasRock = false;
    public GameObject rockIndicator;

    public float rockSpeed = 10f;
    public int rockDamage = 5;
    public Vector2 rockSize = new Vector2(0.5f, 1f);
    private AudioSource audioSource;
    public AudioClip throwSound;

    private Animator animator;

    void Start()
    {
        rockIndicator.SetActive(hasRock);
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (hasRock && Input.GetKeyDown(shootKey))
        {
            audioSource.PlayOneShot(throwSound);
            PlayShootAnimation();
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject rock = Instantiate(rockPrefab, firePoint.position, Quaternion.identity);
        Rock rockScript = rock.GetComponent<Rock>();

        rockScript.speed = rockSpeed;
        rockScript.damage = rockDamage;

        rock.transform.localScale = new Vector3(rockSize.x, rockSize.y, 1f);

        rockScript.Launch(GameManager.Instance.GetOtherPlayer(transform), gameObject.tag);
        hasRock = false;
        rockIndicator.SetActive(false);
    }

    void PlayShootAnimation()
    {
        Vector2 shootDir = GameManager.Instance.GetOtherPlayer(transform).position - transform.position;
        float angle = Vector2.SignedAngle(Vector2.up, shootDir);

        if (angle >= -45 && angle <= 45)
        {
            animator.Play("throw_up");
        }
        else if (angle > 45 && angle < 135)
        {
            animator.Play("throw_sideways");
            transform.localScale = new Vector3(1, 1, 1); // facing right
        }
        else if (angle < -45 && angle > -135)
        {
            animator.Play("throw_sideways");
            transform.localScale = new Vector3(-1, 1, 1); // facing left
        }
        else
        {
            animator.Play("throw_down");
        }
    }

    public void GiveRock()
    {
        hasRock = true;
        rockIndicator.SetActive(true);
    }

    public void AddDamage()
    {
        rockDamage += rockDamage;
        GameManager.Instance.hideLevelUp();
    }

    public void AddSpeed()
    {
        rockSpeed += 5;
        GameManager.Instance.hideLevelUp();
    }

    public void AddSize()
    {
        rockSize *= 2;
        GameManager.Instance.hideLevelUp();
    }
}
