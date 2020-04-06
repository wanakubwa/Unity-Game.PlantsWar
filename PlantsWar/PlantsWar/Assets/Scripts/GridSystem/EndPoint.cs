using System;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    #region Fields
    
    
    
    #endregion
    
    #region Propeties
    
    public event Action<Collider2D> OnTrigger = delegate{};
    
    #endregion
    
    #region Methods
    
    
    
    #endregion
    
    #region Handlers
    
    private void OnTriggerEnter2D(Collider2D obj) 
    {
        OnTrigger.Invoke(obj);
    }
    
    #endregion
    
    #region Enums
    
    
    
    #endregion
}
