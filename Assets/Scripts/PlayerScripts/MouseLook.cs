using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }

    [System.Serializable]
    public class RotatingObject
    {
        public Transform go_tansform;
        [HideInInspector()]
        public Quaternion originalRotation;
        public RotationAxes axes;

        public RotatingObject(Transform go_tansform, RotationAxes axes)
        {
            this.go_tansform = go_tansform;
            this.originalRotation = this.go_tansform.localRotation;
            this.axes = axes;
        }

        public void ResetRotation()
        {
            this.originalRotation = this.go_tansform.localRotation;
        }
    }
    public List<RotatingObject> rotatingObjects;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -85F;
    public float maximumY = 85F;

    float rotationX = 0F;
    float rotationY = 0F;

    public float frameCounter = 20;

    void Update()
    {
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationX += Input.GetAxis("Mouse X") * sensitivityX;

        rotationY = ClampAngle(rotationY, minimumY, maximumY);
        rotationX = ClampAngle(rotationX, minimumX, maximumX);

        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
        
        foreach (RotatingObject ro in rotatingObjects)
        {
            if (ro.axes == RotationAxes.MouseXAndY)
            {
                ro.go_tansform.localRotation = ro.originalRotation * xQuaternion * yQuaternion;
            }
            else if (ro.axes == RotationAxes.MouseX)
            {
                ro.go_tansform.localRotation = ro.originalRotation * xQuaternion;
            }
            else
            {
                ro.go_tansform.localRotation = ro.originalRotation * yQuaternion;
            }
        }
    }

    private void Awake()
    {
        if(rotatingObjects == null)
            rotatingObjects = new List<RotatingObject>();

        foreach(RotatingObject ro in rotatingObjects)
        {
            ro.ResetRotation();
        }
    }

    private void OnEnable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
