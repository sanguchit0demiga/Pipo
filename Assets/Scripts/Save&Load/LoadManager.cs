using UnityEngine;

public class LoadManager : MonoBehaviour
{
    public StatsPlayer stats;

    public void SavePlayer()
    {
        Debug.Log("Saved");
        SaveSystem.SavePlayer(stats);
    }

    public void LoadPlayer()
    {
        Debug.Log("Loaded");
        PlayerData data =SaveSystem.LoadPlayer();

        stats.hambre = data.hambre;
        stats.energia = data.energia;
        stats.limpieza = data.limpieza;
        stats.felicidad = data.felicidad;
        stats.exp = data.exp;
        stats.nivel = data.nivel;
    }
}
