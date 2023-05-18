using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExitDoorPlantilla2D : MonoBehaviour
{
    // Flags de estado

    private bool jugadorCerca;

    private void Awake()
    {
        jugadorCerca = false;
    }

    private void Update()
    {
        // Si el jugador esta cerca y oprimimos espacio
        if(jugadorCerca && Input.GetKeyDown(KeyCode.Space))
        {
            GameObject.Find("ScenesManager").GetComponent<ScenesManager>().CargarEscena("EM_Principal");
        }
    }

    private void OnCollisionEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Activamos el Flag y mostramos el icono de dialogo
            jugadorCerca = true;          
        }

    }
}
