using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunningState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Entering Running State");
    }
    public override void UpdateState(PlayerStateManager player)
    {
        if (Vector2.Distance(player.frameInput.Move, Vector2.zero) < 0.01f)
        {
            player.SwitchState(player.idleState);
        }
    }

    public override void OnCollisionEnter2D(PlayerStateManager player)
    {

    }
}
