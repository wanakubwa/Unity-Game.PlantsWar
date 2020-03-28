using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static CharactersContainerSetup;

public class ShopManager : ManagerSingletonBase<ShopManager>
{
    #region Fields

    [Space]
    [SerializeField]
    private ShopUIController shopUIPrefab;

    private SelectedCharacter positiveCharacter;
    private ShopUIController shopUIController;

    #endregion

    #region Propeties

    public ShopUIController ShopUIPrefab { 
        get => shopUIPrefab; 
        private set => shopUIPrefab = value; 
    }

    public ShopUIController ShopUIController { 
        get => shopUIController; 
        private set => shopUIController = value; 
    }

    public SelectedCharacter PositiveCharacter { 
        get => positiveCharacter; 
        private set => positiveCharacter = value; 
    }

    #endregion

    #region Methods

    public void SetSelectedCharacterBykey(string key)
    {
        CharactersContainerSetup charactersContainer = CharactersContainerSetup.Instance;
        if(charactersContainer != null)
        {
            SingleCharacter character = charactersContainer.GetPositiveCharacterByKey(key);
            if(character != null)
            {
                PositiveCharacter = new SelectedCharacter(character.Key, character.Prize, character.CharacterPrefab);
            }
        }
    }

    public void UnselectCharacter()
    {
        PositiveCharacter = null;
    }

    public bool TryBuySelectedCharacter()
    {
        PlayerWalletManager walletManager = PlayerWalletManager.Instance;
        bool canBuy = walletManager.TryAddMoney(-1 * PositiveCharacter.Prize);
        return canBuy;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        InitializeShopUI();

        Debug.LogFormat("[{0}] Zainicjalizowany.".SetColor(Color.green), GetType());
    }

    private void InitializeShopUI()
    {
        ShopUIController = Instantiate(ShopUIPrefab);
        ShopUIController.SetCanvasCamera(Camera.main);

        CharactersContainerSetup charactersContainer = CharactersContainerSetup.Instance;
        if(charactersContainer != null)
        {
            CreateAllPositiveShopCharacters(charactersContainer.PositiveCharacters);
        }
        else
        {
            Debug.LogErrorFormat("Watchout! [{0}] Was null!", charactersContainer.GetType());
        }
    }

    private void CreateAllPositiveShopCharacters(List<SingleCharacter> characters)
    {
        foreach (SingleCharacter character in characters)
        {
            // TODO: Dalej trzeba zrobic normalnie - Fabian.
            SpriteRenderer sprite = character.CharacterPrefab.GetComponentInChildren<SpriteRenderer>();
            ShopUIController.CreateShopElement("Postac_tmp", character.Key, sprite.sprite, character.Prize);
        }
    }

    #endregion

    #region Handlers



    #endregion

    public class SelectedCharacter
    {
        #region Fields

        private string key;
        private int prize;
        private GameObject characterObject;

        #endregion

        #region Propeties

        public string Key { 
            get => key; 
            private set => key = value; 
        }

        public int Prize { 
            get => prize; 
            private set => prize = value; 
        }

        public GameObject CharacterObject { 
            get => characterObject; 
            private set => characterObject = value; 
        }

        #endregion

        #region Methods

        public SelectedCharacter(string key, int prize, GameObject characterObj)
        {
            Key = key;
            Prize = prize;
            CharacterObject = characterObj;
        }

        #endregion

        #region Handlers



        #endregion
    }
}
