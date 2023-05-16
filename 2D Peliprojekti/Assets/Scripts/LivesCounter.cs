using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.Rendering;
using UnityEditor.Tilemaps;

public class LivesCounter : MonoBehaviour
{
    bool canTakeDamage = true;
    public float damageTimeout = 1f;
    public GameObject player;
    public GameObject dieParticles;
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
        if(canTakeDamage)
        {
            hitPointsRemaining--;
            hitPoints[hitPointsRemaining].enabled = false;
            StartCoroutine(damageTimer());
        }
        

        if (hitPointsRemaining <= 0)
        {
            dieParticles.transform.position = player.transform.position;
            dieParticles.SetActive(true);  
            player.SetActive(false);
            Debug.Log("YOU LOST");
            Invoke("GameOverScreen", 2f);
        }

    }

    public void GameOverScreen()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        ScreenShakeController.instance.enabled = false;
        audioPlayer.enabled = false;
    }

    private IEnumerator damageTimer()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageTimeout);
        canTakeDamage = true;
        Debug.Log("HIT TIMER");
    }
}
