using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CheatsManager : ManagerSingletonBase<CheatsManager>
{
    #region Fields

    [SerializeField]
    private Button saveData;
    [SerializeField]
    private Button loadData;
    [SerializeField]
    private Slider ambientVolumeSlider;

    #endregion

    #region Propeties

    public Button SaveData {
        get => saveData;
        private set => saveData = value;
    }

    public Button LoadData {
        get => loadData;
        private set => loadData = value;
    }

    public Slider AmbientVolumeSlider { 
        get => ambientVolumeSlider; 
        private set => ambientVolumeSlider = value; 
    }

    #endregion

    #region Methods

    protected override void OnEnable()
    {
        base.OnEnable();

        AmbientVolumeSlider.value = AmbientVolumeSlider.maxValue * 0.5f;
    }

    public void OnSaveDataButton()
    {
        SaveLoadManager.Instance.CallSaveGame();
    }

    public void OnLoadDataButton()
    {
        SaveLoadManager.Instance.CallLoadGame();
    }

    public void OnMainMenuButton()
    {
        GameManager.Instance.LoadMenuScene();
    }

    public void OnAmbientVolumeChanged()
    {
        AudioManager.Instance.SetAmbientVolume(AmbientVolumeSlider.value);
    }

    #endregion

    #region Handlers



    #endregion
}
