using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PopUpManager : ManagerSingletonBase<PopUpManager>
{
    #region Fields

    [Space(10)]
    [SerializeField]
    private GameObject waveEndPopUp;

    #endregion

    #region Propeties


    public GameObject WaveEndPopUp { 
        get => waveEndPopUp; 
        private set => waveEndPopUp = value; 
    }

    public GameObject MediumPriorityPopUp {
        get;
        private set;
    }

    #endregion

    #region Methods

    public void RequestWaveEndPopUp()
    {
        Instantiate(WaveEndPopUp);
    }

    public void RequestUpgradePopUp()
    {
        if(MediumPriorityPopUp == null)
        {
            //TODO;
        }
    }

    protected override void AttachEvents()
    {
        base.AttachEvents();

        GameplayManager.Instance.OnWaveClear += RequestWaveEndPopUp;
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        GameplayManager.Instance.OnWaveClear -= RequestWaveEndPopUp;
    }

    #endregion

    #region Handlers

    #endregion
}
