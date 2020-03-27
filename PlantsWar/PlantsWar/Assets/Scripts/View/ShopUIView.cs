using UnityEngine;
using System.Collections;

public class ShopUIView : MonoBehaviour
{
    #region Fields

    [Space]
    [SerializeField]
    private ShopElement shopItemElement;
    [SerializeField]
    private GameObject contentElement;

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

    #endregion

    #region Methods

    public void AddShopElement(string name, Sprite characterSprite, int prize)
    {
        ShopElement element = Instantiate(ShopItemElement);
        element.transform.SetParent(ContentElement.transform);
        element.gameObject.transform.localScale = ShopItemElement.transform.localScale;

        element.SetName(name);
        element.SetImage(characterSprite);

        // TODO: ustawic cene postaci.
    }

    #endregion

    #region Handlers



    #endregion
}
