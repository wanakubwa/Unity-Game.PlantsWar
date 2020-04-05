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

    [Space]
    [SerializeField]
    MeshRenderer meshRenderer;
    [SerializeField]
    Material transparentMaterial;
    [SerializeField]
    Material highlightMaterial;

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

    public MeshRenderer MeshRenderer { 
        get => meshRenderer; 
        private set => meshRenderer = value; 
    }

    public Material TransparentMaterial { 
        get => transparentMaterial; 
        private set => transparentMaterial = value; 
    }

    public Material HighlightMaterial { 
        get => highlightMaterial; 
        private set => highlightMaterial = value; 
    }

    public float HighlightDuration{
        get;
        private set;
    }

    public float HighlightDurationCounter{
        get;
        private set;
    }

    public bool IsHighlighted {
        get;
        private set;
    }

    #endregion

    #region Methods
    public void SetHighlight(float durationInMs)
    {
        HighlightDuration = durationInMs;
        IsHighlighted = true;

        Material[] actualMaterials = new Material[1];
        actualMaterials[0] = HighlightMaterial;
        MeshRenderer.materials = actualMaterials;
    }

    public void Refresh(float deltaTime)
    {
        // Sprawdzenie czy podswietlonie komorki.
        if(IsHighlighted == true)
        {
            if(HighlightDurationCounter >= HighlightDuration)
            {
                HighlightDurationCounter = 0f;
                IsHighlighted = false;
                SetDefaultMaterial();
            }

            HighlightDurationCounter += deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
        }
    }

    // private void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         CastRay();
    //     }
    // }

    private void CastRay()
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

    private void OnEnable() 
    {
        SetDefaultMaterial();
    }

    private void SetDefaultMaterial()
    {
        Material[] actualMaterials = new Material[1];
        actualMaterials[0] = TransparentMaterial;
        MeshRenderer.materials = actualMaterials;
    }

    #endregion

    #region Handlers



    #endregion
}
