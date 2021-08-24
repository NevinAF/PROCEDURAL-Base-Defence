using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Unit
{
    public GameObject OnStartWeapon;
    // Use this for initialization
    private void Start()
    {
        _rigidbody.freezeRotation = true;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = false;

        //GetComponent<TargetObject>().target = GameObject.Find("PlayerWorldObject").transform;

        _weaponSlot.SetWeapon(OnStartWeapon);

        _health.AddDeathListener(OnDeath);
    }

    public void OnDeath()
    {
        Functions.RagDollGameObject(gameObject);
        ShrinkAndDestroy sad = gameObject.AddComponent<ShrinkAndDestroy>();
        sad.shrinkTime = 5;
    }
}
