using UnityEngine;

public class BathtubTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StatsPlayer.InvokeOnBañeraEnteredEvent();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StatsPlayer.InvokeOnBañeraExitedEvent();
        }
    }
}