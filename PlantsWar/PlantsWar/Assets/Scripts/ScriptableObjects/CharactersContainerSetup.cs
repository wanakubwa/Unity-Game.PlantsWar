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

    #endregion

    #region Methods

    public PositiveCharacterElement GetPositiveCharacterByKeyOfType(string key, CharacterType type)
    {
        CharacterSet characterSet = GetPositiveCharacterSetOfType(type);
        if(characterSet != null)
        {
            foreach (PositiveCharacterElement character in characterSet.Characters)
            {
                if(character.LocalizeKey == key)
                {
                    return character;
                }
            }
        }

        return null;
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

    public PositiveCharacterElement GetPositiveCharacterByKey(string key)
    {
        for(int i = 0 ; i< PositiveCharactersSet.Count; i++)
        {
            for(int y = 0; y < PositiveCharactersSet[i].Characters.Count; y++)
            {
                if(PositiveCharactersSet[i].Characters[y].LocalizeKey == key)
                {
                    return PositiveCharactersSet[i].Characters[y];
                }
            }
        }

        return null;
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
        private List<PositiveCharacterElement> characters;

        #endregion

        #region Propeties

        public CharacterType Type { 
            get => type; 
            private set => type = value; 
        }

        public List<PositiveCharacterElement> Characters { 
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
    public class PositiveCharacterElement
    {
        #region Fields

        [SerializeField]
        private string localizeKey;

        [SerializeField]
        private Sprite shopCardBackground; 

        [SerializeField]
        private PositiveCharacterBase characterPrefab;

        #endregion

        #region Propeties

        public string LocalizeKey { 
            get => localizeKey; 
            private set => localizeKey = value; 
        }

        public PositiveCharacterBase CharacterPrefab { 
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