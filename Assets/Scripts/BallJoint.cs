using UnityEngine;

public class BallJoint : MonoBehaviour

 {
    private TargetJoint2D jointt;
    private Camera cam;
    public float felicidadPorMovimiento = 5f;
    public float velocidadMinima = 0.5f;
    public float tiempoNecesario = 1f;

    private float tiempoEnMovimiento = 0f;
    private Rigidbody2D rb;

    void Start()
    {
        jointt = GetComponent<TargetJoint2D>();
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMouseDown()
    {
        jointt.enabled = true;
        jointt.target = GetMousePos();
    }

    void OnMouseDrag()
    {
        jointt.target = GetMousePos();
    }

    void OnMouseUp()
    {
        jointt.enabled = false;
    }

    Vector2 GetMousePos()
    {
        Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(pos.x, pos.y);
    }
    void Update()
    {
        if (rb.linearVelocity.magnitude > velocidadMinima)
        {
            tiempoEnMovimiento += Time.deltaTime;

            if (tiempoEnMovimiento >= tiempoNecesario)
            {
                if (StatsPlayer.instance != null)
                {
                    StatsPlayer.instance.Jugar(felicidadPorMovimiento);
                    Debug.Log("¡Pipo está feliz jugando con la pelota!");
                }

                tiempoEnMovimiento = 0f; 
            }
        }
        else
        {
            tiempoEnMovimiento = 0f; 
        }
    }
}

