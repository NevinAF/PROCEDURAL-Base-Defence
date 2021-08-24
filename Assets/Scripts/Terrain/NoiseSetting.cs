using System;
using UnityEngine;

[CreateAssetMenu()]
public class NoiseSetting : ScriptableObject
{
    public int seed;
    public int octaves;
    public Vector2 offset;
    public float persistance;
    public float scale;
    public float lacunarity;
}