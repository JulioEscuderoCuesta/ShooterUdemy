using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WavesUI : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        WaveManager.SharedInstance.onWaveChanged.AddListener(RefreshText);
    }

    private void RefreshText()
    {
        _text.text = "WAVES: " + (WaveManager.SharedInstance.MaxWaves - WaveManager.SharedInstance.WavesCount 
                                  + "/" + WaveManager.SharedInstance.MaxWaves);
    }
}
