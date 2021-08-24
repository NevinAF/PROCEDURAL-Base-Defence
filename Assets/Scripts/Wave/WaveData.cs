using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class WaveData : ScriptableObject
{
    [Tooltip("Anything less than 0 will wait for all enemies to die")]
    public float SpawnWaveAtTime = 10;
    public SpawnData[] spawnDatas;
    
    [System.Serializable]
    public class SpawnData
    {
        public float SpawnAtTime = 0;
        public int SpawnNumber = 0;
        public int Amount = 1;
        public float TimeBetween = 0;

        public GameObject SpawnPrefab;
    }
}