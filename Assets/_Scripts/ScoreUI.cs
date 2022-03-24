using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI _text;
    
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _text.text = "SCORE: " + ScoreManager.SharedInstance.Amount;
    }
}
