﻿using UnityEngine;
using System.Collections;
using UnityEditor.UI;
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

    public void CreateShopElement(string name, string key, Sprite characterSprite, int prize)
    {
        View.AddShopElement(name, key, characterSprite, prize);
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
