using UnityEngine;
using System.Collections.Generic;
using OdinSerializer;
using System;

public class WavesManager : ManagerSingletonBase<WavesManager>, ISaveable
{
    #region Fields

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

    #endregion

    #region Propeties

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

    #endregion

    #region Methods

    public void ResetFields()
    {
        CharactersInRowCounter = 0;
        RowDelayCounter = 0f;
        RowsCounter = 0;
        SpawnCharacterDelayCounter = 0f;
        StartDelayCounter = 0f;
        WavesCounter = 0;
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
        StartDelayCounter = 0f;
        RowDelayCounter = 0f;
        SpawnCharacterDelayCounter = 0f;

        RowsCounter = 0;
        CharactersInRowCounter = 0;
        WavesCounter = 0;
    }

    private void Update()
    {
        if(StartDelayCounter >= FirstWaveDelay)
        {
            SpawnEnemiesInWave(Time.deltaTime * 1000);
        }
        else
        {
            StartDelayCounter += (Time.deltaTime * 1000);
        }
    }

    // TODO:
    private void SpawnEnemiesInWave(float time)
    {
        if(WavesCounter == 1)
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
                        Vector3 position = GridManager.Instance.GetRandomSpawnPosition();
                        CharacterType characterType = EnemyManager.Instance.GetRandomCharacterType();
                        EnemyManager.Instance.SpawnCharacterOfTypeAtPosition(characterType, position);

                        SpawnCharacterDelayCounter = 0f;
                        CharactersInRowCounter++;

                        Debug.Log("#### Enemie ####".SetColor(Color.cyan));
                    }
                    else
                    {
                        RowsCounter++;
                        RowDelayCounter = 0f;
                        CharactersInRowCounter = 0;

                        Debug.Log("#### Row ####".SetColor(Color.green));
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
            WavesCounter++;
        }
    }

    #endregion

    #region Handlers



    #endregion
}