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

    #endregion

    #region Propeties

    public ShopUIController ShopUIPrefab { 
        get => shopUIPrefab; 
        private set => shopUIPrefab = value; 
    }

    public ShopUIController ShopUIController {
        get;
        private set;
    }

    //public SelectedCharacter PositiveCharacter { 
    //    get => positiveCharacter; 
    //    private set => positiveCharacter = value; 
    //}

    public CharacterBase SelectedCharacter {
        get;
        private set;
    }

    #endregion

    #region Methods

    public void SetSelectedCharacterByIdAndType(int id, CharacterType type)
    {
        SelectedCharacter = PositiveCharactersManager.Instance.GetCharacterByIdAndType(id, type);
    }

    public void UnselectCharacter()
    {
        SelectedCharacter = null;
    }

    public bool TryBuySelectedCharacter()
    {
        if (SelectedCharacter == null)
        {
            return false;
        }

        PlayerWalletManager walletManager = PlayerWalletManager.Instance;
        bool canBuy = walletManager.TryAddMoney(-1 * (int)SelectedCharacter.Prize);
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

        UpdateAllPositiveShopCharacters();
    }

    private void UpdateAllPositiveShopCharacters()
    {
        // Pobranie wszystkich postaci tym razem bierzemy z managera postaci.
        PositiveCharactersManager charactersManager = PositiveCharactersManager.Instance;
        foreach (Tuple<CharacterBase, CharacterType> character in charactersManager.GetCharactersAwaibleToBuy())
        {
            ShopUIController.CreateShopElement(character.Item1, character.Item2);
        }
    }

    #endregion

    #region Handlers



    #endregion

    //public class SelectedCharacter
    //{
    //    #region Fields

    //    private string key;
    //    private int prize;
    //    private GameObject characterObject;

    //    #endregion

    //    #region Propeties

    //    public string Key { 
    //        get => key; 
    //        private set => key = value; 
    //    }

    //    public int Prize { 
    //        get => prize; 
    //        private set => prize = value; 
    //    }

    //    public GameObject CharacterObject { 
    //        get => characterObject; 
    //        private set => characterObject = value; 
    //    }

    //    #endregion

    //    #region Methods

    //    public SelectedCharacter(string key, int prize, GameObject characterObj)
    //    {
    //        Key = key;
    //        Prize = prize;
    //        CharacterObject = characterObj;
    //    }

    //    #endregion

    //    #region Handlers



    //    #endregion
    //}
}
