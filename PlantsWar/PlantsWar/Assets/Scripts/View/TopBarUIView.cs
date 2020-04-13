using UnityEngine;
using TMPro;

public class TopBarUIView : MonoBehaviour
{
    #region Fields

    [Space]
    [SerializeField]
    private TextMeshProUGUI coinsText;
    [SerializeField]
    private TextMeshProUGUI coinsNumber;
    [SerializeField]
    private TextMeshProUGUI livesText;
    [SerializeField]
    private TextMeshProUGUI livesNumber;

    #endregion

    #region Propeties

    public TextMeshProUGUI CoinsText { 
        get => coinsText; 
        private set => coinsText = value; 
    }

    public TextMeshProUGUI CoinsNumber { 
        get => coinsNumber; 
        private set => coinsNumber = value; 
    }

    public TextMeshProUGUI LivesText { 
        get => livesText; 
        private set => livesText = value; 
    }

    public TextMeshProUGUI LivesNumber { 
        get => livesNumber; 
        private set => livesNumber = value; 
    }

    #endregion

    #region Methods

    public void SetCoinsText(string text)
    {
        CoinsText.text = text;
    }

    public void SetCoinsTextColor(Color color)
    {
        CoinsText.color = color;
    }

    public void SetCoinsNumber(int number)
    {
        CoinsNumber.text = number.ToString();
    }

    public void SetLivesText(string text)
    {
        LivesText.text = text;
    }

    public void SetLivesNumber(int playerLives, int totalLives)
    {
        LivesNumber.text = string.Format("{0}/{1}", playerLives, totalLives);
    }

    #endregion

    #region Handlers



    #endregion
}