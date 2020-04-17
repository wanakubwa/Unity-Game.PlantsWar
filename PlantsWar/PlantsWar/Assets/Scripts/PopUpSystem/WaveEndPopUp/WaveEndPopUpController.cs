using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WaveEndPopUpController : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private WaveEndPopUpView view;
    [SerializeField]
    private WaveEndPopUpModel model;

    [SerializeField]
    private Canvas canvas;

    #endregion

    #region Propeties

    public WaveEndPopUpView View { 
        get => view; 
        private set => view = value; 
    }

    public Canvas Canvas { 
        get => canvas; 
        private set => canvas = value; 
    }

    public WaveEndPopUpModel Model { 
        get => model; 
        private set => model = value; 
    }

    #endregion

    #region Methods

    public void ClosePopUp()
    {
        Destroy(gameObject);
    }

    public void Initialize()
    {
        int wavesNumber = WavesManager.Instance.WavesCounter;
        View.SetWavesClearedNumber(wavesNumber);

        // Liczenie czasu do automatycznego zamkniecia.
        Model.StartAutoClose();
    }

    public void SetCanvasCamera(Camera camera)
    {
        Canvas.worldCamera = camera;
    }

    #endregion

    #region Handlers

    #endregion
}
