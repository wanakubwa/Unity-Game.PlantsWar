using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : ManagerSingletonBase<EnemyManager>, ISaveable, IContentLoadable
{
    #region Fields


    #endregion

    #region Propeties

    public event Action OnSpawnedEnemiesChanged = delegate { };
    public event Action<CharacterBase> OnEnemieSpawned = delegate { };

    public List<SingleCharacter> EnemyCharactersDefinitions
    {
        get;
        private set;
    } = new List<SingleCharacter>();

    public List<CharacterBase> EnemyCharactersSpawned { 
        get; 
        private set; 
    } = new List<CharacterBase>();

    public bool IsGameFreezed
    {
        get;
        private set;
    }

    #endregion

    #region Methods

    public void SpawnCharacterOfTypeAtPosition(CharacterType type, Vector3 position)
    {
        CharacterBase character = GetMaxUpgradedCharacterOfType(type);
        if(character != null)
        {
            CharacterBase spawnedCharacter = Instantiate(character);
            spawnedCharacter.gameObject.transform.SetParent(transform);
            spawnedCharacter.transform.position = position;

            EnemyCharactersSpawned.Add(spawnedCharacter);

            OnSpawnedEnemiesChanged.Invoke();
            OnEnemieSpawned(spawnedCharacter);
        }
        else
        {
            Debug.Log("Brak postaci do spawnu!".SetColor(Color.red));
        }
    }

    public CharacterBase GetMaxUpgradedCharacterOfType(CharacterType type)
    {
        // TODO:
        foreach (SingleCharacter character in EnemyCharactersDefinitions)
        {
            if(character.Type == type)
            {
                return character.Characters[0];
            }
        }

        return null;
    }

    public CharacterType GetRandomCharacterType()
    {
        int index = UnityEngine.Random.Range(0, EnemyCharactersDefinitions.Count);
        return EnemyCharactersDefinitions[index].Type;
    }

    public void RemoveSpawnedCharacter(CharacterBase character)
    {
        if(character != null)
        {
            EnemyCharactersSpawned.Remove(character);
            Destroy(character.gameObject);

            OnSpawnedEnemiesChanged.Invoke();
        }
    }

    public void KillSpawnedCharacter(CharacterBase character)
    {
        if(EnemyCharactersSpawned != null)
        {
            //TODO: wywolac animacje smierci zamast destroy.
            RemoveSpawnedCharacter(character);
        }
    }

    public void ResetFields()
    {
        for(int i = 0; i < EnemyCharactersSpawned.Count; i++)
        {
            Destroy(EnemyCharactersSpawned[i].gameObject);
        }

        EnemyCharactersSpawned.Clear();
    }

    public void Load()
    {
        EnemyManagerMemento enemyManagerMemento = SaveLoadManager.Instance.LoadManagerClass(this) as EnemyManagerMemento;
        if(enemyManagerMemento != null)
        {
            EnemyCharactersSpawned = enemyManagerMemento.EnemyCharactersSpawned;
        }

    }

    public void Save()
    {
        SaveLoadManager.Instance.SaveManagerClass(this);
    }

    public void LoadGameContent()
    {
        EnemyCharactersDefinitions = CharactersContainerSetup.Instance?.GetAllAwaibleEnemiesCharacters();
        if (EnemyCharactersDefinitions == null)
        {
            Debug.LogError("UWAGA! - Brak przeciwnikow do pobrania!");
        }
    }

    public void FreeGameContent()
    {
        for (int i = 0; i < EnemyCharactersSpawned.Count; i++)
        {
            Destroy(EnemyCharactersSpawned[i].gameObject);
        }

        EnemyCharactersSpawned.Clear();
    }

    protected override void OnEnable () {
        base.OnEnable ();

        EnemyCharactersSpawned = new List<CharacterBase>();

        Debug.LogFormat ("[{0}] Zainicjalizowany.".SetColor (Color.green), this.GetType ());
    }

    protected override void AttachEvents()
    {
        base.AttachEvents();

        GameEventsManager.Instance.OnGameFreez += OnGameFreezHandler;
        SaveLoadManager.Instance.OnResetGame += ResetFields;
        SaveLoadManager.Instance.OnLoadGame += Load;
        SaveLoadManager.Instance.OnSaveGame += Save;
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        GameEventsManager.Instance.OnGameFreez -= OnGameFreezHandler;
        SaveLoadManager.Instance.OnResetGame -= ResetFields;
        SaveLoadManager.Instance.OnLoadGame -= Load;
        SaveLoadManager.Instance.OnSaveGame -= Save;
    }

    private void Update() 
    {
        if (IsGameFreezed == false)
        {
            float deltaMilis = Time.deltaTime * 1000f;
            for (int i = 0; i < EnemyCharactersSpawned.Count; i++)
            {
                EnemyCharactersSpawned[i].Move(deltaMilis);
                EnemyCharactersSpawned[i].Attack(deltaMilis);
            }
        }
    }

    private void OnGameFreezHandler(bool isFreezed)
    {
        IsGameFreezed = isFreezed;
    }

    #endregion
}