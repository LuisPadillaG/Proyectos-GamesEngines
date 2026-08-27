using UnityEngine;

public class JugadorDestello : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer jugadorSpriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        //jugadorSpriteRenderer = jugadorSpriteRenderer.GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = jugadorSpriteRenderer.sprite;
    }
}
