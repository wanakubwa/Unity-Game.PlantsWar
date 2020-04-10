using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
            memento = GetEnemyManagerMemento(manager as EnemyManager);
        }

        if(memento != null)
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

        Debug.LogFormat("[{0}] Was Saved succesfully!".SetColor(Color.blue), classType);
    }

    private void SaveEnemyManager(EnemyManager manager)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + Path.DirectorySeparatorChar + manager.FileName);

        EnemyManagerMemento managerMemento = new EnemyManagerMemento();
        managerMemento.EnemyCharactersSpawned = manager.EnemyCharactersSpawned;

        bf.Serialize(file, managerMemento);
        file.Close();
    }

    private object GetEnemyManagerMemento(EnemyManager manager)
    {
        EnemyManagerMemento managerMemento = null;

        if (File.Exists(Application.persistentDataPath + Path.DirectorySeparatorChar + manager.FileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + Path.DirectorySeparatorChar + manager.FileName, FileMode.Open);

            managerMemento = (EnemyManagerMemento)bf.Deserialize(file);
            file.Close();
        }

        return managerMemento;
    }

    #endregion

    #region Handlers



    #endregion
}
