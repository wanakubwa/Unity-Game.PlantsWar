using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class GridSelectorManager : ManagerSingletonBase<GridSelectorManager>
{
    #region Fields

    public event Action<int> OnGridCellClick = delegate { };


    #endregion

    #region Propeties



    #endregion

    #region Methods

    protected override void OnEnable()
    {
        base.OnEnable();

        Debug.LogFormat("[{0}] Zainicjalizowany.".SetColor(Color.green), this.GetType());
    }

    public void OnGridCellClickCall(int id)
    {
        OnGridCellClick.Invoke(id);

        GridManager gridManager = GridManager.Instance;
        PositiveCharactersManager positiveCharacters = PositiveCharactersManager.Instance;

        if(positiveCharacters != null && gridManager != null)
        {
            GridCell cell = gridManager.GetCellByID(id);
            positiveCharacters.SpawnCharacterInCell(cell);
        }
    }

    #endregion

    #region Handlers



    #endregion

}
