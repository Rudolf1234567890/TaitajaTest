using UnityEngine;

public class Rock : MonoBehaviour
{
    public float speed = 10f;
    private Transform targetPlayer;
    private string currentHolderTag;
    public int damage = 5;
    public AudioSource audioSource;    
    public AudioClip hitSound;

    public void Launch(Transform target, string holderTag)
    {
        targetPlayer = target;
        currentHolderTag = holderTag;
    }

    void Update()
    {
        if (targetPlayer != null)
        {
            Vector2 dir = (targetPlayer.position - transform.position).normalized;
            transform.Translate(dir * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            if (other.tag != currentHolderTag)
            {
                other.GetComponent<Player>().GiveRock();
                Destroy(gameObject);
            }
        }
        
        if (other.CompareTag("Slime"))
        {
            audioSource.PlayOneShot(hitSound);
            other.GetComponent<SlimeEnemy>().TakeDamage(damage);
        }

        if (other.CompareTag("MeleeEnemy"))
        {
            audioSource.PlayOneShot(hitSound);
            other.GetComponent<MeleeEnemy>().TakeDamage(damage);
        }
    }
}
