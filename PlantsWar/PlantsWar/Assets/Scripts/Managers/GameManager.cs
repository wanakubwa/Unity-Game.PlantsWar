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
        set => managersToSpawn = value;
    }

    public List<GameObject> EventsToSpawn { 
        get => eventsToSpawn; 
        set => eventsToSpawn = value; 
    }

    #endregion
    #region Methods

    private void OnEnable()
    {
        List<GameObject> toSpawnOnAwake;
        toSpawnOnAwake = GetObjectsToSpawnOnAwake();
        SpawnAllObjects(toSpawnOnAwake);

    }

    private List<GameObject> GetObjectsToSpawnOnAwake()
    {
        List<GameObject> toSpawnObjects = new List<GameObject>();

        // Najpierw spawnowane są eventy pozniej dopiero managery.
        if(eventsToSpawn != null)
        {
            toSpawnObjects.AddRange(eventsToSpawn);
        }

        if(managersToSpawn != null)
        {
            toSpawnObjects.AddRange(managersToSpawn);
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
