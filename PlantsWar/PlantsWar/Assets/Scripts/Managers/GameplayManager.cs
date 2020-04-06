using System;
using UnityEngine;

public class GameplayManager : ManagerSingletonBase<GameplayManager>
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

    public int EnemiesLimitCounter{
        get;
        private set;
    }

    #endregion

    #region Methods

    protected override void AttachEvents()
    {
        base.AttachEvents();

        rightEndPoint.OnTrigger += OnRightTrigger;
        leftEndPoint.OnTrigger += OnLeftTrigger;
        OnEnemiesLimitCounterChange += EnemiesLimitCounterHandler;
    }

    protected override void DettachEvents()
    {
        base.DettachEvents();

        rightEndPoint.OnTrigger -= OnRightTrigger;
        leftEndPoint.OnTrigger -= OnLeftTrigger;
        OnEnemiesLimitCounterChange -= EnemiesLimitCounterHandler;
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
            OnGameOver.Invoke();
        }
    }

    #endregion

    #region Enums



    #endregion
}