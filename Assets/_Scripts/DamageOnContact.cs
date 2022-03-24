using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{

    [SerializeField] [Tooltip("Cuánto daño")]
    private float damage;
    private void OnTriggerEnter(Collider other)
    {
        //Destroy(gameObject) PROHIBIDO; ESTAMOS UTILIZANDO OBJECT POOLING QUE ES MEJOR
        gameObject.SetActive(false); //Desactivar la bala
       /* if(other.CompareTag("Enemy") || other.CompareTag("Player"))
            Destroy(other.gameObject); */
       Life life = other.GetComponent<Life>();
       if(life!=null) 
           life.Amount -= damage;
    }
}
