using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RectAnchorsToCornersTool
{
    public static void AnchorsToCorners(RectTransform rectTransform, RectTransform parentRectTransform)
    {
       Vector2 anchorVector = Vector2.zero;
       Rect anchorRect = new Rect();
       
       float pivotX = anchorRect.width * rectTransform.pivot.x;
       float pivotY = anchorRect.height * (1 - rectTransform.pivot.y);
       Vector2 newXY = new Vector2(rectTransform.anchorMin.x * parentRectTransform.rect.width + rectTransform.offsetMin.x + pivotX - parentRectTransform.rect.width * anchorVector.x,
          - (1 - rectTransform.anchorMax.y) * parentRectTransform.rect.height + rectTransform.offsetMax.y - pivotY + parentRectTransform.rect.height * (1 - anchorVector.y));
       anchorRect.x = newXY.x;
       anchorRect.y = newXY.y;

       anchorRect.width = rectTransform.rect.width;
       anchorRect.height = rectTransform.rect.height;
       
       rectTransform.anchorMin = new Vector2(0f, 1f);
       rectTransform.anchorMax = new Vector2(0f, 1f);
       rectTransform.offsetMin = new Vector2(anchorRect.x / rectTransform.localScale.x, anchorRect.y / rectTransform.localScale.y - anchorRect.height);
       rectTransform.offsetMax = new Vector2(anchorRect.x / rectTransform.localScale.x + anchorRect.width, anchorRect.y / rectTransform.localScale.y);
       rectTransform.anchorMin = new Vector2(rectTransform.anchorMin.x + anchorVector.x + (rectTransform.offsetMin.x - pivotX) / parentRectTransform.rect.width * rectTransform.localScale.x,
                                      rectTransform.anchorMin.y - (1 - anchorVector.y) + (rectTransform.offsetMin.y + pivotY) / parentRectTransform.rect.height * rectTransform.localScale.y);
       rectTransform.anchorMax = new Vector2(rectTransform.anchorMax.x + anchorVector.x + (rectTransform.offsetMax.x - pivotX) / parentRectTransform.rect.width * rectTransform.localScale.x,
                                      rectTransform.anchorMax.y - (1 - anchorVector.y) + (rectTransform.offsetMax.y + pivotY) / parentRectTransform.rect.height * rectTransform.localScale.y);
       rectTransform.offsetMin = new Vector2((0 - rectTransform.pivot.x) * anchorRect.width * (1 - rectTransform.localScale.x), (0 - rectTransform.pivot.y) * anchorRect.height * (1 - rectTransform.localScale.y));
       rectTransform.offsetMax = new Vector2((1 - rectTransform.pivot.x) * anchorRect.width * (1 - rectTransform.localScale.x), (1 - rectTransform.pivot.y) * anchorRect.height * (1 - rectTransform.localScale.y));
    }
}
