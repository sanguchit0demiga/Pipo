using UnityEngine;
using UnityEngine.UI;
public class StatsPlayer : MonoBehaviour
{
    //stats
    [Range(0, 100)] public float hambre = 100f;
    [Range(0, 100)] public float energia = 100f;
    [Range(0, 100)] public float felicidad = 100f;
    [Range(0, 100)] public float limpieza = 100f;
    [Range(0, 100)] public float salud = 100f;

    //ui
    public Image barraHambre;
    public Image barraEnergia;
    public Image barraFelicidad;
    public Image barraLimpieza;
    public Image barraSalud;

    public Image happy;
    public Image normal;
    public Image sad;
    public Image sick;

    void Update()
    {

        //desgaste, cada uno se desgasta a v distinta
        hambre = Mathf.Max(hambre - Time.deltaTime, 0);
        energia = Mathf.Max(energia - Time.deltaTime * 0.3f, 0);
        limpieza = Mathf.Max(limpieza - Time.deltaTime * 0.1f, 0);

        //si la limpieza es menor a 10 entonces esta sucio y se desgasta mas rapido
        float factor = limpieza < 10 ? 2f : 1f;
        felicidad = Mathf.Max(felicidad - Time.deltaTime * factor, 0);

        //actualizar barras
        barraHambre.fillAmount = hambre / 100f;
        barraEnergia.fillAmount = energia / 100f;
        barraFelicidad.fillAmount = felicidad / 100f;
        barraLimpieza.fillAmount = limpieza / 100f;
        barraSalud.fillAmount = salud / 100f;
        EmotionState();
    }

    public void Comer(float cantidad)
    {
        hambre = Mathf.Clamp(hambre + cantidad, 0, 100);
    }
    public void Dormir(float cantidad)
    {
        energia = Mathf.Clamp(energia + cantidad, 0, 100);
    }
    public void Jugar(float cantidad)
    {
        felicidad = Mathf.Clamp(felicidad + cantidad, 0, 100);
    }
    public void Bañar(float cantidad)
    {
        limpieza = Mathf.Clamp(limpieza + cantidad, 0, 100);
    }

    public void EmotionState()
    {

        {
            happy.gameObject.SetActive(false);
            normal.gameObject.SetActive(false);
            sad.gameObject.SetActive(false);
            sick.gameObject.SetActive(false);

            if (limpieza < 10f)
                sick.gameObject.SetActive(true);
            else if (felicidad <= 30f)
                sad.gameObject.SetActive(true);
            else if (felicidad > 70f)
                happy.gameObject.SetActive(true);
            else
                normal.gameObject.SetActive(true);
        }
    }
}
    


