using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    [SerializeField] [Tooltip("Prefab de enemigo a generar")]
    private GameObject prefab;

    [SerializeField] [Tooltip("Tiempo en el que se inicia y finaliza la oleada")]
    private float startTime, endTime;

    [SerializeField] [Tooltip("Tiempo entre la generación de enemigos")]
    private float spawnRate;
    
    // Start is called before the first frame update
    void Start()
    {
        WaveManager.SharedInstance.AddWave(this);
        InvokeRepeating(nameof(SpawnEnemy), startTime, spawnRate);
        Invoke(nameof(EndWave), endTime);
    }
 
    private void SpawnEnemy()
    {
        /*Quaternion q = Quaternion.Euler(0, transform.rotation.eulerAngles.y + Random.Range(-45.0f, 46.0f), 0);*/
        Instantiate(prefab, transform.position, transform.rotation);
    }

    private void EndWave()
    {
        WaveManager.SharedInstance.RemoveWave(this);
        CancelInvoke();
    }
    
}
