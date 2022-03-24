using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Sight : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Distancia de visión")]
    private float distance;
    [SerializeField] [Tooltip("Ángulo de visión")]
    private float angle;
    [SerializeField] [Tooltip("Qué tipos de objetos quiero que este script detecte")]
    private LayerMask targetLayers;
    [SerializeField] [Tooltip("// Qué objetos se considerarán obstáculos.")]
    private LayerMask obstacleLayers;

    public Collider detectedTarget;

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, distance, targetLayers);
        //Limpiar lo del frame anterior
        detectedTarget = null;
        foreach (Collider collider in colliders)
        {
            Vector3 directionToCollider = Vector3.Normalize(collider.bounds.center - transform.position);
            
            //cos(angle) = u.v / ||u|| . ||v||
            float angleToCollider = Vector3.Angle(transform.forward, directionToCollider);
            
            if(angleToCollider < angle) {
                //Objeto dentro del ángulo de visión
                //Con RaycastHit se devolverá el objeto con el que ha colisionado la visión.
                if (!Physics.Linecast(transform.position, collider.bounds.center, out RaycastHit hit, obstacleLayers))
                {   //No hay objetos entre medio de la visión
                    Debug.DrawLine(transform.position, collider.bounds.center, Color.green);
                    //Guardar la referencia del objetivo detectado. Cuando se encuentra el primer objeto, break.
                    detectedTarget = collider;
                    break;
                }
                else //Hay objeto colisionado
                {
                    //Pintar desde el enemigo hasta el punto de colisión
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                }
            }
            
            //Con producto vectorial
            //Vector3.Dot()
        }
    }

    private void OnDrawGizmosSelected()
    {
        //Visión esfera
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);

        Gizmos.color = Color.magenta;
        //Visión de cono
        //Al ser transform.forward un vector de distancia 1, el restulado lo multiplico por disntance para pintar la línea.
        //Para saber los dos lados que forman el cono, roto la visión forward el ángulo correspondiente hacia
        //derecha e izquierda. Al no ser GameObject, necesito Quaternion para rotar.
        Vector3 rightDir = Quaternion.Euler(0, angle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, rightDir * distance);
        Vector3 leftDir = Quaternion.Euler(0, -angle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, leftDir* distance);
    }
}
