using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private static int ExplosionAnimationDelay = 1;
    [SerializeField] [Tooltip("Cantidad de puntos que se obtienen al derrotar al enemigo")]
    private int pointsAmount = 0;

    private void Awake()
    {
        var life = GetComponent<Life>();
        life.onDeath.AddListener(DestroyEnemy);
    }

    private void Start()
    {
        EnemyManager.SharedInstance.AddEnemy(this);
    }

    private void DestroyEnemy()
    {
        Animator dieAnim = GetComponent<Animator>();
        dieAnim.SetTrigger("Play Die");
        Invoke(nameof(PlayDestruction), ExplosionAnimationDelay);
        var life = GetComponent<Life>();
        life.onDeath.RemoveListener(DestroyEnemy);
        Destroy(gameObject, 1);
        
        EnemyManager.SharedInstance.RemoveEnemy(this);
        ScoreManager.SharedInstance.Amount += pointsAmount;
    }

    private void PlayDestruction()
    {
        ParticleSystem explosion = GetComponentInChildren<ParticleSystem>();
        explosion.Play();
    }
    
}
