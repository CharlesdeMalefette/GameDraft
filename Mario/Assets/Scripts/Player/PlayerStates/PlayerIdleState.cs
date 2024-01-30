using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Entering Idle State");
    }
    public override void UpdateState(PlayerStateManager player)
    {
        if (Vector2.Distance(player.frameInput.Move, Vector2.zero) > 0.01f)
        {
            player.SwitchState(player.runningState);
        }
    }
    public override void OnCollisionEnter2D(PlayerStateManager player)
    {

    }
}
