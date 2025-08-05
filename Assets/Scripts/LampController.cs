using UnityEngine;

public class LampController : MonoBehaviour
{
    public static bool lampIsOn = true;
    public GameObject pointLight;

    private Animator playerAnimator;

    public AudioSource audioSource;
    public AudioClip soundOn;
    public AudioClip soundOff;
    void Start()
    {
       
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerAnimator = player.GetComponent<Animator>();
        }
        else
        {
            Debug.LogWarning("No se encontr� el jugador con la tag 'Player'");
        }
    }

    public void ToggleLamp()
    {
        lampIsOn = !lampIsOn;
        Debug.Log("Bot�n presionado. Estado de la l�mpara: " + lampIsOn);

        if (pointLight != null)
            pointLight.SetActive(lampIsOn);

        if (playerAnimator != null)
            playerAnimator.enabled = lampIsOn; 

        Debug.Log("L�mpara encendida: " + lampIsOn);
        if (audioSource != null)
        {
            if (lampIsOn && soundOn != null)
                audioSource.PlayOneShot(soundOn);
            else if (!lampIsOn && soundOff != null)
                audioSource.PlayOneShot(soundOff);
        }
    }
}
