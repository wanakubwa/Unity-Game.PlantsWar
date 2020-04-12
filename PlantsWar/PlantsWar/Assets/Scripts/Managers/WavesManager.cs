using UnityEngine;
using System.Collections.Generic;
using OdinSerializer;
using System;

public class WavesManager : ManagerSingletonBase<WavesManager>, ISaveable
{
    #region Fields

    [Space]
    [SerializeField]
    private int enemiesInRow;
    [SerializeField]
    private int rowsNum;
    [SerializeField] 
    private float firstWaveDelay;
    [SerializeField]
    private float spawnCharactersDelay;
    [SerializeField]
    private float rowsDelay;

    [Space]
    [SerializeField]
    private int wavesLimit;

    [Space]
    [SerializeField]
    private List<WaveElement> wavesCollection = new List<WaveElement>();

    #endregion

    #region Propeties

    public event Action OnWavesCounterChanged = delegate {};
    public event Action OnRowsCounterChanged = delegate {};
    public event Action OnCharactersInRowChanged = delegate {};
    public event Action OnEndWave = delegate {};

    public int EnemiesInRow { 
        get => enemiesInRow; 
        private set => enemiesInRow = value; 
    }

    public int RowsNum { 
        get => rowsNum; 
        private set => rowsNum = value; 
    }

    public float FirstWaveDelay { 
        get => firstWaveDelay; 
        private set => firstWaveDelay = value; 
    }

    public float SpawnCharactersDelay { 
        get => spawnCharactersDelay; 
        private set => spawnCharactersDelay = value; 
    }

    public float RowsDelay { 
        get => rowsDelay; 
        private set => rowsDelay = value; 
    }

    public float StartDelayCounter{
        get;
        private set;
    }

    public float RowDelayCounter{
        get;
        private set;
    }

    public float SpawnCharacterDelayCounter {
        get;
        private set;
    }

    public int RowsCounter{
        get;
        private set;
    }

    public int CharactersInRowCounter{
        get;
        private set;
    }

    public int WavesCounter{
        get;
        private set;
    }

    public bool IsGameFreezed {
        get;
        private set;
    }

    public int WavesLimit { 
        get => wavesLimit; 
        private set => wavesLimit = value; 
    }

    public bool IsWaitingForWaveRequest {
        get;
        private set;

    }

    public List<WaveElement> WavesCollection { 
        get => wavesCollection; 
        private set => wavesCollection = value; 
    }

    #endregion

    #region Methods

    public void ResetFields()
    {
        CharactersInRowCounter = 0;
        RowDelayCounter = 0f;
        SetWavesCounter(0);
        SetRowsCounter(0);

        SpawnCharacterDelayCounter = 0f;
        StartDelayCounter = 0f;

        IsWaitingForWaveRequest = false;
    }

    public void Load()
    {
        WavesManagerMemento memento =  SaveLoadManager.Instance.LoadManagerClass(this) as WavesManagerMemento;
        if(memento != null)
        {
            SetCharactersInRowCounter(memento.CharactersInRowCounter);
            SetWavesCounter(memento.WavesCounter);
            SetRowsCounter(memento.RowsCounter);

            ResetDelayCounters();
        }
    }

    public void Save()
    {
        SaveLoadManager.Instance.SaveManagerClass(this);
    }
    
    public void SetWavesCounter(int value)
    {
        WavesCounter = value;
        RefreshRowsNumber();

        OnWavesCounterChanged.Invoke();
    }

    public void SetRowsCounter(int value)
    {
        RowsCounter = value;
        RefreshCharactersInRowNumber();

        OnRowsCounterChanged.Invoke();
    }

    public void SetCharactersInRowCounter(int value)
    {
        CharactersInRowCounter = value;
        OnCharactersInRowChanged.Invoke();
    }

    public void OnEndWaveNotify()
    {
        OnEndWave.Invoke();
    }

    public void RequestForNewWave()
    {
        IsWaitingForWaveRequest = false;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        WavesLimit = WavesCollection.Count;
        SetWavesCounter(0);
        SetRowsCounter(0);

        StartDelayCounter = 0f;
        RowDelayCounter = 0f;
        SpawnCharacterDelayCounter = 0f;
        CharactersInRowCounter = 0;

        IsWaitingForWaveRequest = false;
    }

    protected override void AttachEvents()
    {
        base.AttachEvents();

        GameEventsManager.Instance.OnGameFreez += OnGameFreezHandler;
        GameplayManager.Instance.OnWaveClear += OnWaveClearHandler;

        SaveLoadManager.Instance.OnResetGame += ResetFields;
        SaveLoadManager.Instance.OnLoadGame += Load;
        SaveLoadManager.Instance.OnSaveGame += Save;
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        GameEventsManager.Instance.OnGameFreez -= OnGameFreezHandler;
        GameplayManager.Instance.OnWaveClear -= OnWaveClearHandler;

        SaveLoadManager.Instance.OnResetGame -= ResetFields;
        SaveLoadManager.Instance.OnLoadGame -= Load;
        SaveLoadManager.Instance.OnSaveGame -= Save;
    }

