using UnityEngine;

public class MainMenuController : MonoBehaviour 
{
    #region Fields
    

    
    #endregion
    
    #region Propeties
    
    
    
    #endregion
    
    #region Methods
    
    public void OnNewGameButton()
    {
        GameManager.Instance.LoadGameScene(false);
    }

    public void OnContinueGameButton()
    {
        GameManager.Instance.LoadGameScene(true);
    }

    public void OnOptionsButtons()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=4oLnJiYN_GE");
    }

    public void OnFacebookButton()
    {
        Application.OpenURL("https://www.facebook.com/GeekBox-109186153819085/");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
    
    #endregion
    
    #region Handlers
    
    
    
    #endregion
    
    #region Enums
    
    
    
    #endregion
} 