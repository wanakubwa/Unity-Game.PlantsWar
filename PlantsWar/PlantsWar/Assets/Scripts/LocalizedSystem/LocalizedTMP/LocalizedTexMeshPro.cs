using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class LocalizedTexMeshPro : TextMeshProUGUI
{

    #region Fields

    [Space(10)]
    [SerializeField]
    private string localizedKey;

    #endregion

    #region Propeties

    public string LocalizedKey { 
        get => localizedKey; 
        private set => localizedKey = value; 
    }

    #endregion

    #region Methods

    protected override void Awake()
    {
        base.Awake();

        string localizedText = FileContainerSetup.Instance.GetStringByLocalizedKey(LocalizedKey);
        text = localizedText;
    }

    #endregion

    #region Handlers



    #endregion
}
