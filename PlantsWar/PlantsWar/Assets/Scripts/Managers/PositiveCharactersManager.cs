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

    public List<GameObject> SpawnedCharacters {
        get;
        private set;
    }

    public List<SingleCharacter> InGameCharacters {
        get;
        private set;
    }

    #endregion

    #region Methods

    public void SpawnCharacterInCell(GridCell cell)
    {
        if(cell.IsEmpty == false)
        {
            return;
        }

        ShopManager shopManager = ShopManager.Instance;
        if(shopManager == null)
        {
            return;
        }

        CharacterBase characterToSpawn = shopManager.SelectedCharacter;
        if(characterToSpawn == null)
        {
            Debug.Log("Postac wybrana w sklepie to null!".SetColor(Color.red));
            return;
        }

        // Kupienie jezeli garcza stac na aktualnie wybrana postac.
        if(shopManager.TryBuySelectedCharacter() == true)
        {
            GameObject newCharacter = Instantiate(characterToSpawn.gameObject);
            newCharacter.transform.position = cell.SpawnPosition.position;
            newCharacter.transform.SetParent(transform);

            cell.IsEmpty = false;
            SpawnedCharacters.Add(newCharacter);
        }
    }

    public List<Tuple<CharacterBase, CharacterType>> GetCharactersAwaibleToBuy()
    {
        List<Tuple<CharacterBase, CharacterType>> output = new List<Tuple<CharacterBase, CharacterType>>();

        for(int i = 0; i < InGameCharacters.Count; i++)
        {
            output.Add(new Tuple<CharacterBase, CharacterType>(InGameCharacters[i].Characters[0], InGameCharacters[i].Type));
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

    protected override void OnEnable()
    {
        base.OnEnable();

        // todo: przerobic na normalne wywoalnie.
        SpawnedCharacters = new List<GameObject>();

        // Pobranie wszystkich dostepnych characterow.
        InGameCharacters = GetPositiveCharacters();

        Debug.LogFormat("[{0}] Zainicjalizowany.".SetColor(Color.green), this.GetType());
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
