using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 lastMousePos;
    private Vector3 currentVelocity;
    private bool isDragging = false;


    public float velocityMultiplier = 1f;  // Ajusta qué tan fuerte sale según velocidad del mouse

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMouseDown()
    {
        isDragging = true;
        lastMousePos = GetMouseWorldPos();

        // Cancelar movimiento físico
        currentVelocity = rb.linearVelocity;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void OnMouseDrag()
    {
        if (!isDragging) return;

        Vector3 mouseWorldPos = GetMouseWorldPos();

        // Calcular velocidad en unidades por segundo (frame independiente)
        currentVelocity = (mouseWorldPos - lastMousePos) / Time.deltaTime;

        transform.position = mouseWorldPos;
        lastMousePos = mouseWorldPos;
    }

    void OnMouseUp()
    {
        if (!isDragging) return;

        rb.bodyType = RigidbodyType2D.Dynamic;

        // Aplicar impulso según velocidad del mouse
        rb.AddForce(currentVelocity * velocityMultiplier * Time.fixedDeltaTime, ForceMode2D.Impulse);

        isDragging = false;
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }
}
