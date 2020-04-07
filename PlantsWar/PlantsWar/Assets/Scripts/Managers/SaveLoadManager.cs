using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SaveLoadManager : ManagerSingletonBase<SaveLoadManager>
{
    #region Fields



    #endregion

    #region Propeties

    public event Action OnResetGame = delegate { };
    public event Action OnLoadGame = delegate { };
    public event Action OnSaveGame = delegate { };

    #endregion

    #region Methods

    public void CallResetGame()
    {
        OnResetGame.Invoke();
    }

    public void CallLoadGame()
    {
        OnLoadGame.Invoke();
    }

    public void CallSaveGame()
    {
        OnSaveGame.Invoke();
    }

    #endregion

    #region Handlers



    #endregion
}
