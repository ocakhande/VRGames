using TMPro;
using UnityEngine;

public class StarScore : MonoBehaviour
{
    public int cash = 0;
    public TextMeshProUGUI starScoreText;
    public static StarScore instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void UpdateScore() 
    {
        starScoreText.text = "$ Cash: " + cash.ToString();
    }
}

