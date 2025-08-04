using UnityEngine;

public class LampController : MonoBehaviour
{
    public static bool lampIsOn = true;
    public GameObject pointLight;

    private Animator playerAnimator;

    void Start()
    {
       
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerAnimator = player.GetComponent<Animator>();
        }
        else
        {
            Debug.LogWarning("No se encontró el jugador con la tag 'Player'");
        }
    }

    public void ToggleLamp()
    {
        lampIsOn = !lampIsOn;
        Debug.Log("Botón presionado. Estado de la lámpara: " + lampIsOn);

        if (pointLight != null)
            pointLight.SetActive(lampIsOn);

        if (playerAnimator != null)
            playerAnimator.enabled = lampIsOn; 

        Debug.Log("Lámpara encendida: " + lampIsOn);
    }
}
