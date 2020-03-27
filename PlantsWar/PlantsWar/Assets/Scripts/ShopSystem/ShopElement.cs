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

    #endregion

    #region Methods

    public void OnSelectButtonClick()
    {
        ShopManager shop = ShopManager.Instance;
        if(shop != null)
        {
            shop.SetSelectedCharacterBykey(Key);
        }

        ButtonsToggle();
    }

    public void OnUnselectButtonClick()
    {
        ShopManager shop = ShopManager.Instance;
        if (shop != null)
        {
            shop.UnselectCharacter();
        }

        ButtonsToggle();
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

    public void SetName(string name)
    {
        Name.text = name;
    }

    public void SetImage(Sprite sprite)
    {
        CharacterImage.sprite = sprite;
        //CharacterImage.SetNativeSize();
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
