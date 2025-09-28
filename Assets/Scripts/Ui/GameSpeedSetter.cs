using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedSetter : MonoBehaviour
{
    public void SetGameSpeed(float speed)
    {
        Time.timeScale = speed;
    }
}
