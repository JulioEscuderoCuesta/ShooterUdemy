using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBulletsUI : MonoBehaviour
{
    
    private TextMeshProUGUI _text;

    [SerializeField]
    [Tooltip("Personaje que dispara")]
    private PlayerShooting targetShooting;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _text.text = "BULLETS: " + targetShooting.BulletsAmount;
    }
}