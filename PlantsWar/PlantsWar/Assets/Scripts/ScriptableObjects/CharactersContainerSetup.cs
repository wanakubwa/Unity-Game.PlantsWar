﻿using UnityEngine;
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

    public SingleCharacter GetPositiveCharacterByKeyOfType(string key, CharacterType type)
    {
        CharacterSet characterSet = GetPositiveCharacterSetOfType(type);
        if(characterSet != null)
        {
            foreach (SingleCharacter character in characterSet.Characters)
            {
                if(character.Key == key)
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

    public SingleCharacter GetPositiveCharacterByKey(string key)
    {
        for(int i = 0 ; i< PositiveCharactersSet.Count; i++)
        {
            for(int y = 0; y < PositiveCharactersSet[i].Characters.Count; y++)
            {
                if(PositiveCharactersSet[i].Characters[y].Key == key)
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
        private List<SingleCharacter> characters;

        #endregion

        #region Propeties

        public CharacterType Type { 
            get => type; 
            private set => type = value; 
        }

        public List<SingleCharacter> Characters { 
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
    public class SingleCharacter
    {
        #region Fields

        [SerializeField]
        private string key;

        [SerializeField]
        private int prize;

        [SerializeField]
        private Sprite shopCardBackground; 

        [SerializeField]
        private GameObject characterPrefab;

        #endregion

        #region Propeties

        public string Key { 
            get => key; 
            private set => key = value; 
        }

        public int Prize { 
            get => prize; 
            private set => prize = value; 
        }

        public GameObject CharacterPrefab { 
            get => characterPrefab; 
            private set => characterPrefab = value; 
        }

        public Sprite ShopCardBackground { 
            get => shopCardBackground; 
            private set => shopCardBackground = value; 
        }

        #endregion

        #region Methods



        #endregion

        #region Handlers



        #endregion
    }
}