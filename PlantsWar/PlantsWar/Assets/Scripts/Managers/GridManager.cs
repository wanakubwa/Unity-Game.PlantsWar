﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class GridManager : ManagerSingletonBase<GridManager> {
    #region Fields

    [Space]
    [SerializeField]
    private float spawnPointOffest;

    [Space]
    [Header ("Single cell settings")]
    [SerializeField]
    private float cellWidth = 0f;
    [SerializeField]
    private float cellHeight = 0f;
    [SerializeField]
    private GameObject cellPrefab;

    [Space, Header ("Grid plane size settings")]
    [SerializeField]
    private int gridWithCells;
    [SerializeField]
    private int gridHeightCells;

    private Transform gridStartPosition;
    private GridCell[, ] grid;
    private List<Vector3> spawnPositions = new List<Vector3> ();

    #endregion
    #region Propeties

    public GameObject SingleCell {
        get => cellPrefab;
        set => cellPrefab = value;
    }

    public float CellWidth {
        get => cellWidth;
        set => cellWidth = value;
    }

    public float CellHeight {
        get => cellHeight;
        set => cellHeight = value;
    }

    public int GridWithCells {
        get => gridWithCells;
        set => gridWithCells = value;
    }

    public int GridHeightCells {
        get => gridHeightCells;
        set => gridHeightCells = value;
    }

    public Transform GridStartPosition {
        get => gridStartPosition;
        set => gridStartPosition = value;
    }

    public List<Vector3> SpawnPositions {
        get => spawnPositions;
        private set => spawnPositions = value;
    }

    public float SpawnPointOffest {
        get => spawnPointOffest;
        private set => spawnPointOffest = value;
    }

    /// <summary>
    /// Siatka przechowywana jest jako tablica X*Y gdzie x to szerokośc od lewej do prawej, a wyskosc to
    /// wartosc komorek liczona od dolu do gory mapy.
    /// </summary>
    public GridCell[, ] Grid {
        get => grid;
        private set => grid = value;
    }

    #endregion
    #region Methods

    public GridCell GetCellByID (int id) {
        for (int i = 0; i < Grid.GetLength (0); i++) {
            for (int y = 0; y < Grid.GetLength (1); y++) {
                if (Grid[i, y].Id == id) {
                    return Grid[i, y];
                }
            }
        }

        return null;
    }

    public bool CanSpawnCharacterInCell (GridCell cell) {
        if (cell.IsEmpty == false) {
            return false;
        }

        return true;
    }

    public List<Vector3> GetLastCellsForEachRow () {
        List<Vector3> output = new List<Vector3> ();

        for (int i = 0; i < Grid.GetLength (1); i++) {
            output.Add (Grid[Grid.GetLength (0) - 1, i].transform.position);
        }

        return output;
    }

    public Vector3 GetRandomSpawnPosition()
    {
        int index = UnityEngine.Random.Range(0, SpawnPositions.Count -1);
        return SpawnPositions[index];
    }

    protected override void OnEnable () {
        base.OnEnable ();

        SetGridStartPostion ();

        SetGridCellSize ();
        CreateGridOnMap ();

        // Inicjalizacja pozycji spawnowania przeciwnikow.
        List<Vector3> lastCellsPosition = GetLastCellsForEachRow ();
        InitializeSpawnPositions (lastCellsPosition);

        Debug.LogFormat ("[{0}] Zainicjalizowany.".SetColor (Color.green), this.GetType ());
    }

    protected override void AttachEvents () {
        GridSelectorManager.Instance.OnGridCellClick += OnCellSelectedHandler;
    }

    private void SetGridCellSize () {
        int childs = SingleCell.transform.childCount;
        if (childs == 0) {
            Debug.LogErrorFormat ("Can't find childs for cell object!");
            return;
        }

        BoxCollider2D cellCollider = SingleCell.transform.GetChild (0).GetComponent<BoxCollider2D> ();
        Vector2 cellSize = cellCollider.size;

        CellWidth = cellSize.x;
        CellHeight = cellSize.y;
    }

    private void SetGridStartPostion () {
        MapManager mapManager = MapManager.Instance;
        if (mapManager != null) {
            GridStartPosition = mapManager.GetGridStartPosition ();
        } else {
            Debug.LogErrorFormat ("Map manager was null! {0}", this.GetType ());
        }
    }

    private void CreateGridOnMap () {
        Grid = new GridCell[gridWithCells, gridHeightCells];
        int id = 0;

        for (int i = 0; i < GridHeightCells; i++) {
            for (int y = 0; y < GridWithCells; y++) {
                // Uwtorzenie pozycji nowej komorki siatki zmodyfikowanej o pozycje w tablicy.
                // W poziomie nalzy dodawac aby przesunac w pionie idziemy w dol wiec odejmujemy.
                Vector3 position = new Vector3 (GridStartPosition.position.x + y * CellWidth, GridStartPosition.position.y - i * CellHeight, GridStartPosition.position.z);

                GameObject cellObject = Instantiate (SingleCell, position, Quaternion.identity);
                cellObject.transform.SetParent (this.transform);

                GridCell singleCell = cellObject.GetComponent<GridCell> ();
                singleCell.Id = id;

                Grid[y, i] = singleCell;
                id++;
            }
        }
    }

    private void InitializeSpawnPositions (List<Vector3> lastCellsPosition) 
    {
        for(int i = 0; i < lastCellsPosition.Count; i++)
        {
            SpawnPositions.Add(new Vector3(lastCellsPosition[i].x + spawnPointOffest, lastCellsPosition[i].y));
        }
    }

    #endregion
    #region Handlers

    private void OnCellSelectedHandler (int id) {
        GridCell selectedCell = GetCellByID (id);

        Debug.LogFormat ("Transfrom: {0}, For cell id: {1}.", selectedCell.transform, selectedCell.Id);
    }

    #endregion
}