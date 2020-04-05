using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : ManagerSingletonBase<EnemyManager> {
    #region Fields

    private List<SingleCharacter> enemyCharactersDefinitions;

    private List<CharacterBase> enemyCharactersSpawned;

    #endregion

    #region Propeties

    public List<SingleCharacter> EnemyCharactersDefinitions { 
        get => enemyCharactersDefinitions; 
        private set => enemyCharactersDefinitions = value; 
    }

    public List<CharacterBase> EnemyCharactersSpawned { 
        get => enemyCharactersSpawned; 
        private set => enemyCharactersSpawned = value; 
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
        }

        // TODO: INFO ZE LISTA SIE ZMIENILA.
    }
    public void KillSpawnedCharacter(CharacterBase character)
    {
        if(EnemyCharactersSpawned != null)
        {
            RemoveSpawnedCharacter(character);
        }
    }

    protected override void OnEnable () {
        base.OnEnable ();

        EnemyCharactersSpawned = new List<CharacterBase>();
        
        EnemyCharactersDefinitions = CharactersContainerSetup.Instance?.GetAllAwaibleEnemiesCharacters();
        if(EnemyCharactersDefinitions == null)
        {
            Debug.LogError("UWAGA! - Brak przeciwnikow do pobrania!");
        }
    }

    private void Update() 
    {
        float deltaMilis = Time.deltaTime * 1000f;
        for(int i = 0; i < EnemyCharactersSpawned.Count; i++)
        {
            EnemyCharactersSpawned[i].Move(deltaMilis);
            EnemyCharactersSpawned[i].Attack(deltaMilis);
        }
    }

    #endregion
}