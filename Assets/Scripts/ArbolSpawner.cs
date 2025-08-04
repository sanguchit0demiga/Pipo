using UnityEngine;

public class ArbolSpawner : MonoBehaviour
{
    public GameObject manzanaPrefab; 
    public Transform[] spawnPoints; 

    public float tiempoEntreSpawns = 30f;
    private float tiempoRestante;

    private void Start()
    {
        tiempoRestante = tiempoEntreSpawns;
    }

    private void Update()
    {
        tiempoRestante -= Time.deltaTime;

        if (tiempoRestante <= 0f)
        {
            SpawnearManzana();
            tiempoRestante = tiempoEntreSpawns;
        }
    }

    void SpawnearManzana()
    {
        int indice = Random.Range(0, spawnPoints.Length);
        Instantiate(manzanaPrefab, spawnPoints[indice].position, Quaternion.identity);
    }
}
