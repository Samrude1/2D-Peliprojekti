using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    CharacterMovement characterMovement;
    float currentTime;
    float startingTime = 0f;
    public float recordTime = 300f;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI recordTimeText;
    [SerializeField] GameObject pauseScreen;
    public bool isPaused = false;

    private void Awake()
    {  
        recordTime = PlayerPrefs.GetFloat("Best Time");
        recordTimeText.text = recordTime.ToString("0.0");
    }
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
        characterMovement = FindObjectOfType<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += 1 * Time.deltaTime;
        timerText.text = currentTime.ToString("0.0");
        DeleteFloat();
        PauseMenu();
        Menu();
    }

    public void LogRecordtTime()
    {
        if(recordTime == 0)
        {
            PlayerPrefs.SetFloat("Best Time", currentTime);
        }

        else if(currentTime < recordTime)
        {
            PlayerPrefs.SetFloat("Best Time", currentTime);
            recordTimeText.text = recordTime.ToString("0.0");
            Debug.Log("LOG ONNN");
        }
    }

    public void DeleteFloat()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            PlayerPrefs.DeleteKey("Best Time");
            Debug.Log("TIME DELETED");
        }
    }

    void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pauseScreen.activeSelf)
            {
                pauseScreen.SetActive(false);
                Time.timeScale = 1f;
                characterMovement.enabled = true;
            }

            else
            {
                pauseScreen.SetActive(true);
                Time.timeScale = 0;
                characterMovement.enabled = false;
            }
        }

    }
    public void Menu()
    {
       if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            SceneManager.LoadScene("MainMenu");
        }
    }


}
