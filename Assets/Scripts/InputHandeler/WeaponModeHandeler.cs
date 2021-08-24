using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  This class is meant to world with a weapon slot on a player. This
 *  controls where weapons go when swapped. Mode also alows switching
 *  between what the user wants to do (input modes)
 */
public class WeaponModeHandeler : ModeHandeler
{
    public GameObject weaponPrefab;
    public WeaponSlot _weaponSlot { get; private set; }

    public override InputMode Mode
    {
        get
        {
            return InputMode.Weapon;
        }
    }

    private void Start()
    {
        _weaponSlot = _player._weaponSlot;
        _weaponSlot.IsTryingToAttack = () => Input.GetMouseButton(0);
        if (weaponPrefab != null)
            CreateWeapon();

    }

    private void OnEnable()
    {
        if (_weaponSlot != null)
        {
            _weaponSlot.enabled = true;
            CreateWeapon();
        }
    }

    private void OnDisable()
    {
        if(_weaponSlot != null)
            _weaponSlot.enabled = false;
    }

    private void OnDestroy()
    {
        
    }

    private void CreateWeapon()
    {
        _weaponSlot.SetWeapon(weaponPrefab);
    }
}
