using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public List<GameObject> EventsToSpawn
    {
        get => eventsToSpawn;
        private set => eventsToSpawn = value;
    }

    public List<GameObject> SpawnedElementsCollection
    {
        get;
        private set;
    } = new List<GameObject> ();

    public bool IsContinueRequired{
        get;
        private set;
    }

    public List<IManager> SpawnedManagers
    {
        get;
        private set;
    }

    #endregion
    #region Methods

    public void LoadMenuScene ()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void LoadGameScene (bool isContinueRequired)
    {
        IsContinueRequired = isContinueRequired;
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    protected override void OnEnable ()
    {
        base.OnEnable ();

        IsContinueRequired = false;

        Debug.LogFormat ("[{0}] Zainicjalizowany.".SetColor (Color.cyan), this.GetType ());
    }

    protected override void AttachEvents()
    {
        base.AttachEvents();

        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    protected override void DetachEvents ()
    {
        base.DetachEvents ();

        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    protected override void Awake ()
    {
        base.Awake ();

        GameManager[] objs = FindObjectsOfType<GameManager> ();
        if(objs.Length == 1)
        {
            SpawnObjects();
        }

        if (objs.Length > 1)
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void SpawnObjects ()
    {
        List<GameObject> toSpawnOnAwake;
        toSpawnOnAwake = GetObjectsToSpawnOnAwake ();
        SpawnAllObjects (toSpawnOnAwake);
    }

    private List<GameObject> GetObjectsToSpawnOnAwake ()
    {
        List<GameObject> toSpawnObjects = new List<GameObject> ();

        // Najpierw spawnowane są eventy pozniej dopiero managery.
        if (eventsToSpawn != null)
        {
            toSpawnObjects.AddRange (EventsToSpawn);
        }

        if (managersToSpawn != null)
        {
            toSpawnObjects.AddRange (ManagersToSpawn);
        }

        return toSpawnObjects;
    }

    private void SpawnAllObjects (List<GameObject> toSpawnObjects)
    {
        if (toSpawnObjects == null)
        {
            Debug.LogFormat ("Brak elemwntow do zespawnowania na starcie gry {0}".SetColor (Color.red), this);
            return;
        }

        SpawnedManagers = new List<IManager>();
        foreach (GameObject toSpawn in toSpawnObjects)
        {
            GameObject spawnedObject = Instantiate (toSpawn);
            spawnedObject.transform.SetParent (this.transform);

            IManager imanager = spawnedObject.GetComponent<IManager>();
            if (imanager != null)
            {
                SpawnedManagers.Add(imanager);
            }

            SpawnedElementsCollection.Add (spawnedObject);
        }

        Debug.Log(SpawnedManagers.Count);
    }

    private void CheckLoadedScene ()
    {
        if(IsContinueRequired == true)
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SaveLoadManager.Instance.RefreshContentForSceneWithLoad(sceneIndex, SpawnedManagers);
        }
        else
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SaveLoadManager.Instance.RefreshContentForScene(sceneIndex, SpawnedManagers);
        }
    }

    #endregion
    #region Handlers

    void OnLevelFinishedLoading (Scene scene, LoadSceneMode mode)
    {
        CheckLoadedScene ();
    }

    #endregion
}