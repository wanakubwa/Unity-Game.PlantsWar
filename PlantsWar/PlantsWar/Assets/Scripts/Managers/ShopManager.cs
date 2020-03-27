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
    private GameObject characterPrefab;

    [SerializeField]
    private ShopUIController shopUIPrefab;

    private ShopUIController shopUIController;

    #endregion

    #region Propeties

    public GameObject CharacterPrefab {
        get => characterPrefab;
        private set => characterPrefab = value;
    }

    public ShopUIController ShopUIPrefab { 
        get => shopUIPrefab; 
        private set => shopUIPrefab = value; 
    }

    public ShopUIController ShopUIController { 
        get => shopUIController; 
        private set => shopUIController = value; 
    }

    #endregion

    #region Methods

    protected override void OnEnable()
    {
        base.OnEnable();

        InitializeShopUI();

        Debug.LogFormat("[{0}] Zainicjalizowany.".SetColor(Color.green), this.GetType());
    }

    private void InitializeShopUI()
    {
        ShopUIController = Instantiate(shopUIPrefab);
        ShopUIController.transform.SetParent(transform);
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
}
