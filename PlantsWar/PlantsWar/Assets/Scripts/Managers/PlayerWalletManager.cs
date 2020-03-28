using System;
using UnityEngine;

public class PlayerWalletManager : ManagerSingletonBase<PlayerWalletManager> 
{
    #region Fields

    [SerializeField]
    private int money;

    #endregion

    #region Propeties

    public int Money {
        get => money;
        private set => money = value;
    }
    
    public event Action<int> OnMoneyChange = delegate{};

    #endregion

    #region Methods

    public bool TryAddMoney(int value)
    {
        bool isSuccess = true;

        if (value > 0)
        {
            Money += value;
        }
        else
        {
            if (Money + value < 0)
            {
                isSuccess = false;
            }
            else
            {
                Money += value;
            }
        }

        OnMoneyChangeCall();
        return isSuccess;
    }

    public void OnMoneyChangeCall()
    {
        OnMoneyChange.Invoke(Money);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        Debug.LogFormat("[{0}] Zainicjalizowany.".SetColor(Color.green), this.GetType());
    }

    protected override void BrodcastEvents()
    {
        base.BrodcastEvents();

        OnMoneyChangeCall();
    }

    #endregion
    
    #region Handlers
    
    
    #endregion
    
    #region Enums
    
    
    
    #endregion
}