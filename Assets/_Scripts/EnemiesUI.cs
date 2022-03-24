using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class EnemiesUI : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        EnemyManager.SharedInstance.onEnemyChanged.AddListener(RefreshText);
    }
    
    

    private void RefreshText()
    {
        _text.text = "REMAINING ENEMIES: " + EnemyManager.SharedInstance.EnemiesCount;
    }
}
