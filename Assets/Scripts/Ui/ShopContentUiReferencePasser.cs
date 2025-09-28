using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopContentUiReferencePasser : UITextAndImagePasser
{
    public TMP_Text costText;
    public Button Button;
    public ShopItemScriptable itemScriptable;

    public void Initialize()
    {
        image.sprite = itemScriptable.icon;
        text.text = itemScriptable.name;
        costText.text = itemScriptable.cost.ToString();
    }
}
