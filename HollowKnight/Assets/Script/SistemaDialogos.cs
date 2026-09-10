using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class SistemaDialogos : MonoBehaviour
{
    [SerializeField] GameObject recuadroTexto;
    [SerializeField] TMP_Text textoDialogo;



    int EstadoDialogo;
    int i;
    string texto;
    float contador;
    AudioSource audioSource;
    float tiempoLetra = 0.05f;

    [SerializeField] GameObject botonSiguiente;
    PlayerInput playerInput;
    [SerializeField] TMP_Text nombrePersona;
    ActivarDialogo _activadorDialogo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        recuadroTexto.SetActive(false);
        EstadoDialogo = 0;
        texto = "";
        i = 0;
        contador = 0;
        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        _activadorDialogo = null;
    }

    // Update is called once per frame
    void Update()
    {
        switch (EstadoDialogo)
        {
            case 0:

                break;
            case 1: //Escribir dialogo
                contador += Time.deltaTime;
                if (contador > tiempoLetra)
                {
                    if (i < texto.Length)
                    {
                        textoDialogo.text += texto[i].ToString();
                        i++;
                    }
                    else
                    {
                        botonSiguiente.SetActive(true);
                        /*if (playerInput.actions["Jump"].WasPressedThisFrame())
                        {
                            EstadoDialogo = 0;
                            textoDialogo.text = "";
                        }*/
                        EstadoDialogo = 2;
                    }
                }
                if(playerInput.actions["Jump"].WasPressedThisFrame())
                {
                    textoDialogo.text= texto;
                    botonSiguiente.SetActive(true);
                    EstadoDialogo = 2;
                    i = 0;
                }
                break;
            case 2 :
                if (playerInput.actions["Jump"].WasPressedThisFrame())
                {
                    EstadoDialogo = 0;
                    textoDialogo.text = "";
                    botonSiguiente.SetActive(false);
                    recuadroTexto.SetActive(false);
                    _activadorDialogo.TerminoDialogoActual();
                    i = 0;
                }
                break;
        }
    }

    public void MostrarDialogos(string dialogo, string nombre, ActivarDialogo activarDialogo, AudioClip audio)
    {
        recuadroTexto.SetActive(true);
        if (EstadoDialogo == 0)
        {
            nombrePersona.text = nombre;
            
            texto = dialogo;
            textoDialogo.text = " ";
            EstadoDialogo = 1;
            _activadorDialogo = activarDialogo;

            audioSource.clip = audio;
        }
    }
}
