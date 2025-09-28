using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    public void LoadScene(int index)
    {
        FadeCanvas.instance.FadeOutToScene(index);
    }
}
