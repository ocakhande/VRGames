using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    public static GameOver instance; 
    public GameObject gameOverPanel;

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


    private void Start()
    {
        gameOverPanel.SetActive(false);
    }


    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

}
