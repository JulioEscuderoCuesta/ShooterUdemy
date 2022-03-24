using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public class WaveManager : MonoBehaviour
{
    public static WaveManager SharedInstance;
    [SerializeField] [Tooltip("Manager de las oleadas de enemigos")]
    private List<WaveSpawner> waves;
    public UnityEvent onWaveChanged;
    private int maxWaves;
    public int WavesCount => waves.Count;

    public int MaxWaves => maxWaves;

    private void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
            waves = new List<WaveSpawner>();
        }
        else
        {
            Destroy(this);
            Debug.Log("Dentro destroy WaveManagement");
        }
    }

    public void AddWave(WaveSpawner wave)
    {
        maxWaves++;
        waves.Add(wave);
        onWaveChanged.Invoke();
    }

    public void RemoveWave(WaveSpawner wave)
    {
        waves.Remove(wave);
        onWaveChanged.Invoke();
    }
}
