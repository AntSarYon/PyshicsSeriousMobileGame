using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineFreeLook;

public class PysichsMaster : MonoBehaviour
{
    //Distancia del rayo
    [SerializeField] float rangoDeteccion = 25f;

    //Referencia al Script de Orbita
    private OrbitaController mOrbita;
    private TouchDeteccion mTouchDeteccion;

    //Referencia al Objeto del medio de la escena
    [SerializeField] private GameObject centro;
    [SerializeField] private GameObject centroRelativo;

    //Fuerza para GOLPE
    [SerializeField] private float fuerzaGolpe;
    
    //Data del objeto impactado por el Rayo
    private RaycastHit hitPointer;

    //------------------------------------------------------------
    private void Awake()
    {
        //Obtenemos referencia a componentes
        mOrbita = GetComponent<OrbitaController>();
        mTouchDeteccion = GetComponent<TouchDeteccion>();
    }

    //------------------------------------------------------------
    
    public void Empujar()
    {
        //Compruebo si hay un RigidBody asignado
        if (mTouchDeteccion.RigidBodySeleccionado != null)
        {
            //Disparo un rayo para obtener un punto de contacto con el objeto en la escena.
            if (Physics.Raycast(transform.position, transform.forward, out hitPointer, rangoDeteccion))
            {
                //Aplicamos la fuerza a parir del punto de contacto
                mTouchDeteccion.RigidBodySeleccionado.AddForceAtPosition(
                    (transform.forward * fuerzaGolpe),
                    hitPointer.point, 
                    ForceMode.Impulse
                    );
            }
            
        }
    }

    //--------------------------------------------------------------
    public void ModificarGravedad(int direccion)
    {
        switch (direccion)
        {
            //Caso 0 (Hacia abajo)
            case 0:
                Physics.gravity = new Vector3(0, -9.81f, 0);
                break;
            //Caso 1 (Hacia arriba)
            case 1:
                Physics.gravity = new Vector3(0, 9.81f, 0);
                break;
            //Caso 2 (Hacia la Izquierda)
            case 2:
                Physics.gravity = new Vector3(-9.81f, 0, 0);
                break;
            //Caso 3 (Hacia la Derecha)
            case 3:
                Physics.gravity = new Vector3(9.81f, 0, 0);
                break;
            //Caso 4 (Hacia Atras)
            case 4:
                Physics.gravity = new Vector3(0, 0, -9.81f);
                break;
            //Caso 5 (Hacia Adelante)
            case 5:
                Physics.gravity = new Vector3(0, 0, 9.81f);
                break;
        }
    }

    //--------------------------------------------------------------
    public void DesacoplarseDeObjeto()
    {
        //Actualizamos la posicion del Centro Relativo en base al ultimo objeto seleccionado
        centroRelativo.transform.position = mOrbita.ObjetoSeguido.transform.position;

        //Cambiamos a Null la referencia de Objeto y RB seguidos
        mOrbita.ObjetoSeguido = null;
        mTouchDeteccion.RigidBodySeleccionado = null;

        //Activamos la orbita desde la posicion donde soltamos el objeto 
        mOrbita.ObjetoSeguido = centroRelativo.transform;
    }

    public void RegresarAlMedio()
    {
        //Cambiamos a Null la referencia de Objeto y RB seguidos
        mOrbita.ObjetoSeguido = null;
        mTouchDeteccion.RigidBodySeleccionado = null;

        //Lo asignamos como nuevo punto de orbita
        mOrbita.ObjetoSeguido = centro.transform;
    }

    //--------------------------------------------------------------

    private void OnDrawGizmos()
    {
        //Pintaremos el rayo de color azul
        Gizmos.color = Color.blue;

        //Dibujamos el rayo hacia el frente de la camar
        Gizmos.DrawRay(transform.position, transform.forward * rangoDeteccion);
    }
}
