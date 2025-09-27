using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyBotChance : MonoBehaviour
{
    public GameObject tinyBot;
    public int chanceToSpawn;

    private void OnEnable()
    {
        int chance = Random.Range(1, chanceToSpawn);
        if(chance == 1)
        {
            tinyBot.SetActive(true);
        }
    }
}
