using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LivesCounter : MonoBehaviour
{
    public Image[] hitPoints;
    public int hitPointsRemaining;
    public GameObject gameOverPanel;
    CharacterMovement characterMovement;
    //ScreenShakeController screenShakeController;
    public AudioSource audioPlayer;

    // Start is called before the first frame update
    void Start()
    {
       characterMovement = FindObjectOfType<CharacterMovement>();
       //screenShakeController = GetComponent<ScreenShakeController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            LoseHitPoint();
        }
    }

    public void LoseHitPoint()
    {
        hitPointsRemaining--;
        hitPoints[hitPointsRemaining].enabled = false;

        
        if (hitPointsRemaining <= 0)
        {
            Debug.Log("YOU LOST");
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
            characterMovement.enabled = false;
            ScreenShakeController.instance.enabled = false;
            audioPlayer.enabled= false;

        }

    }

    
}
