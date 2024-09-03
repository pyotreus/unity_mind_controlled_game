using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : MovementBaseState
{
    // Start is called before the first frame update
    public override void EnterState(MovementStateManager movement)
    {
        movement.animator.SetBool("Running", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKeyUp(KeyCode.LeftShift)) ExitState(movement, movement.walk);
        else if (movement.direction.magnitude < 0.1f) ExitState(movement, movement.idle);

        if (movement.direction.magnitude < 0)
        {
            movement.currentMoveSpeed = movement.runBackSpeed;
        }
        else
        {
            movement.currentMoveSpeed = movement.runSpeed;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement.previousState = this;
            ExitState(movement, movement.jump);
        }
    }

    void ExitState(MovementStateManager movement, MovementBaseState baseState)
    {
        movement.animator.SetBool("Running", false);
        movement.SwitchState(baseState);
    }
}
