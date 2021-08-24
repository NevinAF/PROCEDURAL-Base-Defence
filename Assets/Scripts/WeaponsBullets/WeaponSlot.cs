using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TeamLayer))]
public class WeaponSlot : MonoBehaviour
{
    public Transform WeaponHolder;
    
    public bool DropWeaponOnWeaponSwitch;
    public Func<bool> IsTryingToAttack;

    public TeamLayer _team { get; private set; }
    public Weapon currentWeapon { get; private set; }

    void Start ()
    {
        _team = GetComponent<TeamLayer>();
    }

    private void OnDisable()
    {
        if (DropWeaponOnWeaponSwitch)
            DropWeapon();
        else
            StoreWeapon();
    }

    private void StoreWeapon()
    {
        if (currentWeapon != null)
        {
            //Do Something;
        }
    }

    public void SetWeapon(GameObject weapon_go, Weapon weapon_data)
    {
        DropWeapon();

        weapon_go.transform.position = WeaponHolder.position;
        weapon_go.transform.rotation = WeaponHolder.rotation;
        weapon_go.transform.parent = WeaponHolder;

        currentWeapon = weapon_data;
        currentWeapon.SetOwner(gameObject);
        currentWeapon.OverrideIsTryingToAttack(IsTryingToAttack);
        currentWeapon.enabled = true;
    }

    public void SetWeapon(GameObject weaponPrefab)
    {
        if (weaponPrefab.GetComponent<Weapon>() == null)
            throw new ArgumentException("WeaponSlot.SetWeapon() -- weaponPrefab does not have weapon component.");
        else
        {
            GameObject go = Instantiate(weaponPrefab);
            SetWeapon(go, go.GetComponent<Weapon>());
        }
    }

    public void DropWeapon()
    {
        if (currentWeapon != null)
        {
            currentWeapon.transform.parent = World.instance.transform;
            currentWeapon.enabled = false;
            currentWeapon = null;
        }
    }

    private void OnApplicationQuit()
    {
        currentWeapon = null;
    }
}
