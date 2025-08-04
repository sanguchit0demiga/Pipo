using UnityEngine;
using UnityEngine.SceneManagement;
using System; 
using System.Collections.Generic; 

public class StatsPlayer : MonoBehaviour
{
    public static StatsPlayer instance;

    [Range(0, 100)] public float hambre = 100f;
    [Range(0, 100)] public float energia = 100f;
    [Range(0, 100)] public float felicidad = 100f;
    [Range(0, 100)] public float limpieza = 100f;

    private bool estaEnCama = false;
    private bool estaEnBa�era = false;
    public bool EstaEnBa�era => estaEnBa�era;
    private float exp = 0f;
    private int nivel = 1;
    private float tiempoExp = 0f;

    private static event Action OnBedEnteredEvent;
    private static event Action OnBedExitedEvent;
    private static event Action OnBa�eraEnteredEvent;
    private static event Action OnBa�eraExitedEvent;

    public static void InvokeOnBedEnteredEvent() => OnBedEnteredEvent?.Invoke();
    public static void InvokeOnBedExitedEvent() => OnBedExitedEvent?.Invoke();
    public static void InvokeOnBa�eraEnteredEvent() => OnBa�eraEnteredEvent?.Invoke();
    public static void InvokeOnBa�eraExitedEvent() => OnBa�eraExitedEvent?.Invoke();

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

        Fruit.OnFruitCollected += HandleFruitCollected;
        OnBedEnteredEvent += () => { estaEnCama = true; Debug.Log("Pipo entr� en la cama."); };
        OnBedExitedEvent += () => { estaEnCama = false; Debug.Log("Pipo sali� de la cama."); };
        OnBa�eraEnteredEvent += () => { estaEnBa�era = true; Debug.Log("Pipo entr� en la ba�era."); };
        OnBa�eraExitedEvent += () => { estaEnBa�era = false; Debug.Log("Pipo sali� de la ba�era."); };
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        Fruit.OnFruitCollected -= HandleFruitCollected;
        OnBedEnteredEvent -= () => { estaEnCama = true; Debug.Log("Pipo entr� en la cama."); };
        OnBedExitedEvent -= () => { estaEnCama = false; Debug.Log("Pipo sali� de la cama."); };
        OnBa�eraEnteredEvent -= () => { estaEnBa�era = true; Debug.Log("Pipo entr� en la ba�era."); };
        OnBa�eraExitedEvent -= () => { estaEnBa�era = false; Debug.Log("Pipo sali� de la ba�era."); };
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
            UIManager.instance.barraExp == null ||
            UIManager.instance.nivelText == null
        ))
        {
            timeout -= Time.deltaTime;
            yield return null;
        }

        RefreshUI();
    }

    void Update()
    {
        hambre = Mathf.Max(hambre - Time.deltaTime * 0.5f, 0);
        limpieza = Mathf.Max(limpieza - Time.deltaTime * 0.6f, 0);

        float factor = limpieza < 10 ? 2f : 1f;
        felicidad = Mathf.Max(felicidad - Time.deltaTime * 0.4f, 0);

        if (estaEnCama && !LampController.lampIsOn)
        {
            energia = Mathf.Min(energia + Time.deltaTime * 0.5f, 100f);
        }
        else
        {
            energia = Mathf.Max(energia - Time.deltaTime * 0.5f, 0);
        }
        EarnExp();
        RefreshUI();
    }

    void RefreshUI()
    {
        if (UIManager.instance == null) return;
        if (
            UIManager.instance.barraHambre == null ||
            UIManager.instance.barraEnergia == null ||
            UIManager.instance.barraFelicidad == null ||
            UIManager.instance.barraLimpieza == null
        ) return;

        UIManager.instance.barraHambre.fillAmount = hambre / 100f;
        UIManager.instance.barraEnergia.fillAmount = energia / 100f;
        UIManager.instance.barraFelicidad.fillAmount = felicidad / 100f;
        UIManager.instance.barraLimpieza.fillAmount = limpieza / 100f;

        EmotionState();

        if (UIManager.instance.barraExp != null)
            UIManager.instance.barraExp.value = exp;

        if (UIManager.instance.nivelText != null)
            UIManager.instance.nivelText.text = nivel.ToString();
    }

    public void Comer(float cantidad) => hambre = Mathf.Clamp(hambre + cantidad, 0, 100);
    public void Dormir(float cantidad) => energia = Mathf.Clamp(energia + cantidad, 0, 100);
    public void Jugar(float cantidad) => felicidad = Mathf.Clamp(felicidad + cantidad, 0, 100);
    public void Ba�ar(float cantidad) => limpieza = Mathf.Clamp(limpieza + cantidad, 0, 100);

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COLISI�N DETECTADA con " + collision.gameObject.name);
    }

    void EarnExp()
    {
        tiempoExp += Time.deltaTime;
        if (tiempoExp >= 25f)
        {
            tiempoExp = 0f;
            exp += 2f;
            if (exp >= 10f)
            {
                exp -= 10f;
                nivel++;
                RefreshUI();
            }
        }
    }

    private void HandleFruitCollected(float cantidadHambre)
    {
        Comer(cantidadHambre);
    }
}