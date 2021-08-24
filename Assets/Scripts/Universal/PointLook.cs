using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLook : MonoBehaviour
{
    //TODO: Implement max and min PointLook angles
    public enum RotationAxes { X, Y, Z, XY, XZ, YZ, XYZ }

    [Serializable]
    public class RotatingObject
    {
        public Transform go_tansform;
        [HideInInspector()]
        public Vector3 originalUP;
        public RotationAxes axes;

        public RotatingObject(Transform go_tansform, RotationAxes axes)
        {
            originalUP = go_tansform.up;
            this.go_tansform = go_tansform;
            this.axes = axes;
        }

        public void ResetOriginal()
        {
            originalUP = go_tansform.up;
        }
    }
    public List<RotatingObject> rotatingObjects;
    [Tooltip("Degrees per second")]
    public float maxRotation = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -360F;
    public float maximumY = 360F;

    public float frameCounter = 20;

    public Transform target;

    void Update()
    {

        if (target == null)
            return;

        foreach (RotatingObject ro in rotatingObjects)
        {
            Vector3 targetDir = Vector3.RotateTowards(
                ro.go_tansform.forward,
                target.position - ro.go_tansform.position,
                Mathf.Deg2Rad * maxRotation * Time.deltaTime,
                0.0f
                );
            targetDir.Scale(BoundAxes(ro.axes));
            ro.go_tansform.rotation = Quaternion.LookRotation(targetDir, ro.originalUP);
        }
    }

    private static Vector3 BoundAxes(RotationAxes axes)
    {
        switch (axes)
        {
            case RotationAxes.X:
                return Vector3.right;
            case RotationAxes.Y:
                return Vector3.up;
            case RotationAxes.Z:
                return Vector3.forward;
            case RotationAxes.XY:
                return new Vector3(1, 1, 0);
            case RotationAxes.XZ:
                return new Vector3(1, 0, 1);
            case RotationAxes.YZ:
                return new Vector3(0, 1, 1);
            case RotationAxes.XYZ:
                return Vector3.one;
            default:
                throw new ArgumentException("PointLook.BoundAxes() -- Unreconized axis.");
        }
    }

    private void Awake()
    {
        if(rotatingObjects == null)
            rotatingObjects = new List<RotatingObject>();
        foreach (RotatingObject ro in rotatingObjects)
            ro.ResetOriginal();
    }

    public void AddRotatingObject(Transform go_tansform, RotationAxes axes)
    {
        rotatingObjects.Add(new RotatingObject(go_tansform, axes));
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <= 360F))
        {
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }
}
