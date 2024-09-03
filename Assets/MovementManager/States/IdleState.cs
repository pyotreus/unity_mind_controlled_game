using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MovementBaseState
{
    // Start is called before the first frame update
    public override void EnterState(MovementStateManager manager)
    {

    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (movement.direction.magnitude > 0.1f) { 
            if (Input.GetKey(KeyCode.LeftShift))
            {
                movement.SwitchState(movement.run);
            } else
            {
                movement.SwitchState(movement.walk);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            movement.SwitchState(movement.crouch);  
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement.previousState = this;
            movement.SwitchState(movement.jump);
        }
        
    }
}
