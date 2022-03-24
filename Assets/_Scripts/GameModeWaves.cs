using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameModeWaves : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Vida del jugador")]
    private Life playerLife;
    
    [SerializeField]
    [Tooltip("Vida de la base")]
    private Life baseLife;

    private void Start()
    {
        playerLife.onDeath.AddListener(CheckLoseCondition);
        baseLife.onDeath.AddListener(CheckLoseCondition);
        
        EnemyManager.SharedInstance.onEnemyChanged.AddListener(CheckWinCondition);
        WaveManager.SharedInstance.onWaveChanged.AddListener(CheckWinCondition);
        
    }

   private void CheckLoseCondition()
   {
       Debug.Log("dentro checklosecondition");
       RegisterScore();
       RegisterTime();
       SceneManager.LoadScene("Lose Scene", LoadSceneMode.Single);
   }
   private void CheckWinCondition()
   {
       Debug.Log("dentro checkwincondition");
       Debug.Log("enemigos" + EnemyManager.SharedInstance.EnemiesCount);
       if (EnemyManager.SharedInstance.EnemiesCount <= 0 &&
           WaveManager.SharedInstance.WavesCount <= 0)
       {
           RegisterScore();
           RegisterTime();
           SceneManager.LoadScene("Win Scene", LoadSceneMode.Single);
       }  
   }

   private void RegisterScore()
   {
       var actualScore = ScoreManager.SharedInstance.Amount;
       PlayerPrefs.SetInt("Last Score", actualScore);

       var highestScore = PlayerPrefs.GetInt("Highest Score", 0);
       if(actualScore > highestScore)
           PlayerPrefs.SetInt("Highest Score", actualScore);
   }
   
   private void RegisterTime()
   {
       var actualTime = Time.time;
       PlayerPrefs.SetFloat("Last Time", actualTime);

       var lowestTime = PlayerPrefs.GetFloat("Lowest Score", 999999999.0f);
       if(actualTime < lowestTime)
           PlayerPrefs.SetFloat("Lowest Time", actualTime);
   }
}
