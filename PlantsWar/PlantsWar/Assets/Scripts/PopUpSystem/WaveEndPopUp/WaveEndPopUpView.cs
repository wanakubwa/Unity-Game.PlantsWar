using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;


public class WaveEndPopUpView : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private TextMeshProUGUI wavesNumberTMP;

    #endregion

    #region Propeties

    public TextMeshProUGUI WavesNumberTMP { 
        get => wavesNumberTMP; 
        private set => wavesNumberTMP = value; 
    }

    #endregion

    #region Methods

    public void SetWavesClearedNumber(int wavesCleared)
    {
        WavesNumberTMP.text = wavesCleared.ToString();
    }

    #endregion

    #region Handlers



    #endregion
}
