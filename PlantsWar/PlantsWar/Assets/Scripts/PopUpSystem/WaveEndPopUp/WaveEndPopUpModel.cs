using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WaveEndPopUpModel : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private WaveEndPopUpController controller;
    [SerializeField]
    private float timeDelayMs = 4000f;

    #endregion

    #region Propeties

    public WaveEndPopUpController Controller { 
        get => controller; 
        private set => controller = value; 
    }

    #endregion

    #region Methods

    public void StartAutoClose()
    {
        StartCoroutine(WaitAndClose(timeDelayMs * 0.001f));
    }

    private IEnumerator WaitAndClose(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Controller.ClosePopUp();
    }

    #endregion

    #region Handlers



    #endregion
}
