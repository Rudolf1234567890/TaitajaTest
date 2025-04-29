using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement = Vector2.zero;

        if (Input.GetKey(moveUp))    movement.y = 1;
        if (Input.GetKey(moveDown))  movement.y = -1;
        if (Input.GetKey(moveLeft))  movement.x = -1;
        if (Input.GetKey(moveRight)) movement.x = 1;

        movement = movement.normalized;

        UpdateAnimation(movement);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * moveSpeed;
    }

    void UpdateAnimation(Vector2 dir)
    {
        if (dir.magnitude == 0)
        {
            animator.Play("idle_player");
            return;
        }

        float angle = Vector2.SignedAngle(Vector2.up, dir);

        if (angle >= -45 && angle <= 45)
        {
            animator.Play("walk_up");
        }
        else if (angle > 45 && angle < 135)
        {
            animator.Play("walk_sideways");
            transform.localScale = new Vector3(1, 1, 1); // facing right
        }
        else if (angle < -45 && angle > -135)
        {
            animator.Play("walk_sideways");
            transform.localScale = new Vector3(-1, 1, 1); // facing left
        }
        else
        {
            animator.Play("walk_down");
        }
    }
}
