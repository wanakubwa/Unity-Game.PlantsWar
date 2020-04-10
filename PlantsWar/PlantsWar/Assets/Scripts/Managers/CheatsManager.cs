using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CheatsManager : ManagerSingletonBase<CheatsManager>
{
    #region Fields

    [SerializeField]
    private Button saveData;
    [SerializeField]
    private Button loadData;

    #endregion

    #region Propeties

    public Button SaveData {
        get => saveData;
        private set => saveData = value;
    }

    public Button LoadData {
        get => loadData;
        private set => loadData = value;
    }

    #endregion

    #region Methods

    public void OnSaveDataButton()
    {
        SaveLoadManager.Instance.CallSaveGame();
    }

    public void OnLoadDataButton()
    {
        SaveLoadManager.Instance.CallLoadGame();
    }

    #endregion

    #region Handlers



    #endregion
}
