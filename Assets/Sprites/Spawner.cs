using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public void SpawnGO(GameObject go)
    {
        GameObject obj = Instantiate(
            original: go,
            position: transform.position,
            rotation: transform.rotation,
            parent: World.instance.Enemies);
    }
}
