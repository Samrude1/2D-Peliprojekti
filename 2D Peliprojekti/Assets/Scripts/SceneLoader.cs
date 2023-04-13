using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    public GameObject levelCompletePanel;



    private void Awake()
    {
        instance = this;

    }

    public void MainMenu()
    {

        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");

        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        levelCompletePanel.SetActive(false);
    }

    public void GameOver()
    {

        //SceneManager.LoadScene("GameOver");
    }

    public void Options()
    {
        //SceneManager.LoadScene("Options");
    }

    public void Quit()
    {
        Application.Quit();
    }



}
