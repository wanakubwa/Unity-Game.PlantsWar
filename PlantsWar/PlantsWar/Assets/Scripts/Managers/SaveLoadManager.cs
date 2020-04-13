using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using OdinSerializer;

public class SaveLoadManager : ManagerSingletonBase<SaveLoadManager>
{
    #region Fields



    #endregion

    #region Propeties

    public event Action OnResetGame = delegate { };
    public event Action OnLoadGame = delegate { };
    public event Action OnSaveGame = delegate { };

    #endregion

    #region Methods

    public void CallResetGame()
    {
        OnResetGame.Invoke();
    }

    public void CallLoadGame()
    {
        OnLoadGame.Invoke();
    }

    public void CallSaveGame()
    {
        OnSaveGame.Invoke();
    }

    public object LoadManagerClass(object manager)
    {
        Type classType = manager.GetType();

        object memento = null;

        if (classType == typeof(EnemyManager))
        {
            memento = GetSavedEnemyManagerMemento(manager as EnemyManager);
        }
        else if (classType == typeof(GameplayManager))
        {
            memento = GetSavedGameplayMemento(manager as GameplayManager);
        }
        else if (classType == typeof(PlayerWalletManager))
        {
            memento = GetSavedPlayerWalletManagerMemento(manager as PlayerWalletManager);
        }
        else if (classType == typeof(PositiveCharactersManager))
        {
            memento = GetSavedPositiveCharactersManagerMemento(manager as PositiveCharactersManager);
        }
        else if (classType == typeof(WavesManager))
        {
            memento = GetWavesManagerMemento(manager as WavesManager);
        }

        if (memento != null)
        {
            Debug.LogFormat("[{0}] Was Loaded successfully!".SetColor(Color.green), classType);
            return memento;
        }

        Debug.LogFormat("[{0}] Was NOT Loaded successfully!".SetColor(Color.red), classType);
        return memento;
    }

    public void SaveManagerClass(object manager)
    {
        Type classType = manager.GetType();

        if(classType == typeof(EnemyManager))
        {
            SaveEnemyManager(manager as EnemyManager);
        }
        else if (classType == typeof(GameplayManager))
        {
            SaveGameplayManager(manager as GameplayManager);
        }
        else if (classType == typeof(PlayerWalletManager))
        {
            SavePlayerWalletManager(manager as PlayerWalletManager);
        }
        else if (classType == typeof(PositiveCharactersManager))
        {
            SavePositiveCharactersManager(manager as PositiveCharactersManager);
        }
        else if (classType == typeof(WavesManager))
        {
            SaveWavesManager(manager as WavesManager);
        }

        // TODO: wyjatek przy zapisie.
        Debug.LogFormat("[{0}] Was Saved successfully!".SetColor(Color.green), classType);
    }

    // ######################### SAVE SECTION ##################################

    private void SaveEnemyManager(EnemyManager manager)
    {
        DataFormat dataFormat = DataFormat.Binary;
        string savePath = Application.dataPath + "/" + manager.FileName;

        EnemyManagerMemento managerMemento = new EnemyManagerMemento();
        managerMemento.EnemyCharactersSpawned = manager.EnemyCharactersSpawned;

        var bytes = SerializationUtility.SerializeValue(managerMemento, dataFormat);
        File.WriteAllBytes(savePath, bytes);
    }

    private void SaveGameplayManager(GameplayManager manager)
    {
        DataFormat dataFormat = DataFormat.Binary;
        string savePath = Application.dataPath + "/" + manager.FileName;

        GameplayManagerMemento managerMemento = new GameplayManagerMemento();
        managerMemento.EnemiesLimitCounter = manager.EnemiesLimitCounter;

        var bytes = SerializationUtility.SerializeValue(managerMemento, dataFormat);
        File.WriteAllBytes(savePath, bytes);
    }

    private void SavePlayerWalletManager(PlayerWalletManager manager)
    {
        DataFormat dataFormat = DataFormat.Binary;
        string savePath = Application.dataPath + "/" + manager.FileName;

        // Miejsce na uzupelnienie memento przed zapisem.
        PlayerWalletManagerMemento managerMemento = new PlayerWalletManagerMemento();
        managerMemento.Money= manager.Money;

        var bytes = SerializationUtility.SerializeValue(managerMemento, dataFormat);
        File.WriteAllBytes(savePath, bytes);
    }

