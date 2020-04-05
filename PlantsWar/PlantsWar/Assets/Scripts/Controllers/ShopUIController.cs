using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopUIController : MonoBehaviour
{
    #region Fields

    [Space]
    [SerializeField]
    private ShopUIView view;
    [SerializeField]
    private Canvas canvas;

    #endregion

    #region Propeties

    public ShopUIView View { 
        get => view; 
        private set => view = value; 
    }

    public Canvas Canvas { 
        get => canvas; 
        private set => canvas = value; 
    }

    #endregion

    #region Methods

    public void CreateShopElement(CharacterBase character)
    {
        View.AddShopElement("tmp", character.Type, character.Id, character.Sprite, character.ShopCardBackground, (int)character.Prize);
    }

    public void SetCanvasCamera(Camera camera)
    {
        Canvas.worldCamera = camera;
    }

    public void UnselectAllShopElements()
    {
        View.UnselectAllElements();
    }

    #endregion

    #region Handlers



    #endregion
}
