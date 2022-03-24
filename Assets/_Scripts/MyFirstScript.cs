using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MyFirstScript : MonoBehaviour
{

    [SerializeField]
    [Range(0,10)]
    [Tooltip("Dinero del jugador")]
    private float moneyIHaveInThePocket;

    private String playerName = "Antonio Banderas";
    
    public float MoneyIHaveInThePocket
    {
        get => moneyIHaveInThePocket;
        set
        {
            if (value < 0)
                moneyIHaveInThePocket = 0;
            else
                moneyIHaveInThePocket = value;
        }
        
    }
    private void Awake()
    {
        Debug.Log("Estoy en el awake");
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Estoy en el start");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.LogFormat("Dinero de {1}: {0}", moneyIHaveInThePocket, playerName);
    }

    private void OnDestroy()
    {
        Debug.Log("Estoy en OnDestroy");
    }
}
