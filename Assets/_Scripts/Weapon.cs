using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float _lastShootTime;
    
    [SerializeField]
    [Tooltip("Sistema de particulas de disparo")]
    private ParticleSystem fireEffect;

    [SerializeField] 
    [Tooltip("Sonido de disparo")]
    private AudioSource shootSound;
    
    [SerializeField]
    [Tooltip("Punto desde el que dispara el enemigo")]
    private GameObject shootingPoint;
    
    [SerializeField]
    [Tooltip("Ratio de disparo del enemigo")]
    private float shootRate;

    private string _layerName;
    
    public bool ShootBullet(string layerName, float delay)
    {
        if (!(Time.timeScale > 0)) return false;
        var timeSinceLastShoot = Time.time - _lastShootTime;
        if (timeSinceLastShoot < shootRate)
        {
            return false;
        }
        
        _lastShootTime = Time.time;
        _layerName = layerName;
        Invoke(nameof(FireBullet), delay);
        return true;

    }

    private void FireBullet()
    {
        var bullet = ObjectPool.SharedInstance.getFirstPooledObject();
        bullet.layer = LayerMask.NameToLayer(_layerName);
        bullet.transform.position = shootingPoint.transform.position;
        bullet.transform.rotation = shootingPoint.transform.rotation;
        bullet.SetActive(true);
        if(fireEffect != null)
            fireEffect.Play();
        if (shootSound != null)
            Instantiate(shootSound, transform.position, transform.rotation).GetComponent<AudioSource>().Play();
    }
}
