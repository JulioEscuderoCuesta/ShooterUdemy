using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private Text actualScore, actualTime, bestScore, bestTime;

    [SerializeField]
    private bool playerHasWon;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (!playerHasWon) return;
        actualScore.text = "Actual Score: " + PlayerPrefs.GetInt("Last Score");
        actualTime.text = "Actual Time: " + PlayerPrefs.GetFloat("Last Time");
        bestScore.text = "Best Score: " + PlayerPrefs.GetInt("Highest Score");
        bestTime.text = "Best Time: " + PlayerPrefs.GetFloat("Lowest Score");

    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene("Level 1");
    }
}
