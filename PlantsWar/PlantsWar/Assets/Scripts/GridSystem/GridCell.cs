using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    #region Fields

    [SerializeField]
    BoxCollider2D childCollider;

    [SerializeField]
    Transform spawnPosition;

    private int id = 0;
    private bool isEmpty = true;

    #endregion

    #region Propeties

    public int Id {
        get => id;
        set => id = value;
    }

    public BoxCollider2D ChildCollider
    {
        get => childCollider;
        set => childCollider = value;
    }

    public bool IsEmpty { 
        get => isEmpty; 
        set => isEmpty = value; 
    }

    public Transform SpawnPosition { 
        get => spawnPosition; 
        set => spawnPosition = value; 
    }

    #endregion

    #region Methods

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
        }
    }

    void CastRay()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider == ChildCollider)
            {
                // Wywolanie eventu zwiazanego z kliknieciem elementu siatki.
                GridSelectorManager.Instance.OnGridCellClickCall(Id);
            }

        }
    }

    #endregion

    #region Handlers



    #endregion
}
