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
        foreach (CharacterBase character in charactersManager.GetCharactersAwaibleToBuy())
        {
            ShopUIController.CreateShopElement(character);
        }
    }

    #endregion

    #region Handlers



    #endregion
}
