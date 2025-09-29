using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonkey : MonoBehaviour
{

    public GameObject monkey;

    public Transform spawnLocation;

    public void SpawnObject()
    {

        Instantiate(monkey, spawnLocation);

    }
}
