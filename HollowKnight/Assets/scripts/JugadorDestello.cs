using UnityEngine;

public class JugadorDestello : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer jugadorSpriteRenderer;
    Color color;
    [SerializeField] GameObject prefabDestello;
    //float tiempoTransparente;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        color = Color.white;
        color.a = 0;
        spriteRenderer.color = color;
        //tiempoTransparente = 3;
        //jugadorSpriteRenderer = jugadorSpriteRenderer.GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //color.a -= Time.deltaTime * 2;
        spriteRenderer.color = color;
        spriteRenderer.sprite = jugadorSpriteRenderer.sprite;
    }

    public float ActivarDestello()
    {
        //color.a = 1;
        Instantiate(prefabDestello, this.transform.position, Quaternion.identity);
        return 0.5f; 
    }
    public void SemiDestello()
    {
        color.a = 1f;
    }
    public void NuloDestello() {
        color.a = 0;
    }
}
