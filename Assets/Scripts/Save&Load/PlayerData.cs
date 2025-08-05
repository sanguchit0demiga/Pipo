using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData : MonoBehaviour
{
    public int level;
    public float hambre;
    public float energia;
    public float limpieza;
    public float felicidad;

    public PlayerData(StatsPlayer player)
    {
        hambre = player.hambre;
        energia = player.energia;
        limpieza = player.limpieza;
        felicidad= player.felicidad;
    }
}
