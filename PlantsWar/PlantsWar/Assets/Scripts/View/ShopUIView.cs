using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopUIView : MonoBehaviour
{
    #region Fields

    [Space]
    [SerializeField]
    private ShopElement shopItemElement;
    [SerializeField]
    private GameObject contentElement;

    private List<ShopElement> shopElements = new List<ShopElement>();

    #endregion

    #region Propeties

    public ShopElement ShopItemElement
    {
        get => shopItemElement;
        private set => shopItemElement = value;
    }

    public GameObject ContentElement { 
        get => contentElement; 
        private set => contentElement = value; 
    }

    public List<ShopElement> ShopElements { 
        get => shopElements; 
        set => shopElements = value; 
    }

    #endregion

    #region Methods

    public void AddShopElement(string name, string key, Sprite characterSprite, int prize)
    {
        ShopElement element = Instantiate(ShopItemElement);
        element.transform.SetParent(ContentElement.transform);
        element.gameObject.transform.localScale = ShopItemElement.transform.localScale;

        element.SetName(name);
        element.SetLocalizeKey(key);
        element.SetImage(characterSprite);
        element.SetPrize(prize);

        // Zapamietanie stworzonego na swiezo elementu.
        ShopElements.Add(element);
    }

    #endregion

    #region Handlers



    #endregion
}
