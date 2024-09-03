using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.animator.SetBool("Walking", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKey(KeyCode.LeftShift)) ExitState(movement, movement.run);
        else if (Input.GetKeyDown(KeyCode.LeftControl)) ExitState(movement, movement.crouch);
        else if (movement.direction.magnitude < 0.1f) ExitState(movement, movement.idle);

        if (movement.direction.magnitude < 0) {
            movement.currentMoveSpeed = movement.walkBackSpeed;
        } else {
            movement.currentMoveSpeed = movement.walkSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement.previousState = this;
            ExitState(movement, movement.jump);
        }
    }

    void ExitState(MovementStateManager movement, MovementBaseState baseState)
    {
        movement.animator.SetBool("Walking", false);
        movement.SwitchState(baseState);
    }
}