    private void RefreshRowsNumber()
    {
        if(WavesCollection.Count > WavesCounter)
        {
            RowsNum = WavesCollection[WavesCounter].RowsCollection.Count;
        }
    }

    private void RefreshCharactersInRowNumber()
    {
        if(WavesCollection.Count <= WavesCounter)
        {
            return;
        }

        if(WavesCollection[WavesCounter].RowsCollection.Count > RowsCounter)
        {
            EnemiesInRow = WavesCollection[WavesCounter].RowsCollection[RowsCounter].EnemiesCollection.Count;
        }
    }

    private void Update()
    {
        if (IsGameFreezed == false)
        {
            if (StartDelayCounter >= FirstWaveDelay)
            {
                if(IsWaitingForWaveRequest == false)
                {
                    SpawnEnemiesInWave(Time.deltaTime * 1000);
                }
            }
            else
            {
                StartDelayCounter += (Time.deltaTime * 1000);
            }
        }
    }

    // TODO:
    private void SpawnEnemiesInWave(float time)
    {
        if(WavesCounter == WavesLimit)
        {
            return;
        }

        if(RowsCounter < RowsNum)
        {
            if(RowDelayCounter > RowsDelay)
            {
                if(SpawnCharacterDelayCounter > SpawnCharactersDelay)
                {
                    if(CharactersInRowCounter < EnemiesInRow)
                    {
                        WaveElement wave = WavesCollection[WavesCounter];
                        RowElement row = wave.RowsCollection[RowsCounter];
                        EnemyElement enemyToSpawn = row.EnemiesCollection[CharactersInRowCounter];

                        Vector3 position = GridManager.Instance.GetSpawnPositionByIndex(enemyToSpawn.SpawnIndex);
                        EnemyManager.Instance.SpawnCharacterOfTypeAtPosition(enemyToSpawn.CharacterType, position);

                        SpawnCharacterDelayCounter = 0f;
                        SetCharactersInRowCounter(CharactersInRowCounter + 1);
                    }
                    else
                    {
                        RowDelayCounter = 0f;
                        SetRowsCounter(RowsCounter + 1);
                        SetCharactersInRowCounter(0);
                    }
                }
                else
                {
                    SpawnCharacterDelayCounter += time;
                }
            }
            else
            {
                RowDelayCounter += time;
            }
        }
        else
        {
            SetWavesCounter(WavesCounter + 1);
            SetRowsCounter(0);
            IsWaitingForWaveRequest = true;
        }
    }

    private void ResetDelayCounters()
    {
        SpawnCharacterDelayCounter = 0f;
        RowDelayCounter = 0f;
        StartDelayCounter = 0f;
    }

    #endregion

    #region Handlers

    private void OnGameFreezHandler(bool isFreezed)
    {
        IsGameFreezed = isFreezed;
    }

    private void OnWaveClearHandler()
    {
        RequestForNewWave();
    }

    #endregion

    [Serializable]
    public class WaveElement
    {
        #region Fields
        
        [SerializeField]
        private List<RowElement> rowsCollection = new List<RowElement>();

        #endregion

        #region Propeties

        public List<RowElement> RowsCollection { 
            get => rowsCollection; 
            private set => rowsCollection = value; 
        }

        #endregion

        #region Methods



        #endregion

        #region Handlers



        #endregion

        #region Enums



        #endregion
    }

    [Serializable]
    public class RowElement
    {
        #region Fields
        
        [SerializeField]
        private List<EnemyElement> enemiesCollection = new List<EnemyElement>();

        #endregion

        #region Propeties

        public List<EnemyElement> EnemiesCollection { 
            get => enemiesCollection; 
            private set => enemiesCollection = value; 
        }

        #endregion

        #region Methods



        #endregion

        #region Handlers



        #endregion

        #region Enums



        #endregion
    }

    [Serializable]
    public class EnemyElement
    {
        #region Fields
        
        [SerializeField]
        private CharacterType characterType;

        [SerializeField]
        private int spawnIndex;

        #endregion

        #region Propeties

        public CharacterType CharacterType { 
            get => characterType; 
            private set => characterType = value; 
        }

        public int SpawnIndex { 
            get => spawnIndex; 
            private set => spawnIndex = value; 
        }

        #endregion

        #region Methods



        #endregion

        #region Handlers



        #endregion

        #region Enums



        #endregion
    }
}