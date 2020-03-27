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
    private Button selectButton;
    [SerializeField]
    private Button unselectButton;
    [SerializeField]
    private Image selectedMask;

    private string key;

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

    public string Key { 
        get => key; 
        private set => key = value; 
    }

    public TextMeshProUGUI Prize { 
        get => prize; 
        private set => prize = value; 
    }

    public Image SelectedMask { 
        get => selectedMask; 
        private set => selectedMask = value; 
    }

    #endregion

    #region Methods

    public void OnSelectButtonClick()
    {
        ShopManager shop = ShopManager.Instance;
        if(shop != null)
        {
            shop.SetSelectedCharacterBykey(Key);
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

    public void ButtonsToggle()
    {
        if(selectButton.gameObject.activeInHierarchy == true)
        {
            unselectButton.gameObject.SetActive(true);
            selectButton.gameObject.SetActive(false);
        }
        else
        {
            unselectButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(true);
        }
    }

    public void SetUnselectStatus()
    {
        Color transparent = SelectedMask.color;
        transparent.a = 0;

        SelectedMask.color = transparent;
        unselectButton.gameObject.SetActive(false);
        selectButton.gameObject.SetActive(true);
    }

    public void SetSelectStatus()
    {
        Color transparent = SelectedMask.color;
        transparent.a = 0.75f;

        SelectedMask.color = transparent;
        unselectButton.gameObject.SetActive(true);
        selectButton.gameObject.SetActive(false);
    }

    public void SetName(string name)
    {
        Name.text = name;
    }

    public void SetImage(Sprite sprite)
    {
        CharacterImage.sprite = sprite;
    }

    public void SetLocalizeKey(string key)
    {
        Key = key;
    }

    public void SetPrize(int prize)
    {
        Prize.text = prize.ToString();
    }

    private void Awake()
    {
        selectButton.gameObject.SetActive(true);
        unselectButton.gameObject.SetActive(false);
    }

    #endregion

    #region Handlers



    #endregion
}
