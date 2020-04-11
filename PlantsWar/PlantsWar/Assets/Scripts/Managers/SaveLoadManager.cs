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

        if (memento != null)
        {
            Debug.LogFormat("[{0}] Was Loaded succesfully!".SetColor(Color.green), classType);
            return memento;
        }

        Debug.LogFormat("[{0}] Was NOT Loaded succesfully!".SetColor(Color.red), classType);
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

        // TODO: wyjatek przy zapisie.
        Debug.LogFormat("[{0}] Was Saved succesfully!".SetColor(Color.green), classType);
    }

    // ######################### SAVE SECTION ##################################

    private void SaveEnemyManager(EnemyManager manager)
    {
        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(Application.dataPath + Path.DirectorySeparatorChar + manager.FileName);

        //EnemyManagerMemento managerMemento = new EnemyManagerMemento();
        //managerMemento.EnemyCharactersSpawned = manager.EnemyCharactersSpawned;

        //bf.Serialize(file, managerMemento);
        //file.Close();

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



    // ############################ LOAD SECTION ###################################

    private object GetSavedEnemyManagerMemento(EnemyManager manager)
    {
        EnemyManagerMemento managerMemento = null;

        //if (File.Exists(Application.persistentDataPath + Path.DirectorySeparatorChar + manager.FileName))
        //{
        //    BinaryFormatter bf = new BinaryFormatter();
        //    FileStream file = File.Open(Application.dataPath + Path.DirectorySeparatorChar + manager.FileName, FileMode.Open);

        //    managerMemento = (EnemyManagerMemento)bf.Deserialize(file);
        //    file.Close();
        //}

        //if (File.Exists(Application.dataPath + "/" + manager.FileName))
        //{
        //    DataFormat dataFormat = DataFormat.Binary;
        //    string savePath = Application.dataPath + "/" + manager.FileName;

        //    var bytes = File.ReadAllBytes(savePath);
        //    managerMemento = SerializationUtility.DeserializeValue<EnemyManagerMemento>(bytes, dataFormat);
        //}

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

    #endregion

    #region Handlers



    #endregion
}
