using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivarDialogo : MonoBehaviour
{
    [SerializeField] GameObject letreroHablar;
    PlayerInput playerInput;
    SistemaDialogo sistemaDialogos;
    Jugador scriptJugador;

    [SerializeField] string[] dialogos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        letreroHablar.SetActive(false);
        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        sistemaDialogos = GameObject.Find("SistemaDialogos").GetComponent<SistemaDialogo>();
        scriptJugador = GameObject.Find("SistemaDialogos").GetComponent<Jugador>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        letreroHablar.SetActive(true);
        Debug.Log("Dile q tu eres mia, mia tu sabe que ere mia, MIA : " + other.gameObject.tag);

    }
    private void OnTriggerExit(Collider other)
    {
        letreroHablar.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if (playerInput.actions["Move"].ReadValue<Vector2>().y > 0.5)
        {
            sistemaDialogos.MostrarDialogos();
            scriptJugador.PoderMoverse = false;
        }
    }
}
