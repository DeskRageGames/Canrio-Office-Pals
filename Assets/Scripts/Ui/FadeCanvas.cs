using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeCanvas : MonoBehaviour
{
    [SerializeField] private float fadeTime = 1;
    [SerializeField] private float fadeTimer = 0;
    [SerializeField] private int targetSceneIndex;
    
    public static FadeCanvas instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void FadeOutToScene(int sceneIndex)
    {
        GetComponent<Animator>().SetTrigger("FadeOut");
        targetSceneIndex = sceneIndex;
        fadeTimer = fadeTime;
    }

    private void FixedUpdate()
    {
        if (fadeTimer > 0)
        {
            fadeTimer -= Time.fixedDeltaTime;

            if (fadeTimer <= 0)
            {
                SceneManager.LoadScene(targetSceneIndex);
            }
        }
    }
}
