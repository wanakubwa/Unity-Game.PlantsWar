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
        PlayerWalletManagerMemento memento = SaveLoadManager.Instance.LoadManagerClass(this) as PlayerWalletManagerMemento;
        if(memento != null)
        {
            SetMoney(memento.Money);
        }
    }

    public void Save()
    {
        SaveLoadManager.Instance.SaveManagerClass(this);
    }

    public void SetMoney(int value)
    {
        Money = value;
        OnMoneyChangeCall();
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
        SaveLoadManager.Instance.OnLoadGame += Load;
        SaveLoadManager.Instance.OnSaveGame += Save;
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        SaveLoadManager.Instance.OnResetGame -= ResetFields;
        SaveLoadManager.Instance.OnLoadGame -= Load;
        SaveLoadManager.Instance.OnSaveGame -= Save;
    }

    #endregion

    #region Handlers


    #endregion

    #region Enums



    #endregion
}