using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class EndGameScreenController : MonoBehaviour
{
    #region Fields


    #endregion

    #region Propeties



    #endregion

    #region Methods

    public void ToggleView()
    {
        if(gameObject.activeInHierarchy == true)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public void ResetGame()
    {
        Debug.Log("Reset");
        ToggleView();
    }

    public void NextLvl()
    {

    }

    #endregion

    #region Handlers



    #endregion
}
