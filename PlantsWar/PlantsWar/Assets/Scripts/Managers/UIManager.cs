using UnityEngine;

public class UIManager : ManagerSingletonBase<UIManager>
{
    #region Fields
    
    [SerializeField]
    private TopBarUIController topBarController;

    #endregion

    #region Propeties

    public TopBarUIController TopBarController { 
        get => topBarController; 
        private set => topBarController = value; 
    }

    #endregion

    #region Methods

    protected override void OnEnable() 
    {
        base.OnEnable();

        Debug.LogFormat("[{0}] Zainicjalizowany.".SetColor(Color.green), this.GetType());
    }

    protected override void AttachEvents()
    {
        base.AttachEvents();

        PlayerWalletManager walletManager = PlayerWalletManager.Instance;
        if(walletManager != null)
        {
            walletManager.OnMoneyChange += OnMoneyChangeHandler;
        }
        else
        {
            Debug.LogErrorFormat("[{0}] Was null! in [{1}]", typeof(PlayerWalletManager), GetType());
        }
    }

    protected override void DettachEvents()
    {
        base.DettachEvents();

        PlayerWalletManager walletManager = PlayerWalletManager.Instance;
        if(walletManager != null)
        {
            walletManager.OnMoneyChange -= OnMoneyChangeHandler;
        }
        else
        {
            Debug.LogErrorFormat("[{0}] Was null! in [{1}]", typeof(PlayerWalletManager), GetType());
        }
    }
    
    #endregion
    
    #region Handlers
    
    private void OnMoneyChangeHandler(int value)
    {
        TopBarController.SetCoinsNumber(value);
    }
    
    #endregion
    
    #region Enums
    
    
    
    #endregion
}