using System;
using UnityEngine;

public class UIManager : ManagerSingletonBase<UIManager>, IContentLoadable
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
    public TopBarUIController TopBarControllerPrefab
    {
        get => topBarController;
        private set => topBarController = value;
    }

    public EndGameScreenController GameOverScreenControllerPrefab
    {
        get => gameOverScreenController;
        private set => gameOverScreenController = value;
    }

    public EndGameScreenController GameWinScreenControllerPrefab
    {
        get => gameWinScreenController;
        private set => gameWinScreenController = value;
    }

    public TopBarUIController TopBarController
    {
        get;
        private set;
    }

    public EndGameScreenController GameOverScreenController
    {
        get;
        private set; 
    }

    public EndGameScreenController GameWinScreenController
    {
        get;
        private set;
    }

    #endregion

    #region Methods

    public void LoadGameContent()
    {
        // Inicjalizacja kontrollerow.
        InitializeTopBarUI();
        InitializeGameOverScreen();
        InitializeGameWinScreen();
        TopBarController.SetCoinsNumber(PlayerWalletManager.Instance.Money);
        TopBarController.UpdateLivesStatistics();
    }

    public void FreeGameContent()
    {
        Destroy(TopBarController);
        Destroy(GameOverScreenController);
        Destroy(GameWinScreenController);
    }

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

        GameplayManager.Instance.OnGameOver += OnGameOverHandler;
        GameplayManager.Instance.OnGameWin += OnGameWinHandler;
        GameplayManager.Instance.OnEnemiesLimitCounterChange += OnEnemiesCounterChangedHandler;
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

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
        GameplayManager.Instance.OnGameWin -= OnGameWinHandler;
        GameplayManager.Instance.OnEnemiesLimitCounterChange -= OnEnemiesCounterChangedHandler;
    }

    
    private void InitializeTopBarUI()
    {
        TopBarController = Instantiate(TopBarControllerPrefab);
        TopBarController.SetCanvasCamera(Camera.main);
    }

    private void InitializeGameOverScreen()
    {
        GameOverScreenController = Instantiate(GameOverScreenControllerPrefab);
        GameOverScreenController.SetCanvasCamera(Camera.main);
        GameOverScreenController.ToggleView();
    }

    private void InitializeGameWinScreen()
    {
        GameWinScreenController = Instantiate(GameWinScreenControllerPrefab);
        GameWinScreenController.SetCanvasCamera(Camera.main);
        GameWinScreenController.ToggleView();
    }

    #endregion

    #region Handlers

    private void OnMoneyChangeHandler(int value)
    {
        if(TopBarController != null)
        {
            TopBarController.SetCoinsNumber(value);
        }
    }

    private void OnGameOverHandler()
    {
        GameOverScreenController.ToggleView();
    }

    private void OnGameWinHandler()
    {
        GameWinScreenController.ToggleView();
    }

    private void OnEnemiesCounterChangedHandler(int value)
    {
        if(TopBarController != null)
        {
            TopBarController.UpdateLivesStatistics();
        }
    }

    #endregion

    #region Enums



    #endregion
}