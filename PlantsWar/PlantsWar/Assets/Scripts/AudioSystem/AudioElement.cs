using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AudioElement : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private AudioSource audio;

    #endregion

    #region Propeties

    public AudioSource Audio { 
        get => audio; 
        private set => audio = value; 
    }

    #endregion

    #region Methods



    #endregion

    #region Handlers



    #endregion
}
