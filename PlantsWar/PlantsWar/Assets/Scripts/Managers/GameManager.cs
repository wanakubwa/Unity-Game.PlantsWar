using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : ManagerSingletonBase<GameManager>
{
    #region Fields

    [SerializeField]
    private List<GameObject> managersToSpawn;

    [SerializeField]
    private List<GameObject> eventsToSpawn;

    #endregion
    #region Propeties

    public List<GameObject> ManagersToSpawn
    {
        get => managersToSpawn; 
        private set => managersToSpawn = value;
    }

    public List<GameObject> EventsToSpawn { 
        get => eventsToSpawn; 
        private set => eventsToSpawn = value; 
    }

    #endregion
    #region Methods

    protected override void OnEnable()
    {
        base.OnEnable();

        List<GameObject> toSpawnOnAwake;
        toSpawnOnAwake = GetObjectsToSpawnOnAwake();
        SpawnAllObjects(toSpawnOnAwake);

        Debug.LogFormat("[{0}] Zainicjalizowany.".SetColor(Color.cyan), this.GetType());
    }

    private List<GameObject> GetObjectsToSpawnOnAwake()
    {
        List<GameObject> toSpawnObjects = new List<GameObject>();

        // Najpierw spawnowane są eventy pozniej dopiero managery.
        if(eventsToSpawn != null)
        {
            toSpawnObjects.AddRange(EventsToSpawn);
        }

        if(managersToSpawn != null)
        {
            toSpawnObjects.AddRange(ManagersToSpawn);
        }

        return toSpawnObjects;
    }

    private void SpawnAllObjects(List<GameObject> toSpawnObjects)
    {
        if(toSpawnObjects == null)
        {
            Debug.LogFormat("Brak elemwntow do zespawnowania na starcie gry {0}".SetColor(Color.red), this);
            return;
        }

        foreach (GameObject @object in toSpawnObjects)
        {
            GameObject spawnedObject = Instantiate(@object, transform.position, Quaternion.identity);
            spawnedObject.transform.SetParent(this.transform);
        }
    }

    #endregion
    #region Handlers

    #endregion
}
