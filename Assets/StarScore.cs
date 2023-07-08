using TMPro;
using UnityEngine;
public class StarScore : MonoBehaviour
{
    
    public TextMeshProUGUI starScoreText;



    private void Update()
    {
        UpdateScore();
    }

    void UpdateScore()
    {
        starScoreText.text = "StarScore: " + EnemyManager.Instance.DeadCount.ToString();
    }

}
