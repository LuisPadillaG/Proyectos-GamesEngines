using System;
using UnityEngine;

public class SistemaDialogo : MonoBehaviour
{
    [SerializeField] GameObject recuadroTexto;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        recuadroTexto.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MostrarDialogos()
    {
        recuadroTexto.SetActive(true);
    }
}
