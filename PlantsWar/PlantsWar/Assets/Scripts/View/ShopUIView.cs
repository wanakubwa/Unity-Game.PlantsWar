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

    public void AddShopElement(string name, string key, Sprite characterSprite, int prize)
    {
        ShopElement element = Instantiate(ShopItemElement);
        element.transform.SetParent(ContentElement.transform);
        element.gameObject.transform.localScale = ShopItemElement.transform.localScale;

        element.SetName(name);
        element.SetLocalizeKey(key);
        element.SetImage(characterSprite);

        // TODO: Cena.
    }

    #endregion

    #region Handlers



    #endregion
}
