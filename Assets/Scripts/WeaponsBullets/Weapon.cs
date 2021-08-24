using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Only handels doing an attack with the weapon. Need
 * other scrips to control transform and if it should shoot.
 * 
 * Shoot direction is based on the direction of the weapon
 * holder that the weapon is put in.
 * 
 * IsTrying to attack is a predicate that should be overritten
 * if the weapon shouldn't be firing all the time.
 */

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
    public bool DestroyOnDrop = true;
    
    public float CooldownCount { get; private set; }

    public GameObject Owner { get; private set; }
    public Collider _collider { get; private set; }
    public Rigidbody _rigidbody { get; private set; }
    private Func<bool> IsTryingToAttack;

    // Use this for initialization
    private void Awake ()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();

        CooldownCount = weaponData.attackRate;
    }

    private void OnEnable()
    {
        _collider.enabled = false;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;

        if (IsTryingToAttack == null)
        {
            IsTryingToAttack = () => { return true; };
        }
    }


    //TODO: Weapon: if disable on start then do OnDisable
    private void OnDisable()
    {
        Functions.RagDollGameObject(gameObject);

        if (DestroyOnDrop)
        {
            ShrinkAndDestroy sad = gameObject.AddComponent<ShrinkAndDestroy>();
            sad.SetupVars(5);
            Destroy(this);
        }
        else
        {
            SetOwner(null);
        }
    }
    
    private void Update ()
    {
        if (CooldownCount > 0)
        {
            CooldownCount -= Time.deltaTime;
        }
        else
        {
            if (IsTryingToAttack())
            {
                Attack();
                CooldownCount = weaponData.attackRate;
            }
        }
    }

    public bool IsWithinRangeOfThis(Vector3 targetPosition)
    {
        return (weaponData.Range > Vector3.Distance(targetPosition, transform.position));
    }

    public void OverrideIsTryingToAttack(Func<bool> predicate)
    {
        IsTryingToAttack = predicate;
    }

    private void Attack()
    {
        foreach(ProjectileData projectileData in weaponData.projectiles)
        {
            //TODO: weapon optimize getComponent<teamlaer>();
            if (Owner != null)
            {
                projectileData.InstantiateSelf(
                    transform.position,
                    transform.rotation,
                    World.instance.Bullets,
                    Owner.GetComponent<TeamLayer>(),
                    Owner);
            }
            else
            {
                projectileData.InstantiateSelf(
                    transform.position,
                    transform.rotation,
                    World.instance.Bullets);
            }
        }
    }

    public void SetOwner(GameObject owner)
    {
        this.Owner = owner;
    }
}
