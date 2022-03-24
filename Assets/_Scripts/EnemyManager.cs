using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager SharedInstance;

    [SerializeField] [Tooltip("Lista de enemigos")]
    private List<Enemy> enemies;

    public UnityEvent onEnemyChanged;

    //public List<Enemy> Enemies => enemies;
    
    public int EnemiesCount => enemies.Count;
    
    private void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
            enemies = new List<Enemy>();
        }
        else
        {
            Destroy(this);
        }
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
        onEnemyChanged.Invoke();
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        onEnemyChanged.Invoke();
    }
}
