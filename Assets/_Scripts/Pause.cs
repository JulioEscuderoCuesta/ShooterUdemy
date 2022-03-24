using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private const float TransitionPeriodMusic = 0.05f;
    [SerializeField]
    [Tooltip("Menú de pausa")]
    private GameObject pauseMenu;

    [SerializeField] [Tooltip("")] 
    private AudioMixerSnapshot pauseSnp, gameSnp;

    public Button exitButton;

    private void Awake()
    {
        pauseMenu.SetActive(false);
        exitButton.onClick.AddListener(ExitGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        //Transición o fundido entre un sonido y otro.
        pauseSnp.TransitionTo(TransitionPeriodMusic);
    }

    private void ResumeGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        gameSnp.TransitionTo(TransitionPeriodMusic);
    }

    private void ExitGame()
    {
        print("SALIR DEL JUEGO");
    }
    
    
}
