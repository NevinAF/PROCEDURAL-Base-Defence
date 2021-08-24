using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHandeler : MonoBehaviour
{
    public Spawner[] spawners;
    public WaveData[] waves;
    public bool RepeatOnceDone = false;

    private int waveNumber;
    private float timer;
    private Queue<WaveData> waveQueue;

    private float nextWaveTime;

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        waveQueue = Functions.MakeQueue(waves);
        if (waveQueue.Count == 0)
            this.enabled = false;

        nextWaveTime = waveQueue.Peek().SpawnWaveAtTime;
    }

    public bool NextWaveReady()
    {
        if(nextWaveTime < 0)
        {
            return (World.instance.Enemies.childCount < 1);
        }

        return (timer >= nextWaveTime);
    }

    // Update is called once per frame
    void Update ()
    {
        while (NextWaveReady())
        {
            StartCoroutine(StartWave(waveQueue.Dequeue()));
            if (waveQueue.Count == 0)
            {
                if (RepeatOnceDone)
                    Setup();
                else
                    this.enabled = false;
                break;
            }
            timer = 0;
            nextWaveTime = waveQueue.Peek().SpawnWaveAtTime;
        }
    }

    private IEnumerator StartWave(WaveData wave)
    {
        Queue<WaveData.SpawnData> dataQueue = Functions.MakeQueue(wave.spawnDatas);
        float lastSpawnTime = 0;
        while(dataQueue.Count > 0)
        {
            WaveData.SpawnData data = dataQueue.Peek();
            if (data.SpawnAtTime <= lastSpawnTime)
            {
                StartCoroutine(Spawn(dataQueue.Dequeue()));
            } else
            {
                yield return new WaitForSeconds(data.SpawnAtTime - lastSpawnTime);
                lastSpawnTime = data.SpawnAtTime;
            }
        }
    }

    private IEnumerator Spawn(WaveData.SpawnData data)
    {
        int amount = data.Amount;
        while (amount > 0)
        {
            spawners[data.SpawnNumber].SpawnGO(data.SpawnPrefab);
            amount--;
            yield return new WaitForSeconds(data.TimeBetween);
        }
    }

}
