using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public Transform Enemies;
    public Transform Buildings;
    public Transform Bullets;
    public Transform Players;

    public static World instance;

    private void Awake()
    {
        if (instance != null)
            Debug.Log("Overriding World static instance.");

        instance = this;
    }
}
