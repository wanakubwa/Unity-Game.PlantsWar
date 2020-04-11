using System;
using UnityEngine;

public class GameplayManager : ManagerSingletonBase<GameplayManager>, ISaveable
{
    #region Fields
    
    [Space]
    [SerializeField]
    private EndPoint rightEndPoint;
    [SerializeField]
    private EndPoint leftEndPoint;
    [SerializeField]
    private int enemiesLimit;

    #endregion

    #region Propeties

    public event Action<int> OnEnemiesLimitCounterChange = delegate{};
    public event Action OnGameOver = delegate{};
    public event Action OnGameWin = delegate{};
    public event Action OnWaveClear = delegate {};

    public EndPoint RightEndPoint { 
        get => rightEndPoint; 
        private set => rightEndPoint = value; 
    }

    public EndPoint LeftEndPoint { 
        get => leftEndPoint; 
        private set => leftEndPoint = value; 
    }

    public int EnemiesLimit { 
        get => enemiesLimit; 
        private set => enemiesLimit = value; 
    }

    public int EnemiesLimitCounter {
        get;
        private set;
    }

    #endregion

    #region Methods

    public void ResetFields()
    {
        EnemiesLimitCounter = 0;
    }

    public void Load()
    {
        GameplayManagerMemento memento = SaveLoadManager.Instance.LoadManagerClass(this) as GameplayManagerMemento;
        if(memento != null)
        {
            SetEnemiesLimitCounter(memento.EnemiesLimitCounter);
        }
    }

    public void Save()
    {
        SaveLoadManager.Instance.SaveManagerClass(this);
    }

    // public void CallGameFreez(bool isFreezed)
    // {
    //     OnGameFreez.Invoke(isFreezed);
    // }

    public void SetEnemiesLimitCounter(int value)
    {
        EnemiesLimitCounter = value;
        OnEnemiesLimitCounterChange.Invoke(EnemiesLimitCounter);
    }

    protected override void AttachEvents()
    {
        base.AttachEvents();

        RightEndPoint.OnTrigger += OnRightTrigger;
        LeftEndPoint.OnTrigger += OnLeftTrigger;
        OnEnemiesLimitCounterChange += EnemiesLimitCounterHandler;

        SaveLoadManager.Instance.OnResetGame += ResetFields;
        SaveLoadManager.Instance.OnSaveGame += Save;
        SaveLoadManager.Instance.OnLoadGame += Load;

        GameplayManager.Instance.OnWaveClear += OnWaveClearHandler;
        
        //TODO naprawic to.
        EnemyManager.Instance.OnSpawnedEnemiesChanged += OnEnemiesSpawnedChangedHandler;
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        rightEndPoint.OnTrigger -= OnRightTrigger;
        leftEndPoint.OnTrigger -= OnLeftTrigger;
        OnEnemiesLimitCounterChange -= EnemiesLimitCounterHandler;

        SaveLoadManager.Instance.OnResetGame -= ResetFields;
        SaveLoadManager.Instance.OnSaveGame -= Save;
        SaveLoadManager.Instance.OnLoadGame -= Load;

        GameplayManager.Instance.OnWaveClear -= OnWaveClearHandler;

        EnemyManager.Instance.OnSpawnedEnemiesChanged -= OnEnemiesSpawnedChangedHandler;
    }

    #endregion

    #region Handlers

    private void OnRightTrigger(Collider2D obj)
    {
        CharacterBase character = obj.gameObject.GetComponent<CharacterBase>();
        if(character != null)
        {
            PositiveCharactersManager.Instance?.KillSpawnedCharacter(character);
        }
        else
        {
            Destroy(obj.gameObject);
        }
    }

    private void OnLeftTrigger(Collider2D obj)
    {
        CharacterBase character = obj.gameObject.GetComponent<CharacterBase>();
        if(character != null)
        {
            EnemiesLimitCounter++;
            EnemyManager.Instance?.KillSpawnedCharacter(character);
            OnEnemiesLimitCounterChange.Invoke(EnemiesLimitCounter);
        }
        else
        {
            Destroy(obj.gameObject);
        }
    }

    private void EnemiesLimitCounterHandler(int counter)
    {
        if(counter >= EnemiesLimit)
        {
            GameEventsManager.Instance.OnGameFreezNotify(true);
            OnGameOver.Invoke();
        }
    }

    private void OnEnemiesSpawnedChangedHandler()
    {
        if(EnemyManager.Instance.EnemyCharactersSpawned.Count == 0)
        {
            WavesManager wavesManager = WavesManager.Instance;
            if(wavesManager.WavesCounter == wavesManager.WavesLimit)
            {
                OnGameWin.Invoke();
            }
        }
        
        if (EnemyManager.Instance.EnemyCharactersSpawned.Count == 0)
        {
            if(WavesManager.Instance.IsWaitingForWaveRequest == true)
            {
                OnWaveClear.Invoke();
            }
        }
    }

    private void OnWaveClearHandler()
    {
        SaveLoadManager.Instance.CallSaveGame();
    }

    #endregion

    #region Enums

    #endregion
}