using UnityEngine;

public class LampController : MonoBehaviour
{
    public static bool lampIsOn = true; 
    public GameObject pointLight; 

    public void ToggleLamp()
    {
        lampIsOn = !lampIsOn;

        if (pointLight != null)
            pointLight.SetActive(lampIsOn); 

        Debug.Log("Lámpara encendida: " + lampIsOn);
    }
}