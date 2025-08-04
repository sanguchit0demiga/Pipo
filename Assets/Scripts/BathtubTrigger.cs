using UnityEngine;

public class BathtubTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (StatsPlayer.instance != null)
            {
                StatsPlayer.instance.OnBañeraEntered();
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (StatsPlayer.instance != null)
            {
                StatsPlayer.instance.OnBañeraExited();
                
            }
        }
    }
}