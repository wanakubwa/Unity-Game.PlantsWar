using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MapManager : ManagerSingletonBase<MapManager>
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

    private void OnEnable()
    {
        GameObject map = Instantiate(GameMap, GameMap.transform.position, Quaternion.identity);
        map.transform.SetParent(this.transform);

        // Pobranie kontrollera z aktualnie usatwionej mapy.
        MapController = map.GetComponent<MapController>();
        MapController.MapCanvas.worldCamera = Camera.main;

        Debug.LogFormat("Map Manager enabled.".SetColor(Color.green));
    }

    public Transform GetGridStartPosition()
    {
        return MapController.GridStartPosition;
    }

    #endregion
    #region Handlers

    #endregion
}
