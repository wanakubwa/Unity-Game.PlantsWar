using System;
using UnityEngine;

public class UIManager : ManagerSingletonBase<UIManager>
{
    #region Fields
    
    [SerializeField]
    private TopBarUIController topBarController;
    [SerializeField]
    private EndGameScreenController gameOverScreenController;
    [SerializeField]
    private EndGameScreenController gameWinScreenController;

    #endregion

    #region Propeties

    public TopBarUIController TopBarController { 
        get => topBarController; 
        private set => topBarController = value; 
    }
    public EndGameScreenController GameOverScreenController { 
        get => gameOverScreenController; 
        private set => gameOverScreenController = value; 
    }

    public EndGameScreenController GameWinScreenController { 
        get => gameWinScreenController; 
        private set => gameWinScreenController = value; 
    }

    #endregion

    #region Methods

    protected override void OnEnable()
    {
        base.OnEnable();

        // Inicjalizacja kontrollerow.
        InitializeTopBarUI();
        InitializeGameOverScreen();
        InitializeGameWinScreen();

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

        GameplayManager.Instance.OnGameOver += OnGameOverHandler;
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

        GameplayManager.Instance.OnGameOver -= OnGameOverHandler;
    }
    
    private void InitializeTopBarUI()
    {
        TopBarController = Instantiate(TopBarController);
        TopBarController.SetCanvasCamera(Camera.main);
    }

    private void InitializeGameOverScreen()
    {
        GameOverScreenController = Instantiate(GameOverScreenController);
        GameOverScreenController.SetCanvasCamera(Camera.main);
        GameOverScreenController.ToggleView();
    }

    private void InitializeGameWinScreen()
    {
        GameWinScreenController = Instantiate(GameWinScreenController);
        GameWinScreenController.SetCanvasCamera(Camera.main);
        GameWinScreenController.ToggleView();
    }

    #endregion

    #region Handlers

    private void OnMoneyChangeHandler(int value)
    {
        TopBarController.SetCoinsNumber(value);
    }

    private void OnGameOverHandler()
    {
        GameOverScreenController.ToggleView();
    }
    
    #endregion
    
    #region Enums
    
    
    
    #endregion
}