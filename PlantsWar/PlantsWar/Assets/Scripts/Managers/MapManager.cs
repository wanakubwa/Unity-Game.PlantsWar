using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MapManager : ManagerSingletonBase<MapManager>, IContentLoadable
{
    #region Fields
    [SerializeField]
    private GameObject gameMap;

    private MapController mapController;

    #endregion
    #region Propeties

    public GameObject GameMap
    {
        get => gameMap;
        set => gameMap = value;
    }

    public MapController MapController
    {
        get => mapController;
        private set => mapController = value;
    }

    #endregion
    #region Methods

    protected override void OnEnable()
    {
        base.OnEnable();

        Debug.LogFormat("[{0}] Zainicjalizowany.".SetColor(Color.green), this.GetType());
    }

    public Transform GetGridStartPosition()
    {
        return MapController.GridStartPosition;
    }

    public void LoadGameContent()
    {
        GameObject map = Instantiate(GameMap, GameMap.transform.position, Quaternion.identity);
        map.transform.SetParent(this.transform);

        // Pobranie kontrollera z aktualnie usatwionej mapy.
        MapController = map.GetComponent<MapController>();
        MapController.MapCanvas.worldCamera = Camera.main;
    }

    public void FreeGameContent()
    {
        if(MapController != null)
        {
            Destroy(MapController?.gameObject);
        }
    }

    #endregion
    #region Handlers

    #endregion
}
