using UnityEngine;
using System;

public abstract class Fruit : MonoBehaviour, ICollectible
{
    public float cantidadHambre = 10f;

    private bool arrastrando = false;

    public static event Action<float> OnFruitCollected;

    public virtual void Collect()
    {
        OnFruitCollected?.Invoke(cantidadHambre);
        Destroy(gameObject);
    }

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
            Collect();
        }
    }
}