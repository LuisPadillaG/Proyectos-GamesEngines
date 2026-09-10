using Unity.VisualScripting;
using UnityEngine;

public class Camara : MonoBehaviour
{
    Transform jugador;
    [SerializeField] Transform limitesPantalla;


    Vector3 LimiteIzq;
    Vector3 LimiteDerecho;
    Vector3 LimiteInf;
    Vector3 LimiteSup;
    Vector3 pos;
    Transform camara;

    float jugadorRotacionY;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        LimiteIzq = limitesPantalla.GetChild(0).position;
        LimiteDerecho = limitesPantalla.GetChild(1).position;
        LimiteInf = limitesPantalla.GetChild(2).position;
        LimiteSup = limitesPantalla.GetChild(3).position;
        camara = Camera.main.transform;
        pos = jugador.position;
        this.transform.position = jugador.position;
        pos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        jugadorRotacionY = jugador.rotation.eulerAngles.y;
        //pos = jugador.position;
        pos = Vector3.MoveTowards(pos, jugador.position, 4 * Time.deltaTime);
        /// Liminte distancia jugador
        /// 
        float distanciaJugador = Vector3.Distance(jugador.position, this.transform.position);
        //Debug.Log(distanciaJugador);
        if(distanciaJugador > 2)
        {
            pos = this.transform.position;
            float angulo = Herramientas.ObtenerAngulo2D(this.transform.position, jugador.transform.position);
            pos.x = jugador.transform.position.x - Mathf.Cos(angulo * Mathf.Deg2Rad) * 2;
            pos.y = jugador.transform.position.y - Mathf.Sin(angulo * Mathf.Deg2Rad) * 2;
            Debug.Log(pos);
            Debug.Log(angulo);
        }
        if (jugadorRotacionY > 90) {
            camara.localPosition = new Vector3(-1,0,-10);
            //escribe bien esto y ya.
            //camara.localPosition = Vector3.MoveTowards(camara.localPosition, jugador.position, 4 * Time.deltaTime);
        }
        else
        {
            camara.localPosition = new Vector3(1, 0, -10);
        }

        // Limite escenario
        if (pos.x < LimiteIzq.x)
        {
            pos.x = LimiteIzq.x;
        }
        if (pos.x > LimiteDerecho.x)
        {
            pos.x = LimiteDerecho.x;
        }
        /**/
        if (pos.y < LimiteInf.y)
        {
            pos.y = LimiteInf.y;
        }
        if (pos.y > LimiteSup.y)
        {
            pos.y = LimiteSup.y;
        }
        
        this.transform.position = pos ;
    }
}
