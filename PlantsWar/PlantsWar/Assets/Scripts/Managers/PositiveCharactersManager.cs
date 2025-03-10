﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PositiveCharactersManager : ManagerSingletonBase<PositiveCharactersManager>, ISaveable, IContentLoadable
{
    #region Fields

    #endregion

    #region Propeties

    public event Action<CharacterBase> OnCharacterSpawn = delegate { };
    public event Action<CharacterBase> OnCharacterKill = delegate { };

    public List<CharacterBase> SpawnedCharacters {
        get;
        private set;
    }

    public List<SingleCharacter> InGameCharacters
    {
        get;
        private set;
    } = new List<SingleCharacter>();

    public bool IsGameFreezed {
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
        newCharacter.CellId = cell.Id;

        cell.IsEmpty = false;
        SpawnedCharacters.Add(newCharacter);

        OnCharacterSpawn(newCharacter);
    }

    public void SpawnCharacterInCellId(CharacterBase character, int cellId)
    {
        GridCell cell = GridManager.Instance.GetCellByID(cellId);
        if(cell == null)
        {
            return;
        }

        SpawnCharacterInCell(character, cell);
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

            OnCharacterKill(character);
        }
    }

    public CharacterBase GetCharacterInCellByCellId(int cellId)
    {
        if(SpawnedCharacters != null)
        {
            foreach (CharacterBase character in SpawnedCharacters)
            {
                if(character.CellId == cellId)
                {
                    return character;
                }
            }
        }

        return null;
    }

    public void KillAllCharacters()
    {
        for (int i = 0; i < SpawnedCharacters.Count; i++)
        {
            Destroy(SpawnedCharacters[i].gameObject);
        }

        SpawnedCharacters.Clear();
    }

    public void ResetFields()
    {
        KillAllCharacters();
    }

    public void Load()
    {
        PositiveCharactersManagerMemento memento = SaveLoadManager.Instance.LoadManagerClass(this) as PositiveCharactersManagerMemento;
        if(memento != null)
        {
            // Reset sceny ma sens tylko do testow potem usunac.
            KillAllCharacters();

            List<CharacterBaseMemento> characterBasesMemento = memento.SpawnedCharacters;
            if(characterBasesMemento != null)
            {
                foreach (CharacterBaseMemento characterMemento in characterBasesMemento)
                {
                    CharacterBase character = GetCharacterByIdAndType(characterMemento.Id, characterMemento.Type);

                    // TODO: puntky zdrowia tez trzeba jakos przywrocic.
                    SpawnCharacterInCellId(character, characterMemento.CellId);
                }
            }
        }
    }

    public void Save()
    {
        SaveLoadManager.Instance.SaveManagerClass(this);
    }

    public void LoadGameContent()
    {
        // Pobranie wszystkich dostepnych characterow.
        InGameCharacters = GetPositiveCharacters();
    }

    public void FreeGameContent()
    {
        KillAllCharacters();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        // todo: przerobic na normalne wywoalnie.
        SpawnedCharacters = new List<CharacterBase>();

        Debug.LogFormat("[{0}] Zainicjalizowany.".SetColor(Color.green), this.GetType());
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
            for (int i = 0; i < SpawnedCharacters.Count; i++)
            {
                SpawnedCharacters[i].Attack(deltaMilis);
            }
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

    private void OnGameFreezHandler(bool isFreezed)
    {
        IsGameFreezed = isFreezed;
    }

    #endregion
}
