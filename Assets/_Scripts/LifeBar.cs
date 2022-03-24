using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LifeBar : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Vida objetivo que reflejará la barra")]
    private Life targetLife;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        _image.fillAmount = targetLife.Amount / targetLife.maximumLife; 
    }
}
