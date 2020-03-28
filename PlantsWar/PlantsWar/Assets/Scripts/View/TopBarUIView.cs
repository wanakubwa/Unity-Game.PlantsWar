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

    #endregion

    #region Handlers



    #endregion
}