using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDoorSCM_Principal : MonoBehaviour
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
            GameObject.Find("ScenesManager").GetComponent<ScenesManager>().SolicitarCambioDeEscena("SCM_Principal");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Estoy chocando con la puerta");
            //Activamos el Flag y mostramos el icono de dialogo
            jugadorCerca = true;
        }
    }
}
