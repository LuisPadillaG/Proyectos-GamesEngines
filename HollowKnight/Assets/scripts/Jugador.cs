using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jugador : MonoBehaviour
{
    CharacterController characterController;
    PlayerInput playerInput;
    Vector3 velocidad;
    Animator animator;
    Vector3 rotacion;
    int salto_restante;
    float is_grounded;
    RaycastHit hit;
    // Estados
    float contador_dash, cooldownDash;
    float contadorInvisible;
    //Ataque
    float contadorAtaque;
    [SerializeField] public GameObject prefabHitBoxAtaque;
    [SerializeField] Transform lugarCreacionHitBoxAtaque;
    [SerializeField] JugadorDestello jugadorDestello;
    int VIDA;
    float tiempoInvulnerable;
    float valorMaximoJugadorDestello;
    float danoMovimientoX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        is_grounded = 0;
        salto_restante = 2;
        characterController = this.GetComponent<CharacterController>();
        playerInput = this.GetComponent<PlayerInput>();
        velocidad = Vector3.zero;
        rotacion = Vector3.zero;
        animator = this.GetComponentInChildren<Animator>();
        // Application.targetFrameRate = 30;
        //Estados
        contador_dash = 0;
        cooldownDash = 0;
        contadorAtaque = 0;
        VIDA = 5;
        contadorInvisible = 0;
        tiempoInvulnerable = 0;
        valorMaximoJugadorDestello = 1.5f;
        danoMovimientoX = 0;
    }

    // Update is called once per frame
    void Update()
    {
        contadorInvisible -= Time.deltaTime; 

        if (contadorInvisible > 0)
        { 
            float intervalo = 0.1f; 
            int estado = Mathf.FloorToInt(contadorInvisible / intervalo);

            if (estado % 2 == 0)
            {
                jugadorDestello.SemiDestello();
            }
            else
            {
                jugadorDestello.NuloDestello();
            }
        }
        else
        {
            jugadorDestello.NuloDestello();
        }



        velocidad.y -= 130 * Time.deltaTime; 
        contador_dash -= Time.deltaTime;
        cooldownDash -= Time.deltaTime;
        if (danoMovimientoX != 0) {
            velocidad.x = playerInput.actions["Move"].ReadValue<Vector2>().x * 10;
        }
        is_grounded -= Time.deltaTime;
        if (characterController.isGrounded)
        {
            is_grounded = 0.2f;
        }
        if (is_grounded > 0)
        {
            salto_restante = 1;
            if (velocidad.x > 0)
            {
                rotacion.y = 0;
            }
            if (velocidad.x < 0)
            {
                rotacion.y = 180;
            }
            if (velocidad.x == 0)
            {
                if (contadorAtaque <= 0) { animator.Play("Jugador_Idle"); }
            }
            else
            {
                if (contador_dash <= 0 && contadorAtaque <= 0)
                {
                    animator.Play("Jugador_Caminando");
                }
            }
            velocidad.y = -1;

            if (playerInput.actions["Jump"].WasPressedThisFrame())
            {
                Debug.Log("Saltaaaaa");
                velocidad.y = 30;
                is_grounded = 0;
                animator.Play("Jugador_Saltar");
            }
        }
        else
        {
            if (playerInput.actions["Jump"].IsPressed())
            {
                velocidad.y += 40 * Time.deltaTime;
            }
            if (contador_dash <= 0 && contadorAtaque <= 0)
            {
                if (velocidad.y < -20)
                {
                    animator.Play("Jugador_EmpezarCaida");
                }
                else if (velocidad.y < 0)
                {
                    animator.Play("JugadorCaida");
                }
            }

            if (playerInput.actions["Jump"].WasPressedThisFrame() && salto_restante > 0)
            {
                velocidad.y = 30;
                salto_restante--;
                animator.Play("doble_salto");
            } 
        }
        if (contador_dash <= 0)
        {
            velocidad.x = playerInput.actions["Move"].ReadValue<Vector2>().x * 4;
            //animator.Play("doble_salto");
        }
        if (playerInput.actions["Sprint"].WasPressedThisFrame())
        {
            
            Debug.Log("Dash y su disfrash");
            contador_dash = 0.3f;
        }
        
        if(contador_dash > 0)
        { 
            if (cooldownDash <= 0)
            {
                if (rotacion.y == 0)
                {
                    velocidad.x = 20;

                }
                else
                {
                    velocidad.x = -20;
                }
                //cooldownDash = 0.5f;
                velocidad.y = 0;
                if (velocidad.x != 0)
                {
                    animator.Play("Jugador_dash");
                }

            }
            //transform.position = new Vector3(transform.position.x, alturaActual,transform.position.z);
            //velocidad.y = 0;
        }
        if(danoMovimientoX > 0)
        {
            Debug.Log(danoMovimientoX);
            danoMovimientoX -= Time.deltaTime * 10;
            if(danoMovimientoX < 0)
            {
                danoMovimientoX = 0;
            } 
        }
        if (danoMovimientoX < 0)
        {
            Debug.Log(danoMovimientoX);
            danoMovimientoX += Time.deltaTime * 10;
            if (danoMovimientoX > 0)
            {
                danoMovimientoX = 0;
            }
        }
        characterController.Move(velocidad * Time.deltaTime);
        this.transform.rotation = Quaternion.Euler(rotacion);
        // ================= ataque
        if (playerInput.actions["Attack"].WasPressedThisFrame())
        {
            if (contadorAtaque <= 0) {
                animator.Play("Jugador_ataque");
                contadorAtaque = 0.2f;
                Instantiate(prefabHitBoxAtaque, lugarCreacionHitBoxAtaque.position, Quaternion.identity);
            }
            
        }
        contadorAtaque -= Time.deltaTime;
        Debug.DrawRay(this.transform.position + Vector3.up, Vector3.up *1.6f, Color.blue);

        if(Physics.Raycast(this.transform.position + new Vector3(0, 0, 0.5f), Vector3.up * 0.2f, out hit, 1.5f))
        {
            Debug.Log(hit.collider.gameObject.name); //rayo, colision, el objeto, el nombre
            //Debug.Log("Le di a algo");
            velocidad.y -= 1;
        }
        else
        {
            //Debug.Log("No le di no le diiiiiiiiiiiii");
        }
        //Debug.Log(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Dano" && contadorInvisible <= 0)
        {
            contadorInvisible = jugadorDestello.ActivarDestello();
            valorMaximoJugadorDestello = contadorInvisible;
            VIDA--;
            if (other.gameObject.transform.position.x > this.transform.position.x) {
                danoMovimientoX = -5;
            }
            else
            {
                danoMovimientoX = 5;
            }
            velocidad.y = 30;
            velocidad.x = danoMovimientoX;
            //characterController.Move(Vector3.up * 0.5f);
            if (VIDA <= 0) { Destroy(this.gameObject); }
        }
    }
}