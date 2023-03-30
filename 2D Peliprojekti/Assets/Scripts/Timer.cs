using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    CharacterMovement characterMovement;
    float currentTime;
    float startingTime = 0f;
    public float recordTime = 300f;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI recordTimeText;
    

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
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            PlayerPrefs.DeleteKey("Best Time");
            Debug.Log("TIME DELETED");
        }
    }

      
    





}
