using UnityEngine;

public class BedTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StatsPlayer.InvokeOnBedEnteredEvent();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StatsPlayer.InvokeOnBedExitedEvent();
        }
    }
}