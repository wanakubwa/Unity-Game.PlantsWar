using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public class MainMenuController : MonoBehaviour 
{
    #region Fields

    [SerializeField]
    private TMP_Dropdown languageDropdown;

    #endregion

    #region Propeties

    public TMP_Dropdown LanguageDropdown { 
        get => languageDropdown; 
        private set => languageDropdown = value; 
    }

    #endregion

    #region Methods

    public void OnNewGameButton()
    {
        GameManager.Instance.LoadGameScene(false);
    }

    public void OnContinueGameButton()
    {
        GameManager.Instance.LoadGameScene(true);
    }

    public void OnOptionsButtons()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=4oLnJiYN_GE");
    }

    public void OnFacebookButton()
    {
        Application.OpenURL("https://www.facebook.com/GeekBox-109186153819085/");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void OnLanguageDropdownChanged()
    {
        FileContainerSetup.Language language = (FileContainerSetup.Language)LanguageDropdown.value + 1;
        FileContainerSetup.Instance.SetLanguageVersion(language);
    }

    private void OnEnable()
    {
        LanguageDropdown.ClearOptions();

        string[] enumNames = Enum.GetNames(typeof(FileContainerSetup.Language));
        LanguageDropdown.AddOptions(new List<string>(enumNames));

        LanguageDropdown.SetValueWithoutNotify((int)FileContainerSetup.Instance.LanguageVersion - 1);
    }

    #endregion

    #region Handlers



    #endregion

    #region Enums



    #endregion
} 