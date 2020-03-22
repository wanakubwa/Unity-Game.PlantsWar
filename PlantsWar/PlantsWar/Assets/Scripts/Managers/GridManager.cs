using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class GridManager : ManagerSingletonBase<GridManager>
{
    #region Fields

    [Header("Single cell settings")]
    [SerializeField]
    private float cellWidth = 0f;
    [SerializeField]
    private float cellHeight = 0f;
    [SerializeField]
    private GameObject cellPrefab;

    [Space, Header("Grid plane size settings")]
    [SerializeField]
    private int gridWithCells;
    [SerializeField]
    private int gridHeightCells;

    private Transform gridStartPosition;
    private GridCell[,] grid;

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

    /// <summary>
    /// Siatka przechowywana jest jako tablica X*Y gdzie x to szerokośc od lewej do prawej, a wyskosc to
    /// wartosc komorek liczona od dolu do gory mapy.
    /// </summary>
    public GridCell[,] Grid
    {
        get => grid;
        private set => grid = value;
    }

    #endregion
    #region Methods

    protected override void OnEnable()
    {
        base.OnEnable();

        SetGridStartPostion();

        SetGridCellSize();
        CreateGridOnMap();

        Debug.LogFormat("[{0}] Zainicjalizowany.".SetColor(Color.green), this.GetType());
    }

    protected override void AttachEvents()
    {
        Debug.Log("Attached");
    }

    private void SetGridCellSize()
    {
        int childs = SingleCell.transform.childCount;
        if(childs == 0)
        {
            Debug.LogErrorFormat("Can't find childs for cell object!");
            return;
        }

        BoxCollider2D cellCollider = SingleCell.transform.GetChild(0).GetComponent<BoxCollider2D>();
        Vector2 cellSize = cellCollider.size;

        CellWidth = cellSize.x;
        CellHeight = cellSize.y;
    }

    private void SetGridStartPostion()
    {
        MapManager mapManager = MapManager.Instance;
        if(mapManager != null)
        {
            GridStartPosition = mapManager.GetGridStartPosition();
        }
        else
        {
            Debug.LogErrorFormat("Map manager was null! {0}", this.GetType());
        }
    }

    private void CreateGridOnMap()
    {
        Grid = new GridCell[gridWithCells, gridHeightCells];
        int id = 0;

        for (int i = 0; i < GridHeightCells; i++)
        {
            for (int y = 0; y < GridWithCells; y++)
            {
                // Uwtorzenie pozycji nowej komorki siatki zmodyfikowanej o pozycje w tablicy.
                // W poziomie nalzy dodawac aby przesunac w pionie idziemy w dol wiec odejmujemy.
                Vector3 position = new Vector3(GridStartPosition.position.x + y * CellWidth, GridStartPosition.position.y - i * CellHeight, GridStartPosition.position.z);

                GameObject cellObject = Instantiate(SingleCell, position, Quaternion.identity);
                cellObject.transform.SetParent(this.transform);

                GridCell singleCell = cellObject.GetComponent<GridCell>();
                singleCell.Id = id;

                Grid[y, i] = singleCell;
                id++;
            }
        }
    }

    #endregion
    #region Handlers

    #endregion
}
