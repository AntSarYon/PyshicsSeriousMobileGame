using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuSeleccionPersonaje : MonoBehaviour
{
    //Indice para la lista de personajes del GameManager
    private int index;

    //Referencia a la imagen para la foto del personaje en la UI
    [SerializeField] private Image uiImagen;

    //Referencia al Texto para el nombre del eprosnaje en la UI
    [SerializeField] private TextMeshProUGUI uiNombre;

    [SerializeField] private List<AudioClip> listaAudios;

    private AudioSource mAudioSource;

    //------------------------------------------------------------

    private void Awake()
    {
        //Inicializamos el Index en 0
        index = 0;
        mAudioSource = GetComponent<AudioSource>();
    }

    //------------------------------------------------------------

    void Start()
    {
        //Utilizamos el almacenamiento de PlayerPrefs para obtener el Index
        index = PlayerPrefs.GetInt("PersonajeIndex");

        //Actualizamos la pantalla -> Mostramos al 1er Personaje desde el comienzo
        CambiarPantalla();
    }

    //------------------------------------------------------------

    public void CambiarPantalla()
    {
        //Almacenamos el Index del Peronaje a trav�s de PlayerPrefs.
        PlayerPrefs.SetInt("PersonajeIndex", index);

        //Modificamos los elementos de la UI en base al Personaje correspondiente al indice
        uiImagen.sprite = GameManager.Instance.personajes[index].imagen;
        uiNombre.text = GameManager.Instance.personajes[index].nombre;
    }

    //------------------------------------------------------------

    public void SiguientePersonaje()
    {
        //Si aun quedan personajes por mostrar
        if (index < GameManager.Instance.personajes.Count - 1)
        {
            //Incrementamos el Index
            index++;
        }
        //En caso ya no queden m�s
        else
        {
            //Devolvemos el index a cero
            index = 0;
        }

        //Actualizamos la pantalla para mostrar la informacion del Index
        CambiarPantalla();
    }

    //------------------------------------------------------------

    public void AnteriorPersonaje()
    {
        //Si aun quedan personajes por mostrar
        if (index > 0)
        {
            //Redcimos el Index
            index--;
        }
        else
        {
            //Enviamos el Index al ultimo elemento
            index = GameManager.Instance.personajes.Count - 1;
        }

        //Actualizamos la pantalla para mostrar la informacion del Index
        CambiarPantalla();
    }

    //------------------------------------------------------------------------

    public void btnPlaySound()
    {
        mAudioSource.PlayOneShot(listaAudios[0], 0.55f);
    }

    //---------------------------------------------------------------------

    public void btnOtherSound()
    {
        mAudioSource.PlayOneShot(listaAudios[1], 0.55f);
    }

    //----------------------------------------------------------------------------

    public void IniciarJuego()
    {
        //Destruimos el objeto que contiene la musica de fondo
        Destroy(GameObject.Find("MenuBackgroundMusic"));

        //Mediante la instancia de SceneManager invocamos la funci�n Empezar Juego
        ScenesManager.Instance.EmpezarJuego();
    }

    public void VolverAlMenu()
    {
        ScenesManager.Instance.CargarEscena("MainMenu");
    }
}
