using System;
using UnityEngine;

public class GameEventsManager : ManagerSingletonBase<GameEventsManager>, ISaveable
{
    #region Fields

    #endregion

    #region Propeties

    public event Action<bool> OnGameFreez = delegate { };

    #endregion

    #region Methods

    public void OnGameFreezNotify(bool isFreezed)
    {
        OnGameFreez.Invoke(isFreezed);
    }

    public void ResetFields()
    {
        
    }

    public void Load()
    {
        
    }

    public void Save()
    {
        
    }

    #endregion

    #region Handlers

    #endregion

    #region Enums

    #endregion
}