using System;
using UnityEditor.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Camera
    private new Camera camera;

    // Components
    private new Rigidbody2D rigidbody;
    private new CapsuleCollider2D collider;

    // Stats
    [SerializeField] private PlayerStats playerStats;

    // World state
    public bool grounded { get; private set; }
    public bool jumping { get; private set; }
    public bool sliding => (frameInput.Move.x > 0f && frameVelocity.x < 0f) || (frameInput.Move.x < 0f && frameVelocity.x > 0f);
    public bool running => Mathf.Abs(frameVelocity.x) > 0.25f || Mathf.Abs(frameInput.Move.x) > 0.25f;

    // Events
    public event Action Jumped;
    private FrameInput frameInput;
    private Vector2 frameVelocity;

    private float time;

    private bool cachedQueryStartInColliders;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
        camera = Camera.main;

        cachedQueryStartInColliders = Physics2D.queriesStartInColliders;
    }

    private void OnEnable()
    {
        collider.enabled = true;
        rigidbody.isKinematic = false;
    }

    private void OnDisable()
    {
        collider.enabled = false;
        rigidbody.isKinematic = false;
    }

    private void Update()
    {
        time += Time.deltaTime;
        GatherInput();
    }

    private void GatherInput()
    {

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = horizontalInput != 0 ? Math.Sign(horizontalInput) : 0;
        verticalInput = verticalInput != 0 ? Math.Sign(verticalInput) : 0;
        frameInput = new FrameInput
        {
            JumpDown = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C),
            JumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.C),

            Move = new Vector2(horizontalInput, verticalInput)
        };

        print(Mathf.Sign(Input.GetAxisRaw("Horizontal")));

        if (frameInput.JumpDown)
        {
            jumpToConsume = true;
            timeJumpWasPressed = time;
        }
    }

    private void FixedUpdate()
    {
        // print(frameVelocity);
        CheckCollisions();
        HandleJump();
        HandleDirection();
        HandleGravity();
    }

    #region Collisions

    private float frameLeftGrounded = float.MinValue;

    private void CheckCollisions()
    {
        Physics2D.queriesStartInColliders = false;

        // Ground and Ceiling
        bool groundHit = Physics2D.CapsuleCast(collider.bounds.center, collider.size, collider.direction, 0, Vector2.down, playerStats.GrounderDistance, ~playerStats.PlayerLayer);
        bool ceilingHit = Physics2D.CapsuleCast(collider.bounds.center, collider.size, collider.direction, 0, Vector2.up, playerStats.GrounderDistance, ~playerStats.PlayerLayer);

        // Hit a Ceiling
        if (ceilingHit) frameVelocity.y = Mathf.Min(0, frameVelocity.y);

        // Landed on the Ground
        if (!grounded && groundHit)
        {
            grounded = true;
            coyoteUsable = true;
            bufferedJumpUsable = true;
            endedJumpEarly = false;
        }
        // Left the Ground
        else if (grounded && !groundHit)
        {
            grounded = false;
            frameLeftGrounded = time;
        }

        Physics2D.queriesStartInColliders = cachedQueryStartInColliders;
    }

    #endregion

    #region Jumping

    private bool jumpToConsume;
    private bool bufferedJumpUsable;
    private bool endedJumpEarly;
    private bool coyoteUsable;
    private float timeJumpWasPressed;

    private bool HasBufferedJump => bufferedJumpUsable && time < timeJumpWasPressed + playerStats.JumpBuffer;
    private bool CanUseCoyote => coyoteUsable && !grounded && time < frameLeftGrounded + playerStats.CoyoteTime;

    private void HandleJump()
    {
        if (!endedJumpEarly && !grounded && !frameInput.JumpHeld && rigidbody.velocity.y > 0) endedJumpEarly = true;

        if (!jumpToConsume && !HasBufferedJump) return;

        if (grounded || CanUseCoyote) ExecuteJump();

        jumpToConsume = false;
    }

    private void ExecuteJump()
    {
        endedJumpEarly = false;
        timeJumpWasPressed = 0;
        bufferedJumpUsable = false;
        coyoteUsable = false;
        frameVelocity.y = playerStats.JumpPower;
    }

    #endregion

    #region Horizontal

    private void HandleDirection()
    {
        if (frameInput.Move.x == 0)
        {
            var deceleration = grounded ? playerStats.GroundDeceleration : playerStats.AirDeceleration;
            frameVelocity.x = Mathf.MoveTowards(frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            frameVelocity.x = Mathf.MoveTowards(frameVelocity.x, frameInput.Move.x * playerStats.MaxSpeed, playerStats.Acceleration * Time.fixedDeltaTime);
        }
    }

    #endregion

    #region Gravity

    private void HandleGravity()
    {
        if (grounded && frameVelocity.y <= 0f)
        {
            frameVelocity.y = playerStats.GroundingForce;
        }
        else
        {
            var inAirGravity = playerStats.FallAcceleration;
            if (endedJumpEarly && frameVelocity.y > 0) inAirGravity *= playerStats.JumpEndEarlyGravityModifier;
            frameVelocity.y = Mathf.MoveTowards(frameVelocity.y, -playerStats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
        }
    }

    #endregion

    public struct FrameInput
    {
        public bool JumpDown;
        public bool JumpHeld;
        public Vector2 Move;
    }

}
