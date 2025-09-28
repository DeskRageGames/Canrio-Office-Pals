using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorRectAnchorCornerTool : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] private bool manualRefresh;
    private RectTransform rectTransform;
    private RectTransform parentRect;
    private void Awake()
    {
        rectTransform = transform as RectTransform;
        parentRect = transform.parent as RectTransform;
    }

    private void Start()
    {
        UpdateAnchors();
    }

    private void Update()
    {
        if (manualRefresh)
        {
            manualRefresh = false;
            UpdateAnchors();
        }
    }

    private void UpdateAnchors()
    {
        RectAnchorsToCornersTool.AnchorsToCorners(rectTransform, parentRect);
    }
#endif
}
