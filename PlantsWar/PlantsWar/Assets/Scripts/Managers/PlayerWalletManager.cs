using System;
using UnityEngine;

public class PlayerWalletManager : ManagerSingletonBase<PlayerWalletManager>, ISaveable
{
    #region Fields
    [SerializeField]
    private int money;
    [SerializeField]
    private int startMoney;

    #endregion

    #region Propeties

    public int Money {
        get => money;
        private set => money = value;
    }

    public int StartMoney { 
        get => startMoney; 
        private set => startMoney = value; 
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

    public void ResetFields()
    {
        Money = StartMoney;
        OnMoneyChangeCall();
    }

    public void Load()
    {
        throw new NotImplementedException();
    }

    public void Save()
    {
        throw new NotImplementedException();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        Money = StartMoney;
        Debug.LogFormat("[{0}] Zainicjalizowany.".SetColor(Color.green), this.GetType());
    }

    protected override void BrodcastEvents()
    {
        base.BrodcastEvents();

        OnMoneyChangeCall();
    }

    protected override void AttachEvents()
    {
        base.AttachEvents();

        SaveLoadManager.Instance.OnResetGame += ResetFields;
    }

    protected override void DettachEvents()
    {
        base.DettachEvents();

        SaveLoadManager.Instance.OnResetGame -= ResetFields;
    }

    #endregion

    #region Handlers


    #endregion

    #region Enums



    #endregion
}