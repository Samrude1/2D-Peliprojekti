using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LivesCounter : MonoBehaviour
{
    public Image[] hitPoints;
    public int hitPointsRemaining;
    

    // Start is called before the first frame update
    void Start()
    {
        
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    
}
