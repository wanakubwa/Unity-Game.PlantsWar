using UnityEngine;

public class TopBarUIController : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private TopBarUIView view;

    [SerializeField]
    private Canvas canvas;

    #endregion
    #region Propeties

    public TopBarUIView View { 
        get => view; 
        private set => view = value; 
    }

    public Canvas Canvas
    {
        get => canvas;
        private set => canvas = value;
    }

    #endregion
    #region Methods

    public void SetCanvasCamera(Camera camera)
    {
        Canvas.worldCamera = camera;
    }
    
    public void SetCoinsText(string text)
    {
        View.SetCoinsText(text);
    }

    public void SetCoinsTextColor(Color color)
    {
        View.SetCoinsTextColor(color);
    }

    public void SetCoinsNumber(int number)
    {
        View.SetCoinsNumber(number);
    }

    public void UpdateLivesStatistics()
    {
        int enemiesLimit = GameplayManager.Instance.EnemiesLimit;
        int playerLives = enemiesLimit - GameplayManager.Instance.EnemiesLimitCounter;

        View.SetLivesNumber(playerLives, enemiesLimit);
    }

    #endregion
    #region Handlers

    #endregion
}
