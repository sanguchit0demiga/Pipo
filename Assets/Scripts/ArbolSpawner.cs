using UnityEngine;
using System.Collections.Generic;  

public class ArbolSpawner : MonoBehaviour
{
   
    public List<GameObject> prefabsFruta;

    public Transform[] spawnPoints;
    public float tiempoSpawneo = 30f;
    private float tiempoSpawneoActual;

    void Start()
    {
        tiempoSpawneoActual = tiempoSpawneo;
    }

    void Update()
    {
        if (tiempoSpawneoActual <= 0)
        {
            Spawn();
            tiempoSpawneoActual = tiempoSpawneo;
        }
        else
        {
            tiempoSpawneoActual -= Time.deltaTime;
        }
    }

    void Spawn()
    {
        if (prefabsFruta == null || prefabsFruta.Count == 0 || spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("No hay prefabs de fruta o puntos de spawn asignados.");
            return;
        }

        int indicePrefab = UnityEngine.Random.Range(0, prefabsFruta.Count);
        GameObject prefabSeleccionado = prefabsFruta[indicePrefab];

        int indiceSpawnPoint = UnityEngine.Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[indiceSpawnPoint];

        GameObject nuevaFruta = Instantiate(prefabSeleccionado, spawnPoint.position, spawnPoint.rotation);
    }
}