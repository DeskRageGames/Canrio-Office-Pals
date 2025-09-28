using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Resource", fileName = "New Resource", order = 0)]
public class ResourceScriptable : ScriptableObject
{
    public Sprite icon;
    public int displayOrderIndex = 0;
}
