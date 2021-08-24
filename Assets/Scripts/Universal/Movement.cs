using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Movement : MonoBehaviour
{
    /*
     * TODO: MOVEMENT
     * Add "pathfinding"
     * Add sight
     * Add auto jump
     * Maybe fix jump/sprint being a function   
     */
    public Func<bool> IsJumping = Functions.False;
    public Func<bool> IsSprinting = Functions.False;
    
    [Tooltip("Where the object is trying to get to. Update this for dynamic movement.")]
    public Vector3 targetPosition;
    [Tooltip("Distance in world units per second. Only effects how fast this can move on its own.")]
    public float speed = 6.0f;
    [Tooltip("Default is twice gravity * 0.02.")]
    public float gravity = -19.8f;
    [Tooltip("Max speed while falling.")]
    public float fallSpeed = 99;
    [Tooltip("The acceleration of object in seconds.")]
    public float maxVelocityChange = 30.0f;
    [Range(0, 1)]
    [Tooltip("The Slope required to reset jump count and to stop sliding.")]
    public float maxGroundSlope = 0.98f;
    [Tooltip("Height of each jump in world units.")]
    public float jumpHeight = 1.0f;
    [Tooltip("Amount of Jumps from ground until touching ground again.")]
    public int numberOfJumps = 1;
    [Tooltip("Multiply Speed by XXX while sprinting.")]
    public float sprintMultiplier = 1.30f;
    //TODO: Look Modifier on movement. For sprint too?
 /* public bool LookDirectionModifier = false; 
    public float InnerAngle = 0f;
    public float OuterAngle = 360f; */
    //TODO: Look Modifier function for movement;

    private bool grounded = false;
    private float actualSpeed; // Speed after spring/look direction calculated
    private int jumpCounter = 0;
    private Rigidbody _rigidbody;
    private Vector3 differenceInPostion, targetDirection, targetVelocity;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = false;
    }

    void FixedUpdate()
    {
        Vector3 velocity = _rigidbody.velocity;
        Vector3 velocityChange;
        /* Calculate Actual Speed */

        actualSpeed = speed;
        if (IsSprinting())
            actualSpeed *= sprintMultiplier;

        /* UPDATE TARGET DIRECTION */

        differenceInPostion = targetPosition - transform.position;
        targetDirection = Vector3.Normalize(differenceInPostion);
        
        /* UPDATE TARGET SPEED */

        if (actualSpeed < Vector3.Magnitude(differenceInPostion))
        {
            targetVelocity = targetDirection * actualSpeed;
        }
        else
        {
            targetVelocity = differenceInPostion;
        }

        // Apply a force that attempts to reach our target velocity
        velocityChange = (targetVelocity - velocity);
        velocityChange = ClampChangeInVelocity(velocityChange, maxVelocityChange * Time.fixedDeltaTime);

        /* JUMP! Handeling */

        if (jumpCounter > 0 || (grounded))
        {
            // Jump
            if (IsJumping())
            {
                if (!grounded)
                    jumpCounter--;
                _rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
            }

        }

        /* Apply Gravity unless on ground */
        if (!grounded)
        {
            float g = Math.Abs(gravity * Time.fixedDeltaTime);
            velocityChange.y = 
                Mathf.Clamp(-(velocity.y + fallSpeed), -g, g);
        }
        else
        {
            velocityChange.y = 0;
        }

        _rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
        grounded = false;
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        // Debug-draw all contact points and normals
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            if (contact.normal.y >= maxGroundSlope)
            {
                grounded = true;
                // The fist jump off the ground does not reduce jump count. Prevents extra jump in air.
                jumpCounter = numberOfJumps - 1;
                return;
            }
        }
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * -gravity);
    }

    Vector3 ClampChangeInVelocity(Vector3 velocity, float max)
    {
        float d = Mathf.Sqrt(Mathf.Pow(velocity.x, 2) + Mathf.Pow(velocity.z, 2));

        if (d <= max)
            return velocity;
        else
        {
            d = max;
            float x = d * Mathf.Cos(Mathf.Atan(velocity.z / velocity.x));
            float z = d * Mathf.Cos(Mathf.Atan(velocity.x / velocity.z));

            if (velocity.x < 0)
                x = -x;
            if (velocity.z < 0)
                z = -z;

            return new Vector3(x, velocity.y, z);
        }
    }
}
