using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ActivarDialogo : MonoBehaviour
{

    [SerializeField] GameObject LetreroHablar;
    PlayerInput playerInput;
    Jugador scriptJugador;
    SistemaDialogo sistemaDialogos;
    [SerializeField] string[] Dialogos;
    public int dialogoActual;
    public bool leyendoDialogo;
    [SerializeField] string Nombre;
    public UnityEvent onDialogoTerminado;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scriptJugador = GameObject.FindWithTag("Player").GetComponent<Jugador>();
        LetreroHablar.SetActive(false);
        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        sistemaDialogos = GameObject.Find("SistemaDialogos").GetComponent<SistemaDialogo>();
        dialogoActual = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("te toqué te toca");
        LetreroHablar.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("pq te tatuatis");
        LetreroHablar.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if (!leyendoDialogo)
        {
            if (playerInput.actions["Move"].ReadValue<Vector2>().y > 0.5f)
            {
                sistemaDialogos.MostrarDialogos(Dialogos[dialogoActual], Nombre, this);
                scriptJugador.PoderMoverse = false;
                Debug.Log("sPEAKING");
                leyendoDialogo = true;
            }
        }
    }
    public void TerminoDialogoActual()
    {
        Debug.Log("Termine");
        if (dialogoActual < Dialogos.Length - 1)
        {
            dialogoActual++;
            sistemaDialogos.MostrarDialogos(Dialogos[dialogoActual], Nombre, this);
        }
        else
        {
            Debug.Log("Aquitermino todo ");
            leyendoDialogo = false;
            LetreroHablar.SetActive(false);
            scriptJugador.PoderMoverse=true;
            onDialogoTerminado.Invoke();
        }
        
    }
}