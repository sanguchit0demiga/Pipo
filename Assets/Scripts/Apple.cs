using UnityEngine;

public class Apple : MonoBehaviour
{
    public float cantidadHambre = 25f; 
    private bool arrastrando = false;

    void Update()
    {
        if (arrastrando)
        {
            Vector2 posMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = posMouse;
        }
    }

    void OnMouseDown()
    {
        arrastrando = true;
    }

    void OnMouseUp()
    {
        arrastrando = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (StatsPlayer.instance != null)
            {
                StatsPlayer.instance.Comer(cantidadHambre);
            }

            Destroy(gameObject);
        }
    }
}