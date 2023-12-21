using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : PhysicsObject
{
    public float MaxSpeed = 7;
    public float JumpTakeOffSpeed = 7;

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            Velocity.y = JumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (Velocity.y > 0)
            {
                Velocity.y *= 0.5f;
            }
        }
        _targetVelocity = move * MaxSpeed;
    }
}
