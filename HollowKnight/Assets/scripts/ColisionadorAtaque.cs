using UnityEngine;

public class ColisionadorAtaque : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Le pegue a un vato: " + other.gameObject.tag);
        if(other.gameObject.tag == "Enemigo")
        {
            enemigo enemigo = other.gameObject.GetComponent<enemigo>();
            enemigo.RecibirDano();
        }
    }
}