    private void SavePositiveCharactersManager(PositiveCharactersManager manager)
    {
        DataFormat dataFormat = DataFormat.Binary;
        string savePath = Application.dataPath + "/" + manager.FileName;

        // Miejsce na uzupelnienie memento przed zapisem.
        PositiveCharactersManagerMemento managerMemento = new PositiveCharactersManagerMemento();
        managerMemento.CreateCharactersMementoCollection(manager.SpawnedCharacters);

        var bytes = SerializationUtility.SerializeValue(managerMemento, dataFormat);
        File.WriteAllBytes(savePath, bytes);
    }

    private void SaveWavesManager(WavesManager manager)
    {
        DataFormat dataFormat = DataFormat.Binary;
        string savePath = Application.dataPath + "/" + manager.FileName;

        // Miejsce na uzupelnienie memento przed zapisem.
        WavesManagerMemento managerMemento = new WavesManagerMemento();
        managerMemento.CharactersInRowCounter = manager.CharactersInRowCounter;
        managerMemento.RowDelayCounter = manager.RowDelayCounter;
        managerMemento.RowsCounter = manager.RowsCounter;
        managerMemento.SpawnCharacterDelayCounter = manager.SpawnCharacterDelayCounter;
        managerMemento.StartDelayCounter = manager.StartDelayCounter;
        managerMemento.WavesCounter = manager.WavesCounter;

        var bytes = SerializationUtility.SerializeValue(managerMemento, dataFormat);
        File.WriteAllBytes(savePath, bytes);
    }


    // ############################ LOAD SECTION ###################################

    private object GetSavedEnemyManagerMemento(EnemyManager manager)
    {
        EnemyManagerMemento managerMemento = null;

        return managerMemento;
    }

    private object GetSavedGameplayMemento(GameplayManager manager)
    {
        GameplayManagerMemento managerMemento = null;

        if (File.Exists(Application.dataPath + "/" + manager.FileName))
        {
            DataFormat dataFormat = DataFormat.Binary;
            string savePath = Application.dataPath + "/" + manager.FileName;

            var bytes = File.ReadAllBytes(savePath);
            managerMemento = SerializationUtility.DeserializeValue<GameplayManagerMemento>(bytes, dataFormat);
        }

        return managerMemento;
    }

    private object GetSavedPlayerWalletManagerMemento(PlayerWalletManager manager)
    {
        PlayerWalletManagerMemento managerMemento = null;

        if (File.Exists(Application.dataPath + "/" + manager.FileName))
        {
            DataFormat dataFormat = DataFormat.Binary;
            string savePath = Application.dataPath + "/" + manager.FileName;

            var bytes = File.ReadAllBytes(savePath);
            managerMemento = SerializationUtility.DeserializeValue<PlayerWalletManagerMemento>(bytes, dataFormat);
        }

        return managerMemento;
    }

    private object GetSavedPositiveCharactersManagerMemento(PositiveCharactersManager manager)
    {
        PositiveCharactersManagerMemento managerMemento = null;

        if (File.Exists(Application.dataPath + "/" + manager.FileName))
        {
            DataFormat dataFormat = DataFormat.Binary;
            string savePath = Application.dataPath + "/" + manager.FileName;

            var bytes = File.ReadAllBytes(savePath);
            managerMemento = SerializationUtility.DeserializeValue<PositiveCharactersManagerMemento>(bytes, dataFormat);
        }

        return managerMemento;
    }

    private object GetWavesManagerMemento(WavesManager manager)
    {
        WavesManagerMemento managerMemento = null;

        if (File.Exists(Application.dataPath + "/" + manager.FileName))
        {
            DataFormat dataFormat = DataFormat.Binary;
            string savePath = Application.dataPath + "/" + manager.FileName;

            var bytes = File.ReadAllBytes(savePath);
            managerMemento = SerializationUtility.DeserializeValue<WavesManagerMemento>(bytes, dataFormat);
        }

        return managerMemento;
    }


    protected override void AttachEvents()
    {
        base.AttachEvents();
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();
    }

    #endregion

    #region Handlers



    #endregion
}
