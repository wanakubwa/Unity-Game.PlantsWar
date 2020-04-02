using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class ShopElement : MonoBehaviour
{
    #region Fields

    [Space]
    [SerializeField]
    private TextMeshProUGUI name;
    [SerializeField]
    private TextMeshProUGUI prize;
    [SerializeField]
    private Image characterImage;
    [SerializeField]
    private Image selectedMask;
    [SerializeField]
    private Image backgroundImage;

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

    public TextMeshProUGUI Prize { 
        get => prize; 
        private set => prize = value; 
    }

    public Image SelectedMask { 
        get => selectedMask; 
        private set => selectedMask = value; 
    }

    public int CharacterId {
        get;
        private set;
    }

    public CharacterType Type {
        get;
        private set;
    }

    public bool IsSelected
    {
        get;
        private set;
    } = false;

    public Image BackgroundImage { 
        get => backgroundImage; 
        private set => backgroundImage = value; 
    }

    #endregion

    #region Methods

    public void OnSelectButtonClick()
    {
        ShopManager shop = ShopManager.Instance;
        if(shop != null)
        {
            shop.SetSelectedCharacterByIdAndType(CharacterId, Type);
        }

        ShopManager shopManager = ShopManager.Instance;
        if (shopManager != null)
        {
            shopManager.ShopUIController.UnselectAllShopElements();
        }

        SetSelectStatus();
    }

    public void OnUnselectButtonClick()
    {
        ShopManager shop = ShopManager.Instance;
        if (shop != null)
        {
            shop.UnselectCharacter();
        }

        ShopManager shopManager = ShopManager.Instance;
        if (shopManager != null)
        {
            shopManager.ShopUIController.UnselectAllShopElements();
        }
    }

    public void OnClick()
    {
        if(IsSelected == true)
        {
            ShopManager shop = ShopManager.Instance;
            if (shop != null)
            {
                shop.UnselectCharacter();
                shop.ShopUIController.UnselectAllShopElements();
            }
        }
        else
        {
            ShopManager shop = ShopManager.Instance;
            if (shop != null)
            {
                shop.SetSelectedCharacterByIdAndType(CharacterId, Type);
                shop.ShopUIController.UnselectAllShopElements();
            }

            SetSelectStatus();
        }
    }

    public void SetUnselectStatus()
    {
        Color transparent = SelectedMask.color;
        transparent.a = 0;

        SelectedMask.color = transparent;
        IsSelected = false;
    }

    public void SetSelectStatus()
    {
        Color transparent = SelectedMask.color;
        transparent.a = 0.75f;

        SelectedMask.color = transparent;
        IsSelected = true;
    }

    public void SetName(string name)
    {
        Name.text = name;
    }

    public void SetImage(Sprite sprite)
    {
        CharacterImage.sprite = sprite;
    }

    public void SetPrize(int prize)
    {
        Prize.text = prize.ToString();
    }

    public void SetCharacterId(int id)
    {
        CharacterId = id;
    }

    public void SetCharacterType(CharacterType type)
    {
        Type = type;
    }

    public void SetBackgroundImagaSprite(Sprite sprite)
    {
        BackgroundImage.sprite = sprite;
    }

    #endregion

    #region Handlers



    #endregion
}
