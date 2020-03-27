﻿using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class ShopElement : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private TextMeshProUGUI name;
    [SerializeField]
    private Image characterImage;

    #endregion

    #region Propeties

    public TextMeshProUGUI Name { 
        get => name; 
        private set => name = value; 
    }

    public Image CharacterImage { 
        get => characterImage; 
        private set => characterImage = value; 
    }

    #endregion

    #region Methods

    public void OnBuyButtonClick()
    {
        ShopManager shop = ShopManager.Instance;
        if(shop != null)
        {
            // TODO: Zrobic wywolanie kupna w sklepie.
        }
    }

    public void SetName(string name)
    {
        Name.text = name;
    }

    public void SetImage(Sprite sprite)
    {
        CharacterImage.sprite = sprite;
        //CharacterImage.SetNativeSize();
    }

    #endregion

    #region Handlers



    #endregion
}
