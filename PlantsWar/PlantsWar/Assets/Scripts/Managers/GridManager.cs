using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class GridManager : ManagerSingletonBase<GridManager>, ISaveable
{
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

    [Space, Header ("Highlights Settings")]
    [SerializeField]
    private float blastHighlightDuration;

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

    public float BlastHighlightDuration { 
        get => blastHighlightDuration; 
        private set => blastHighlightDuration = value; 
    }

    #endregion
    #region Methods

    public GridCell GetCellByID (int id) {

        if(id == -1)
        {
            return null;
        }

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
            output.Add (Grid[Grid.GetLength (0) - 1, i].SpawnPosition.position);
        }

        return output;
    }

    public Vector3 GetRandomSpawnPosition()
    {
        int index = UnityEngine.Random.Range(0, SpawnPositions.Count -1);
        return SpawnPositions[index];
    }

    public Vector3 GetSpawnPositionByIndex(int index)
    {
        if(index < SpawnPositions.Count && index > 0)
        {
            return SpawnPositions[index];
        }

        return SpawnPositions.First();
    }

    public void FreeCellById(int id)
    {
        GridCell cell = GetCellByID(id);
        if(cell != null)
        {
            cell.IsEmpty = true;
        }
    }

    public List<int> GetAllCellsId()
    {
        List<int> output = new List<int>();

        for(int i = 0; i < Grid.GetLength(0); i++)
        {
            for(int x = 0; x < Grid.GetLength(1); x++)
            {
                output.Add(Grid[i, x].Id);
            }
        }

        return output;
    }

    public List<GridCell> GetNeighboursInCrossCellsById(int id)
    {
        List<GridCell> cells = new List<GridCell>();
        Vector2Int cellIndex = GetCellIndexById(id);

        if(cellIndex.x == -1 || cellIndex.y == -1)
        {
            return null;
        }

        GridCell upperCell = GetCellByindex(cellIndex.x, cellIndex.y - 1);
        GridCell bottomCell = GetCellByindex(cellIndex.x, cellIndex.y + 1);
        GridCell leftCell = GetCellByindex(cellIndex.x - 1, cellIndex.y);
        GridCell rightCell = GetCellByindex(cellIndex.x + 1, cellIndex.y);

        if (upperCell != null)
        {
            cells.Add(upperCell);
        }
        if (bottomCell != null)
        {
            cells.Add(bottomCell);
        }
        if (leftCell != null)
        {
            cells.Add(leftCell);
        }
        if (rightCell != null)
        {
            cells.Add(rightCell);
        }

        if(cells.Count == 0)
        {
            return null;
        }

        return cells;
    }

    public GridCell GetCellByindex(int x, int y)
    {
        bool isWidth = (x < Grid.GetLength(0)) && (x != -1);
        bool isHeight = (y < Grid.GetLength(1)) && (y != -1);

        if(isWidth == true && isHeight == true)
        {
            return Grid[x, y];
        }

        return null;
    }

    public Vector2Int GetCellIndexById(int id)
    {
        Vector2Int output = new Vector2Int(-1, -1);
        if (id == -1)
        {
            return output;
        }

        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int y = 0; y < Grid.GetLength(1); y++)
            {
                if (Grid[i, y].Id == id)
                {
                    output.x = i;
                    output.y = y;
                }
            }
        }

        return output;
    }

    public void HighlightCellById(int id)
    {
        GridCell cell = GetCellByID(id);
        if(cell != null)
        {
            cell.SetHighlight(BlastHighlightDuration);
        }
    }

    public void ResetFields()
    {
        List<int> ids = GetAllCellsId();

        foreach (int id in ids)
        {
            FreeCellById(id);
        }
    }

    public void Load()
    {
        //TODO
    }

    public void Save()
    {
        //TODO
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

    protected override void AttachEvents () 
    {
        base.AttachEvents();

        GridSelectorManager.Instance.OnGridCellClick += OnCellSelectedHandler;
        SaveLoadManager.Instance.OnResetGame += ResetFields;
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        GridSelectorManager.Instance.OnGridCellClick -= OnCellSelectedHandler;
        SaveLoadManager.Instance.OnResetGame -= ResetFields;
    }

    private void Update() 
    {
        float deltaInMs = Time.deltaTime * 1000f;
        if(Grid != null)
        {
            for(int i = 0; i < Grid.GetLength(0); i++)
            {
                for(int x = 0; x < grid.GetLength(1); x++)
                {
                    Grid[i, x].Refresh(deltaInMs);
                }
            }
        }
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