using UnityEngine;
using UnityEngine.InputSystem;

public class JugadorVibracion : MonoBehaviour
{
    float contador;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        contador = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (contador > 0) {
            Gamepad.current.SetMotorSpeeds(1, 1);
            contador -= Time.deltaTime;

            if (contador <= 0) {
                Gamepad.current.SetMotorSpeeds(0, 0);
            }
        }

    }
    public void recibirDano()
    {
        contador = 0.1f;
    }
}
