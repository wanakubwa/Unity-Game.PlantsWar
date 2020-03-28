using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

[Serializable]
[CreateAssetMenu(fileName = "CharactersContainerSetup.asset", menuName = "Settings/CharactersContainerSetup")]
public class CharactersContainerSetup : SingletonScriptableBase<CharactersContainerSetup>
{
    #region Fields

    [SerializeField]
    private List<SingleCharacter> positiveCharacters;

    #endregion

    #region Propeties

    public List<SingleCharacter> PositiveCharacters { 
        get => positiveCharacters; 
        private set => positiveCharacters = value; 
    }

    #endregion

    #region Methods

    public SingleCharacter GetPositiveCharacterByKey(string key)
    {
        if(PositiveCharacters != null)
        {
            foreach (SingleCharacter character in PositiveCharacters)
            {
                if(character.Key == key)
                {
                    return character;
                }
            }
        }

        return null;
    }

    #endregion

    #region Handlers

    #endregion

    [Serializable]
    public class SingleCharacter
    {
        #region Fields

        [SerializeField]
        private string key;

        [SerializeField]
        private int prize;

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

        #endregion

        #region Methods



        #endregion

        #region Handlers



        #endregion
    }
}