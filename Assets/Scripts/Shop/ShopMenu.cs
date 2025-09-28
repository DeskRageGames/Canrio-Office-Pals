using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;



[Serializable]
public struct ItemCategory
{
    public ItemType type;
    public Sprite icon;
}
public class ShopMenu : MonoBehaviour
{
    public ShopItemScriptable[] availableItems;
    [SerializeField] private ItemCategory[] categories;


    [Header("Settings")]
    [SerializeField] private float updateButtonsTime = 1f;
  

    [Header("References")] 
    [SerializeField] private GameObject menuPopup;
    [SerializeField] private Transform categoriesParent;
    [SerializeField] private Transform contentParent;

    [SerializeField] private GameObject categoryButtonPrefab;
    [SerializeField] private GameObject contentButtonPrefab;
    [SerializeField] private GameObject contentHorizontalLayoutPrefab;
    
    private float updateButtonsTimer;
    private List<ShopContentUiReferencePasser> contentSlots;
    

    private void Start()
    {
        ClearCategories();
        ClearContentSelection();
        
        PopulateCategories();
        
        if(categories.Length > 0) PopulateContentSection(categories[0]);
    }

    private void Update()
    {
        if (updateButtonsTimer > 0)
        {
            updateButtonsTimer -= Time.deltaTime;

            if (updateButtonsTimer <= 0)
            {
                UpdateButtonPurchasable();
            }
        }
    }

    private void UpdateButtonPurchasable()
    {
        for (int i = 0; i < contentSlots.Count; i++)
        {
            contentSlots[i].Button.interactable = ResourceBank.instance.CanAfford(contentSlots[i].itemScriptable.costType, contentSlots[i].itemScriptable.cost);
        }
        
        updateButtonsTimer = updateButtonsTime;
    }

    private void PopulateCategories()
    {
        for (int i = 0; i < categories.Length; i++)
        {
            GameObject newCategorySlot = Instantiate(categoryButtonPrefab, categoriesParent);
            
            newCategorySlot.GetComponent<UITextAndImagePasser>().image.sprite = categories[i].icon;
            newCategorySlot.GetComponent<UITextAndImagePasser>().text.text = categories[i].type.ToString();
            
            ItemCategory category = categories[i];
            newCategorySlot.GetComponent<Button>().onClick.AddListener(delegate
            {
                ClearContentSelection();
                PopulateContentSection(category);
            });
        }
        
        categoriesParent.GetComponent<VerticalLayoutGroup>().SetLayoutVertical();
    }

    private void PopulateContentSection(ItemCategory category)
    {
        Transform currentRow = Instantiate(contentHorizontalLayoutPrefab, contentParent).transform.GetChild(0);
        contentSlots = new List<ShopContentUiReferencePasser>();
        
        for (int i = 0; i < availableItems.Length; i++)
        {
            if (availableItems[i].type == category.type)
            {
                if (currentRow.childCount >= 3)
                {
                    currentRow = Instantiate(contentHorizontalLayoutPrefab, contentParent).transform.GetChild(0);
                    contentParent.GetComponent<VerticalLayoutGroup>().SetLayoutVertical();
                }
                
                contentSlots.Add(Instantiate(contentButtonPrefab, currentRow).GetComponent<ShopContentUiReferencePasser>());
                contentSlots[^1].itemScriptable = availableItems[i];
                contentSlots[^1].Initialize();
                int index = i;
                contentSlots[^1].Button.onClick.AddListener(delegate { AttemptPurchase(index); });
            }
        }
        
        contentParent.GetComponent<VerticalLayoutGroup>().SetLayoutHorizontal();

        UpdateButtonPurchasable();
    }
    
    
    private void ClearCategories()
    {
        for (int i = 0; i < categoriesParent.childCount; i++)
        {
            Destroy(categoriesParent.GetChild(i).gameObject);
        }
    }

    private void ClearContentSelection()
    {
        for (int i = 0; i < contentParent.childCount; i++)
        {
            Destroy(contentParent.GetChild(i).gameObject);
        }
    }

    public void AttemptPurchase(int index)
    {
        if (ResourceBank.instance.TrySpendResource(availableItems[index].costType, availableItems[index].cost))
        {
            UpdateButtonPurchasable();
        
            ToggleShopMenu(false);
            
            ObjectPlacer.instance.HandlePlacement(availableItems[index].itemPrefab.GetComponent<Placeable>());
        }

    }

    public void ToggleShopMenu(bool open)
    {
        menuPopup.SetActive(open);
        if (open) UpdateButtonPurchasable();
    }
}
