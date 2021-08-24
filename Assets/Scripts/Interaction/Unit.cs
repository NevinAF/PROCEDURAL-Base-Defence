using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(TeamLayer))]
[RequireComponent(typeof(WeaponSlot))]
public abstract class Unit : MonoBehaviour
{
    public Rigidbody _rigidbody { get; private set; }
    public Collider _collider { get; private set; }
    public Health _health { get; private set; }
    public TeamLayer _team { get; private set; }
    public WeaponSlot _weaponSlot { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _health = GetComponent<Health>();
        _team = GetComponent<TeamLayer>();
        _weaponSlot = GetComponent<WeaponSlot>();
    }
}
