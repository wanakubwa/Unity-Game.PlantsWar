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
        
    }

    public void OnFacebookButton()
    {

    }
    
    #endregion
    
    #region Handlers
    
    
    
    #endregion
    
    #region Enums
    
    
    
    #endregion
} 