using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{

    // Use this for initialization
    private void Start()
    {
        _rigidbody.freezeRotation = true;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = false;

        gameObject.GetComponent<CameraMouseRaycast>().ad
    }

    private void
}
