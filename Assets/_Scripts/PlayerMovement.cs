using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))] //Se requiere que el objeto que tenga este script tenga un RigidBody
public class PlayerMovement : MonoBehaviour
{   
    //F = m*a
    [SerializeField]
    [Range(0 , 10000)]
    [Tooltip("Fuerza de movimiento del personaje en N/s")]
    private float speed;

    [SerializeField]
    [Range(0, 1000)]
    [Tooltip("Fuerza de rotación del personaje en N/s")]
    private float rotationSpeed;
    
    private Rigidbody _rb;

    private Animator _animator;

    void Start()
    {
        //Quitar el cursor de la pantalla y bloquearlo para que no se mueva.
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        //S = V * T
        float space = speed * Time.deltaTime; //Tiempo que ha pasado desde el último frame
        float horizontal = Input.GetAxis("Horizontal"); //-1 a 1
        float vertical = Input.GetAxis("Vertical");     //-1 a 1

        //Coger la dirección que se introduce
        Vector3 dir = new Vector3(horizontal, 0, vertical);
        //transform.Translate(dir.normalized * space);  //Bien si no hay físicas
        //FUERZA DE TRANSLACIÓN
        _rb.AddRelativeForce(dir.normalized * space);

        float angle = rotationSpeed * Time.deltaTime;
        float horizontalCameraMovement = Input.GetAxis("Horizontal Camera Movement");
        //transform.Rotate(0, horizontalCameraMovement * angle, 0); //Bien si no hay físicas
        //FUERZA DE ROTACIÓN
        _rb.AddRelativeTorque(0, horizontalCameraMovement * angle, 0);
        _animator.SetFloat("Velocity", _rb.velocity.magnitude);
        _animator.SetFloat("Move X", horizontal);
        _animator.SetFloat("Move Y", vertical);

        /*
        TODO: ajustar animaciones para caminar y correr
        if (Input.GetKey(KeyCode.LeftShift)) 
            _animator.SetFloat("Velocity", _rb.velocity.magnitude);
        else
        {
            if(Mathf.Abs(horizontal) < 0.01 && Mathf.Abs(vertical) < 0.01)
                _animator.SetFloat("Speed", 0);
            else
                _animator.SetFloat("Speed", 1f);
        }*/
        
        
        
        
        
        

        /*if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        { 
            transform.Translate(0,0,space);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        { 
            transform.Translate(0,0,-space);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        { 
            transform.Translate(-space,0,0);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        { 
            transform.Translate(space,0,0);
        }*/
    }
}
