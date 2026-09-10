using UnityEngine;
using UnityEngine.Rendering.Universal;

public class VengefulSpirit : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 2f);
        
    }

    void Update()
    {
        this.transform.Translate(Vector3.right * 25 * Time.deltaTime); 
    }
}
