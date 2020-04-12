using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

[Serializable]
[CreateAssetMenu(fileName = "FileContainerSetup.asset", menuName = "Settings/FileContainerSetup")]
public class FileContainerSetup : ScriptableObject
{
    #region Fields
    
    private static FileContainerSetup instance;

    private const char lineSeparator = '\n';
    private const char fieldSeparator = ',';

    [Space(10)]
    [SerializeField]
    private List<FileElement> filesCollection;
    
    #endregion
    
    #region Propeties
    
    public static FileContainerSetup Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<FileContainerSetup>("Setups/FileContainerSetup");
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public List<FileElement> FilesCollection { 
        get => filesCollection; 
        private set => filesCollection = value; 
    }

    #endregion

    #region Methods



    #endregion

    #region Handlers



    #endregion

    #region Enums

    [Serializable]
    public class FileElement 
    {
        #region Fields
        
        [SerializeField]
        private FileTypes fileType;

        [SerializeField]
        private TextAsset csvFile;


        #endregion

        #region Propeties

        public TextAsset CsvFile { 
            get => csvFile; 
            private set => csvFile = value; 
        }

        public FileTypes FileType { 
            get => fileType; 
            private set => fileType = value; 
        }

        #endregion

        #region Methods



        #endregion

        #region Handlers



        #endregion

        #region Enums



        #endregion
    }

    public enum FileTypes
    {
        CHARACTERS_NAME,
        CHARACTERS_DESCRIPTION
    }
    
    #endregion
}