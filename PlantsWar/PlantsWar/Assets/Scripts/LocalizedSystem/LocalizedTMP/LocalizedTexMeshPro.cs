using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class LocalizedTexMeshPro : MonoBehaviour
{

    #region Fields

    [Space(10)]
    [Header("Klucz lokalizacyjny")]
    [SerializeField]
    public string localizedKey;

    #endregion

    #region Propeties

    public string LocalizedKey { 
        get => localizedKey; 
        private set => localizedKey = value; 
    }

    public TextMeshProUGUI TextMeshPro
    {
        get;
        private set;
    }

    #endregion

    #region Methods

    private void Awake()
    {
        TextMeshPro = GetComponent<TextMeshProUGUI>();
        TextMeshPro.text = FileContainerSetup.Instance.GetStringByLocalizedKey(LocalizedKey);
    }

    #endregion

    #region Handlers



    #endregion
}
