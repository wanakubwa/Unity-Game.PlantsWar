using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PositiveCharactersManager : ManagerSingletonBase<PositiveCharactersManager>
{
    #region Fields

    #endregion

    #region Propeties
    public List<CharacterBase> SpawnedCharacters {
        get;
        private set;
    }

    public List<SingleCharacter> InGameCharacters {
        get;
        private set;
    }

    #endregion

    #region Methods

    public void SpawnCharacterInCell(CharacterBase character, GridCell cell)
    {
        CharacterBase newCharacter = Instantiate(character);
        newCharacter.transform.position = cell.SpawnPosition.position;
        newCharacter.transform.SetParent(transform);

        cell.IsEmpty = false;
        SpawnedCharacters.Add(newCharacter);
    }

    public List<CharacterBase> GetCharactersAwaibleToBuy()
    {
        // TODO:
        List<CharacterBase> output = new List<CharacterBase>();
        for(int i = 0; i < InGameCharacters.Count; i++)
        {
            output.Add(InGameCharacters[i].Characters[0]);
        }

        return output;
    }

    public CharacterBase GetCharacterByIdAndType(int id, CharacterType type)
    {
        List<CharacterBase> characters = GetCharactersBaseForType(type);
        if(characters == null)
        {
            Debug.LogFormat("Brak postaci pozytywnych dla danego typu: {0} {1}".SetColor(Color.red), type, this.GetType());
            return null;
        }

        foreach (CharacterBase character in characters)
        {
            if(character.Id == id)
            {
                return character;
            }
        }

        return null;
    }

    public List<CharacterBase> GetCharactersBaseForType(CharacterType type)
    {
        foreach (SingleCharacter character in InGameCharacters)
        {
            if(character.Type == type)
            {
                return character.Characters;
            }
        }

        return null;
    }

    public CharacterBase GetSpawnedCharacterById(int id)
    {
        if(SpawnedCharacters != null)
        {
            foreach(CharacterBase character in SpawnedCharacters)
            {
                if(character.Id == id)
                {
                    return character;
                }
            }
        }

        return null;
    }

    public void KillSpawnedCharacter(CharacterBase character)
    {
        if (character != null)
        {
            // TODO;
            //character?.kill
            RemoveSpawnedCharacter(character);
            Destroy(character.gameObject);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        // todo: przerobic na normalne wywoalnie.
        SpawnedCharacters = new List<CharacterBase>();

        // Pobranie wszystkich dostepnych characterow.
        InGameCharacters = GetPositiveCharacters();

        Debug.LogFormat("[{0}] Zainicjalizowany.".SetColor(Color.green), this.GetType());
    }

    private void Update() 
    {
        float deltaMilis = Time.deltaTime * 1000f;
        for(int i = 0; i < SpawnedCharacters.Count; i++)
        {
            SpawnedCharacters[i].Attack(deltaMilis);
        }
    }

    private void RemoveSpawnedCharacter(CharacterBase character)
    {
        if(SpawnedCharacters != null)
        {
            SpawnedCharacters.Remove(character);
        }
    }

    private List<SingleCharacter> GetPositiveCharacters()
    {
        CharactersContainerSetup charactersContainer = CharactersContainerSetup.Instance;
        return charactersContainer.GetAllAwaiblePositiveCharacters();
    }
    #endregion

    #region Handlers



    #endregion
}
