using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    public GameObject levelCompletePanel;
    public GameObject optionsScreen;



    private void Awake()
    {
        instance = this;

    }
    private void Update()
    {
        
    }
    public void MainMenu()
    {

        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");

        if (!PlayerPrefs.HasKey("Best Time"))
        {
            PlayerPrefs.SetInt("Best Time", 0);
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
        optionsScreen.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    

}
