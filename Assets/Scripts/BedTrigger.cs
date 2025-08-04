using UnityEngine;

public class BedTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (StatsPlayer.instance != null)
            {
                StatsPlayer.instance.OnBedEntered();
                Debug.Log("Trigger de la cama detectado por el script BedTrigger.");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (StatsPlayer.instance != null)
            {
                StatsPlayer.instance.OnBedExited();
                Debug.Log("El personaje salió del trigger de la cama.");
            }
        }
    }
}