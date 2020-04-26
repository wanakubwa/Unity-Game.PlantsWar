using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
[CreateAssetMenu (fileName = "FileContainerSetup.asset", menuName = "Settings/FileContainerSetup")]
public class FileContainerSetup : ScriptableObject
{
    #region Fields

    private static FileContainerSetup instance;

    private const char lineSeparator = '\n';
    private const char fieldSeparator = ',';

    [Space]
    [SerializeField]
    private Language languageVersion = Language.EN;

    [Space (10)]
    [SerializeField]
    private List<FileElement> filesCollection = new List<FileElement>();

    #endregion

    #region Propeties

    public static FileContainerSetup Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<FileContainerSetup> ("Setups/FileContainerSetup");
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public List<FileElement> FilesCollection
    {
        get => filesCollection;
        private set => filesCollection = value;
    }

    public Language LanguageVersion
    {
        get => languageVersion;
        private set => languageVersion = value;
    }

    public List<string[]> NamesData{
        get;
        private set;
    } = new List<string[]>();

    public List<string[]> StringsData
    {
        get;
        private set;
    } = new List<string[]>();

    #endregion

    #region Methods

    public void SetLanguageVersion (Language language)
    {
        LanguageVersion = language;
    }

    public string GetNameByLocalizedKey(string localizedKey)
    {
        for(int i = 0; i < NamesData.Count; i++)
        {
            if(NamesData[i][0] == localizedKey)
            {
                return NamesData[i][(int)languageVersion];
            }
        }

        Debug.LogFormat("Brak nazwy dla klucza {0}!".SetColor(Color.red), localizedKey);
        return string.Empty;
    }

    public string GetStringByLocalizedKey(string localizedKey)
    {
        for (int i = 0; i < StringsData.Count; i++)
        {
            if (StringsData[i][0] == localizedKey)
            {
                return StringsData[i][(int)languageVersion];
            }
        }

        Debug.LogFormat("Brak napisu dla klucza {0}!".SetColor(Color.red), localizedKey);
        return string.Empty;
    }

    public TextAsset GetTextFileByCategory(FileCategory category)
    {
        for(int i = 0; i < FilesCollection.Count; i++)
        {
            if(FilesCollection[i].Category == category)
            {
                return FilesCollection[i].CsvFile;
            }
        }

        Debug.LogFormat("Brak pliku danej kategorii! {0}".SetColor(Color.red), category);
        return null;
    }

    private void OnEnable() 
    {
        ReadNamesData();
        ReadStringsData();
    }

    private void ReadNamesData()
    {
        NamesData.Clear();

        TextAsset file = GetTextFileByCategory(FileCategory.NAMES);
        if(file == null)
        {
            return;
        }

        string[] lines = file.text.Split(lineSeparator);
        if(lines != null)
        {
            foreach (string line in lines)
            {
                string[] fields = line.Split(fieldSeparator);
                NamesData.Add(fields);
            }
        }

        Debug.Log("Nazwy wczytwane poprawie".SetColor(Color.green));
    }

    private void ReadStringsData()
    {
        StringsData.Clear();

        TextAsset file = GetTextFileByCategory(FileCategory.STRINGS);
        if (file == null)
        {
            return;
        }

        string[] lines = file.text.Split(lineSeparator);
        if (lines != null)
        {
            foreach (string line in lines)
            {
                string[] fields = line.Split(fieldSeparator);
                StringsData.Add(fields);
            }
        }

        Debug.Log("Napisy wczytwane poprawie".SetColor(Color.green));
    }

    #endregion

    #region Handlers

    #endregion

    #region Enums

    [Serializable]
    public class FileElement
    {
        #region Fields

        [SerializeField]
        private FileCategory category;

        [SerializeField]
        private TextAsset csvFile;

        #endregion

        #region Propeties

        public TextAsset CsvFile
        {
            get => csvFile;
            private set => csvFile = value;
        }

        public FileCategory Category
        {
            get => category;
            private set => category = value;
        }

        #endregion

        #region Methods

        #endregion

        #region Handlers

        #endregion

        #region Enums

        #endregion
    }

    public enum FileCategory
    {
        NAMES,
        DESCRIPTIONS,
        STRINGS
    }

    public enum Language
    {
        PL = 1,
        EN = 2
    }

    #endregion
}