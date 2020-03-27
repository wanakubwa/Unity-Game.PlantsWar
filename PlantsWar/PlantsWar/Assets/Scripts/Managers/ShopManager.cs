using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ShopManager : ManagerSingletonBase<ShopManager>
{
    #region Fields

    [SerializeField]
    private GameObject characterPrefab;

    #endregion

    #region Propeties

    public GameObject CharacterPrefab {
        get => characterPrefab;
        private set => characterPrefab = value;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        Debug.LogFormat("[{0}] Zainicjalizowany.".SetColor(Color.green), this.GetType());
    }

    #endregion

    #region Methods


    #endregion

    #region Handlers



    #endregion
}
