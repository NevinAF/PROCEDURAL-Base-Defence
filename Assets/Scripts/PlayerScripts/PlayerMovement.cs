using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    private void Start()
    {
        if (controls.Length != 6)
            throw new System.ArgumentException("PlayerMovement.Start() -- Controls have been impropely set.");
    }
    [Tooltip("This should be Reset Based on control settings. Must be length of 6")]
    public KeyCode[] controls =
        // Default Controls. Should be overritten or used as test.
        new KeyCode[6] { KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S, KeyCode.Space, KeyCode.RightShift };

    public void SetControls(KeyCode left, KeyCode right, KeyCode up, KeyCode down, KeyCode jump, KeyCode sprint)
    {
        controls = new KeyCode[6]
        {
            left,
            right,
            up,
            down,
            jump,
            sprint
        };
        IsJumping = CurrentIsJumping;
        IsSprinting = CurrentIsSprinting;
    }

    private void Update()
    {
        int horizontal = (Input.GetKey(controls[0])) ? -1 : 0;
        horizontal += (Input.GetKey(controls[1])) ? 1 : 0;
        int vertical = (Input.GetKey(controls[2])) ? 1 : 0;
        vertical += (Input.GetKey(controls[3])) ? -1 : 0;

        targetPosition =
            transform.TransformPoint(
                new Vector3(horizontal * speed * sprintMultiplier, 0, vertical * speed * sprintMultiplier));
    }

    private bool CurrentIsJumping()
    {
        return Input.GetKey(controls[4]);
    }

    private bool CurrentIsSprinting()
    {
        return Input.GetKey(controls[5]);
    }
}
