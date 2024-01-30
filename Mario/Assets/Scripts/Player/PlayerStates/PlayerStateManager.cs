using UnityEngine;
using System;

public class PlayerStateManager : MonoBehaviour
{
    PlayerBaseState currentState;
    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerRunningState runningState = new PlayerRunningState();

    public FrameInput frameInput;

    // Start is called before the first frame update
    void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        GatherInput();
        currentState.UpdateState(this);
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    private void GatherInput()
    {

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = horizontalInput != 0 ? Math.Sign(horizontalInput) : 0;
        verticalInput = verticalInput != 0 ? Math.Sign(verticalInput) : 0;

        frameInput = new FrameInput
        {
            JumpDown = Input.GetButtonDown("Jump"),
            JumpHeld = Input.GetButton("Jump"),

            Move = new Vector2(horizontalInput, verticalInput),
            Attack = Input.GetKey(KeyCode.C),
            ChangeWeapon = Input.GetKeyDown(KeyCode.X)
        };
    }

    public struct FrameInput
    {
        public bool JumpDown;
        public bool JumpHeld;
        public Vector2 Move;
        public bool Attack;
        public bool ChangeWeapon;
    }
}
