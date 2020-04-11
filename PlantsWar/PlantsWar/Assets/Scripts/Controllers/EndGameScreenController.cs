using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EndGameScreenController : MonoBehaviour
{
    #region Fields

    [Space]
    [SerializeField]
    private EndGameScreenView view;
    [SerializeField]
    private Canvas canvas;

    #endregion

    #region Propeties

    public EndGameScreenView View { 
        get => view; 
        private set => view = value; 
    }

    public Canvas Canvas { 
        get => canvas; 
        private set => canvas = value; 
    }

    #endregion

    #region Methods

    public void SetCanvasCamera(Camera camera)
    {
        Canvas.worldCamera = camera;
    }

    public void ToggleView()
    {
        if(gameObject.activeInHierarchy == true)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public void ResetGame()
    {
        SaveLoadManager.Instance.CallResetGame();
        GameEventsManager.Instance.OnGameFreezNotify(false);
        ToggleView();
    }

    public void NextLvl()
    {
        //TODO;
    }

    #endregion

    #region Handlers



    #endregion
}
