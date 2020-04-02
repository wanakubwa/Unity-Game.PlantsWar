using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

[Serializable]
[CreateAssetMenu(fileName = "CharactersContainerSetup.asset", menuName = "Settings/CharactersContainerSetup")]
public class CharactersContainerSetup : ScriptableObject
{
    #region Fields
    private static CharactersContainerSetup instance;

    [SerializeField]
    private List<CharacterSet> postiveCharactersSet;

    [SerializeField]
    private List<CharacterSet> enemieCharactersCollection;

    #endregion

    #region Propeties

    public static CharactersContainerSetup Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<CharactersContainerSetup>("Setups/CharactersContainerSetup");
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public List<CharacterSet> PositiveCharactersSet { 
        get => postiveCharactersSet; 
        private set => postiveCharactersSet = value; 
    }

    public List<CharacterSet> EnemieCharactersCollection { 
        get => enemieCharactersCollection; 
        private set => enemieCharactersCollection = value; 
    }

    #endregion

    #region Methods

    public CharacterElement GetPositiveCharacterByIdAndType(int id, CharacterType type)
    {
        CharacterSet characterSet = GetPositiveCharacterSetOfType(type);
        if(characterSet != null)
        {
            foreach (CharacterElement character in characterSet.Characters)
            {
                if(character.CharacterPrefab.Id == id)
                {
                    return character;
                }
            }
        }

        return null;
    }

    public List<SingleCharacter> GetAllAwaiblePositiveCharacters()
    {
        List<SingleCharacter> characters = new List<SingleCharacter>();

        foreach (CharacterSet set in PositiveCharactersSet)
        {
            List<CharacterBase> characterBases = new List<CharacterBase>();
            foreach (CharacterElement element in set.Characters)
            {
                characterBases.Add(element.CharacterPrefab);
            }

            characters.Add(new SingleCharacter(set.Type, characterBases));
        }

        if(characters.Count == 0)
        {
            characters = null;
        }

        return characters;
    }

    public CharacterSet GetPositiveCharacterSetOfType(CharacterType type)
    {
        if (PositiveCharactersSet != null)
        {
            foreach (CharacterSet set in PositiveCharactersSet)
            {
                if (set.Type == type)
                {
                    return set;
                }
            }
        }

        return null;
    }

    public List<SingleCharacter> GetAllAwaibleEnemiesCharacters()
    {
        List<SingleCharacter> characters = new List<SingleCharacter>();

        foreach (CharacterSet set in EnemieCharactersCollection)
        {
            List<CharacterBase> characterBases = new List<CharacterBase>();
            foreach (CharacterElement element in set.Characters)
            {
                characterBases.Add(element.CharacterPrefab);
            }

            characters.Add(new SingleCharacter(set.Type, characterBases));
        }

        if(characters.Count == 0)
        {
            characters = null;
        }

        return characters;
    }

    #endregion

    #region Handlers

    #endregion

    [Serializable]
    public class CharacterSet
    {
        #region Fields
        
        [SerializeField]
        private CharacterType type;

        [SerializeField]
        private List<CharacterElement> characters;

        #endregion

        #region Propeties

        public CharacterType Type { 
            get => type; 
            private set => type = value; 
        }

        public List<CharacterElement> Characters { 
            get => characters; 
            private set => characters = value; 
        }

        #endregion

        #region Methods



        #endregion

        #region Handlers



        #endregion

        #region Enums



        #endregion
    }

    [Serializable]
    public class CharacterElement
    {
        #region Fields

        [SerializeField]
        private string localizeKey;

        [SerializeField]
        private CharacterBase characterPrefab;

        #endregion

        #region Propeties

        public string LocalizeKey { 
            get => localizeKey; 
            private set => localizeKey = value; 
        }

        public CharacterBase CharacterPrefab { 
            get => characterPrefab; 
            private set => characterPrefab = value; 
        }

        #endregion

        #region Methods



        #endregion

        #region Handlers



        #endregion
    }
}