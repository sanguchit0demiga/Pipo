using UnityEngine;

public class JabonDragEspuma : MonoBehaviour
{
    public GameObject espumaPrefab;
    public float intervalo = 0.5f;
    public float cantidadLimpieza = 3f;

    private bool arrastrando = false;
    private bool tocandoPersonaje = false;
    private float temporizador = 0f;

    void Update()
    {
        // Arrastre
        if (arrastrando)
        {
            Vector2 posMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = posMouse;
        }

        if (tocandoPersonaje)
        {
            temporizador += Time.deltaTime;
            if (temporizador >= intervalo)
            {
                if (StatsPlayer.instance != null && StatsPlayer.instance.EstaEnBañera)
                {
                    if (espumaPrefab != null)
                        Instantiate(espumaPrefab, transform.position, Quaternion.identity);

                    StatsPlayer.instance.Bañar(cantidadLimpieza);
                }

                temporizador = 0f;
            }
        }
    }

    void OnMouseDown() => arrastrando = true;

    void OnMouseUp() => arrastrando = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tocandoPersonaje = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tocandoPersonaje = false;
            temporizador = 0f;
        }
    }
}
