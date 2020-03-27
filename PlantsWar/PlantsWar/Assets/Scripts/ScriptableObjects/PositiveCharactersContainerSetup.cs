using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

[Serializable]
[CreateAssetMenu(fileName = "PositiveCharactersContainerSetup.asset", menuName = "Settings/PositiveCharactersContainerSetup")]
public class PositiveCharactersContainerSetup : SingletonScriptableBase<PositiveCharactersContainerSetup>
{
    #region Fields

    [SerializeField]
    private List<SingleCharacter> characters;

    #endregion

    #region Propeties

    public List<SingleCharacter> Characters { 
        get => characters; 
        private set => characters = value; 
    }

    #endregion

    #region Methods



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