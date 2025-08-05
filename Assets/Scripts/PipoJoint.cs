using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(TargetJoint2D))]
public class PipoJoint : MonoBehaviour
{
    private TargetJoint2D joint;
    private Camera cam;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        cam = Camera.main;
        joint = GetComponent<TargetJoint2D>();

        joint.enabled = false;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        cam = Camera.main;

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

    void OnMouseDown()
    {
        if (!LampController.lampIsOn || cam == null) return;

        joint.enabled = true;
        joint.target = GetMouseWorldPosition();
    }

    void OnMouseDrag()
    {
        if (joint.enabled && cam != null)
        {
            joint.target = GetMouseWorldPosition();
        }
    }

    void OnMouseUp()
    {
        joint.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    private Vector2 GetMouseWorldPosition()
    {
        return cam != null
            ? (Vector2)cam.ScreenToWorldPoint(Input.mousePosition)
            : Vector2.zero;
    }
}
