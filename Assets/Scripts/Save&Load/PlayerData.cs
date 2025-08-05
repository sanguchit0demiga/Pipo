using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData : MonoBehaviour
{
    public float hambre;
    public float energia;
    public float limpieza;
    public float felicidad;
    public float exp;
    public int nivel;

    public PlayerData(StatsPlayer player)
    {
        hambre = player.hambre;
        energia = player.energia;
        limpieza = player.limpieza;
        felicidad= player.felicidad;
        exp = player.exp;
        nivel = player.nivel;
    }
}
