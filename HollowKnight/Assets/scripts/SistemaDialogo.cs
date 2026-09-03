using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SistemaDialogo : MonoBehaviour
{
    [SerializeField] GameObject recuadroTexto;
    [SerializeField] TMP_Text texto_dialogo;
    string texto;
    int EstadoDialogo;
    int i;
    float contador;
    [SerializeField] GameObject botonSiguiente;
    ActivarDialogo sistemaDialogos;
    PlayerInput playerInput;
    float tiempoLetra;
    [SerializeField] TMP_Text nombre_persona;
    ActivarDialogo _activarDialogo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        recuadroTexto.SetActive(false);
        EstadoDialogo = 0;
        texto = "";
        i = 0;
        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (EstadoDialogo)
        {
            case 0:
                break;
            case 1:
                contador += Time.deltaTime;
                if(contador > 0.5f)
                {
                    contador = 0;
                    if (i < (texto.Length - 1))
                    {
                        texto_dialogo.text = texto[i].ToString();
                        i++;
                    }
                    else
                    {
                        botonSiguiente.SetActive(true);
                        EstadoDialogo = 2;
                        i = 0;
                    }
                }

                break;
            case 2: //Esperar input jugador. 
                if (playerInput.actions["Jump"].WasPressedThisFrame())
                {
                    EstadoDialogo = 1;
                    texto_dialogo.text = "";
                    botonSiguiente.SetActive(false);
                    i = 0;
                    
                }
                break;

        }
    }
    public void MostrarDialogos(string dialogo, string nombre, ActivarDialogo activarDialogo)
    {
        nombre_persona.text = nombre;
        recuadroTexto.SetActive(true);
        texto = dialogo;
        texto_dialogo.text = ""; 
        EstadoDialogo = 1;
        _activarDialogo = activarDialogo;
    }
}
