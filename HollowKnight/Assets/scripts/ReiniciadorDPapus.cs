using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ReiniciadorDPapus : MonoBehaviour
{ 
    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame) { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
