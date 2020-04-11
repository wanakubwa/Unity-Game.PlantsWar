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

    #endregion
    #region Methods

    public void LoadMenuScene ()
    {
        SceneManager.LoadScene (0, LoadSceneMode.Single);
    }

    public void LoadGameScene (bool isContinueRequired)
    {
        SceneManager.LoadScene (1, LoadSceneMode.Single);
        IsContinueRequired = isContinueRequired;
    }

    protected override void OnEnable ()
    {
        base.OnEnable ();

        SceneManager.sceneLoaded += OnLevelFinishedLoading;

        if (SceneManager.GetActiveScene ().buildIndex != 0)
        {
            SpawnObjects();
        }

        Debug.LogFormat ("[{0}] Zainicjalizowany.".SetColor (Color.cyan), this.GetType ());
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

        if (objs.Length > 1)
        {
            gameObject.SetActive (false);
            Destroy (gameObject);
        }

        DontDestroyOnLoad (gameObject);
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

        foreach (GameObject toSpawn in toSpawnObjects)
        {
            GameObject spawnedObject = Instantiate (toSpawn);
            spawnedObject.transform.SetParent (this.transform);

            SpawnedElementsCollection.Add (spawnedObject);
        }
    }

    private void CheckLoadedScene ()
    {
        if (SceneManager.GetActiveScene ().buildIndex == 0)
        {
            // Menu.
            if (SpawnedElementsCollection != null)
            {
                foreach (GameObject element in SpawnedElementsCollection)
                {
                    Destroy(element);
                }
            }
        }
        else if (SceneManager.GetActiveScene ().buildIndex == 1)
        {
            // GameScene
            SpawnObjects();

            if(IsContinueRequired == true)
            {
                SaveLoadManager.Instance.CallLoadGame();
            }
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