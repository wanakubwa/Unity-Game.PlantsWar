using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : ManagerSingletonBase<ShopManager>
{
    #region Fields

    [Space]
    [SerializeField]
    private GameObject characterPrefab;

    [SerializeField]
    private ShopUIController shopUIPrefab;

    [Space]
    [SerializeField]
    private List<GameObject> inGameCharacters = new List<GameObject>();

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

    public List<GameObject> InGameCharacters { 
        get => inGameCharacters; 
        private set => inGameCharacters = value; 
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

        foreach (GameObject character in InGameCharacters)
        {
            // TODO: przerobic normalnie.
            SpriteRenderer sprite = character.GetComponentInChildren<SpriteRenderer>();
            ShopUIController.CreateShopElement("Postac", sprite.sprite, 100);
        }
    }

    #endregion

    #region Handlers



    #endregion
}
