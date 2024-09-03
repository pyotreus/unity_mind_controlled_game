using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchingState : MovementBaseState
{
    // Start is called before the first frame update
    public override void EnterState(MovementStateManager movement)
    {
        movement.animator.SetBool("Crouching", true);

    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKey(KeyCode.LeftShift)) ExitState(movement, movement.run);
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (movement.direction.magnitude < 0.1f)
            {
                ExitState(movement, movement.idle);
            } else
            {
                ExitState(movement, movement.walk);
            }
        }

        if (movement.direction.magnitude < 0)
        {
            movement.currentMoveSpeed = movement.crouchBackSpeed;
        }
        else
        {
            movement.currentMoveSpeed = movement.crouchSpeed;
        }
    }

    void ExitState(MovementStateManager movement, MovementBaseState baseState)
    {
        movement.animator.SetBool("Crouching", false);
        movement.SwitchState(baseState);
    }
}
