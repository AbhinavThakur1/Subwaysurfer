using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingScreen : MonoBehaviour
{
    [SerializeField] TMP_Text Score;
    [SerializeField] TMP_Text Coins;

    private void Start()
    {
        Score.text = "Top Score: " + PlayerPrefs.GetInt("Score");
        Coins.text = "Coins: " + PlayerPrefs.GetInt("Coins");
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
