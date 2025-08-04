using UnityEngine;
using UnityEngine.SceneManagement;

public class Pipo : MonoBehaviour
{
    private TargetJoint2D jointt;
    private Camera cam;
    private Vector3 offset;

    void Start()
    {
        jointt = GetComponent<TargetJoint2D>();
        cam = Camera.main;
    }

    void OnMouseDown()
    {
        jointt.enabled = true;
        jointt.target = GetMousePos();

        if (!LampController.lampIsOn) return;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mousePos.x, mousePos.y, transform.position.z);
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

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject spawnPoint = GameObject.Find("SpawnPoint");
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
            transform.rotation = spawnPoint.transform.rotation;
            Debug.Log($"Jugador spawneado en {spawnPoint.name} en escena {scene.name}");
        }
        else
        {
            Debug.LogWarning("No se encontró SpawnPoint en la escena " + scene.name);
        }
    }


}
