using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public FloatAttribute maxHealth;
    public FloatAttribute HealthPercent;
    public float HP { get; private set; }
    public bool bulletsHurt;
    private UnityEvent DeathListeners;

    // Use this for initialization
    void Awake ()
    {
        SetHealth();
        DeathListeners = new UnityEvent();
	}

    private void Start()
    {
        maxHealth.Start();
        HealthPercent.Start();
    }

    public void SetHealth(float amount)
    {
        HP = amount;
        HealthPercent.SetValue(HP / maxHealth);

        if (amount <= 0)
            DeathListeners.Invoke();
    }

    private void Die()
    {
    }

    public void Hit(ProjectileData data)
    {
        DecrementHealth(data.damage);
    }

    public void IncrementHealth(float amount = 1)
    {
        SetHealth(HP + amount);
    }

    public void DecrementHealth(float amount = 1)
    {
        SetHealth(HP - amount);
    }

    public void SetHealth()
    {
        SetHealth(maxHealth);
    }

    public void AddDeathListener(UnityAction action)
    {
        DeathListeners.AddListener(action);
    }

    public void RemoveDeathListener(UnityAction action)
    {
        DeathListeners.RemoveListener(action);
    }
}
