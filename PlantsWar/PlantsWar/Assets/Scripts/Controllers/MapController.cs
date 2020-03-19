using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private Image background;

    [SerializeField]
    private Transform gridStartPosition;

    [SerializeField]
    private string name = string.Empty;

    [SerializeField]
    private Canvas mapCanvas;

    #endregion
    #region Propeties

    public Image Background {
        get => background;
        private set => background = value;
    }

    public Transform GridStartPosition {
        get => gridStartPosition;
        private set => gridStartPosition = value;
    }

    public string Name
    {
        get => name; 
        set => name = value;
    }

    public Canvas MapCanvas
    {
        get => mapCanvas;
        set => mapCanvas = value;
    }

    #endregion
    #region Methods

    #endregion
    #region Handlers

    #endregion



}
