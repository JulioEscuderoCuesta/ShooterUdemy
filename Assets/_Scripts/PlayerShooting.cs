using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerShooting : MonoBehaviour
{

    private Animator _animator;

    [SerializeField] [Tooltip("Número de balas del jugador")]
    private int bulletsAmount;

    [SerializeField]
    [Tooltip("Arma del jugador")]
    private Weapon weapon;

    public int BulletsAmount => bulletsAmount;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.timeScale > 0)
        {
            _animator.SetBool("Shoot Bullet Bool", true);
            if (bulletsAmount <= 0 || !weapon.ShootBullet("Player Bullet", 0.25f)) return;
            bulletsAmount--;
            if (bulletsAmount < 0)
                bulletsAmount = 0;
        }
        else
        {
            _animator.SetBool("Shoot Bullet Bool", false);
        }
    }
}
