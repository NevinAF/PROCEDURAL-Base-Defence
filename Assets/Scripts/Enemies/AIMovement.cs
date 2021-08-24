using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : Movement
{
    public TargetMovementInfo targetInfo;
    public float SideStepScale = 0.5f;
    public float BackwardsStepScale = 0.0f;

    private bool tryingToJump = false;
    private Vector3 targetMoveDirection;

    private void Update()
    {
        if (Vector3.Distance(targetInfo.target.position, transform.position) < targetInfo.stopToShootDistance)
        {
            targetMoveDirection = Vector3.zero;
        }
        else
        {
            targetMoveDirection =
                ApplySideStep(
                    target: (targetInfo.target.position - transform.position).normalized,
                    forward: transform.forward,
                    scaleAtRight: SideStepScale,
                    scaleAtBack: BackwardsStepScale
                    );
        }
    }

    [Serializable]
    public struct TargetMovementInfo
    {
        public Transform target;
        public float stopToShootDistance;

        public TargetMovementInfo(Transform target, float stopToShootDistance)
        {
            this.target = target;
            this.stopToShootDistance = stopToShootDistance;
        }
    }

    private static Vector3 ApplySideStep(Vector3 target, Vector3 forward, float scaleAtRight, float scaleAtBack)
    {
        Vector3 dif = forward - target;

        target.x *= ApplySideStep(dif.x, scaleAtRight, scaleAtBack);
        target.y *= ApplySideStep(dif.y, scaleAtRight, scaleAtBack);
        target.z *= ApplySideStep(dif.z, scaleAtRight, scaleAtBack);

        return target;
    }

    private static float ApplySideStep(float dif, float scaleAtRight, float scaleAtBack)
    {
        dif = Mathf.Abs(dif);
        if (dif < 1)
        {
            return Mathf.Lerp(1, scaleAtRight, dif);
        }
        else
        {
            return Mathf.Lerp(scaleAtRight, scaleAtBack, dif - 1);
        }
        
    }
}
