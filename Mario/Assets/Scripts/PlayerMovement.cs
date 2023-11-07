using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Camera
    private new Camera camera;

    // Componetns
    private new Rigidbody2D rigidbody;
    private new Collider2D collider;

    // Player Characteristics
    //// Speed and movement
    public float moveSpeed = 8f;
    //// Jump
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 2f;
    public float jumpForce => 4f * maxJumpHeight;
    private float jumpDone;
    //// Gravity
    public float gravity => -8f * maxJumpHeight;
    //// World state
    public bool grounded { get; private set; }
    public bool jumping { get; private set; }
    public bool sliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f);
    public bool running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f;
    private Vector2 velocity;

    // Input
    private float inputAxis;




    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        camera = Camera.main;
    }

    private void OnEnable()
    {
        collider.enabled = true;
        rigidbody.isKinematic = false;
        velocity = Vector2.zero;
        jumping = false;
        jumpDone = 0f;
    }

    private void OnDisable()
    {
        collider.enabled = false;
        rigidbody.isKinematic = false;
        velocity = Vector2.zero;
        jumping = false;

    }

    private void Update()
    {
        HorizontalMovement();
        VerticalMovement();

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

    }

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, 2 * moveSpeed * Time.deltaTime);

        if (rigidbody.Raycast(Vector2.right * velocity.x))
        {
            velocity.x = 0f;
        }

        if (velocity.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (velocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

    }

    private void VerticalMovement()
    {
        //grounded = rigidbody.Raycast(Vector2.down);

        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;
        velocity.y += multiplier * gravity * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
        if (rigidbody.Raycast(Vector2.down))
        {
            velocity.y = Mathf.Max(velocity.y, 0f);
            jumping = velocity.y > 0f;
        }
    }

    private void Jump()
    {
        grounded = rigidbody.Raycast(Vector2.down);
        if (grounded) { jumpDone = 0; }

        if (jumpDone < maxJumpTime)
        {
            jumpDone++;
            velocity.y = jumpForce;
            jumping = true;
            grounded = false;
        }

    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);

        rigidbody.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (transform.DotTest(collision.transform, Vector2.down))
            {
                velocity.y = jumpForce / 2f;
                jumping = true;
            }
        }

        else if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            if (transform.DotTest(collision.transform, Vector2.up))
            {
                velocity.y = 0f;
            }
        }
    }

}
