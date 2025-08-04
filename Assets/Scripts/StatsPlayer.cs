using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsPlayer : MonoBehaviour
{
    public static StatsPlayer instance;

    [Range(0, 100)] public float hambre = 100f;
    [Range(0, 100)] public float energia = 100f;
    [Range(0, 100)] public float felicidad = 100f;
    [Range(0, 100)] public float limpieza = 100f;
    [Range(0, 100)] public float salud = 100f;

    private bool estaEnCama = false;
    private bool estaEnBañera = false;
    public bool EstaEnBañera => estaEnBañera;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(WaitForUIReady());
    }

    System.Collections.IEnumerator WaitForUIReady()
    {
        float timeout = 1f;
        while (timeout > 0f && (
            UIManager.instance == null ||
            UIManager.instance.barraHambre == null ||
            UIManager.instance.barraEnergia == null ||
            UIManager.instance.barraFelicidad == null ||
            UIManager.instance.barraLimpieza == null ||
            UIManager.instance.barraSalud == null
        ))
        {
            timeout -= Time.deltaTime;
            yield return null;
        }

        RefreshUI();
    }

    void Update()
    {
        hambre = Mathf.Max(hambre - Time.deltaTime, 0);
        limpieza = Mathf.Max(limpieza - Time.deltaTime * 0.1f, 0);

        float factor = limpieza < 10 ? 2f : 1f;
        felicidad = Mathf.Max(felicidad - Time.deltaTime * factor, 0);

        if (estaEnCama && !LampController.lampIsOn)
           
        {

            energia = Mathf.Min(energia + Time.deltaTime * 5f, 100f);
            Debug.Log("Recuperando energía: en cama y luz apagada.");
        }
        else
        {
            energia = Mathf.Max(energia - Time.deltaTime * 0.3f, 0); 
        }

        RefreshUI();
    }

    void RefreshUI()
    {
        if (UIManager.instance == null) return;
        if (
            UIManager.instance.barraHambre == null ||
            UIManager.instance.barraEnergia == null ||
            UIManager.instance.barraFelicidad == null ||
            UIManager.instance.barraLimpieza == null ||
            UIManager.instance.barraSalud == null
        ) return;

        UIManager.instance.barraHambre.fillAmount = hambre / 100f;
        UIManager.instance.barraEnergia.fillAmount = energia / 100f;
        UIManager.instance.barraFelicidad.fillAmount = felicidad / 100f;
        UIManager.instance.barraLimpieza.fillAmount = limpieza / 100f;
        UIManager.instance.barraSalud.fillAmount = salud / 100f;

        EmotionState();
    }

    public void Comer(float cantidad) => hambre = Mathf.Clamp(hambre + cantidad, 0, 100);
    public void Dormir(float cantidad) => energia = Mathf.Clamp(energia + cantidad, 0, 100);
    public void Jugar(float cantidad) => felicidad = Mathf.Clamp(felicidad + cantidad, 0, 100);
    public void Bañar(float cantidad) => limpieza = Mathf.Clamp(limpieza + cantidad, 0, 100);

    public void EmotionState()
    {
        if (UIManager.instance == null) return;
        if (
            UIManager.instance.happy == null ||
            UIManager.instance.normal == null ||
            UIManager.instance.sad == null ||
            UIManager.instance.sick == null
        ) return;

        UIManager.instance.happy.gameObject.SetActive(false);
        UIManager.instance.normal.gameObject.SetActive(false);
        UIManager.instance.sad.gameObject.SetActive(false);
        UIManager.instance.sick.gameObject.SetActive(false);

        if (limpieza < 10f)
            UIManager.instance.sick.gameObject.SetActive(true);
        else if (felicidad <= 30f)
            UIManager.instance.sad.gameObject.SetActive(true);
        else if (felicidad > 70f)
            UIManager.instance.happy.gameObject.SetActive(true);
        else
            UIManager.instance.normal.gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "TriggerDePrueba")
        {
            Debug.Log("Trigger de prueba DETECTADO. La colisión funciona.");
        }
        if (other.CompareTag("Bed"))
        {
            estaEnCama = true;
            Debug.Log("Pipo entró en la cama.");
        }
        if (other.CompareTag("Bathtub"))
        {
            estaEnBañera = true;
            Debug.Log("Pipo entró en la bañera.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Bed"))
        {
            estaEnCama = false;
            Debug.Log("Pipo salió de la cama.");
        }
        if (other.CompareTag("Bathtub"))
        {
            estaEnBañera = false;
            Debug.Log("Pipo salió de la bañera.");
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COLISIÓN DETECTADA con " + collision.gameObject.name);
    }
    public void OnBedEntered()
    {
        estaEnCama = true;
    }

    public void OnBedExited()
    {
        estaEnCama = false;
    }
    public void OnBañeraEntered()
    {
        estaEnBañera = true;
    }

    public void OnBañeraExited()
    {
        estaEnBañera = false;
    }
}
