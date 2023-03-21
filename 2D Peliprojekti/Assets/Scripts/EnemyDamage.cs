using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    
    AudioSource audioSource;
    public AudioClip damageClip;
    LivesCounter livesCounter;
    ScreenShakeController screenShakeController;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        livesCounter = FindObjectOfType<LivesCounter>();
        screenShakeController= GetComponent<ScreenShakeController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audioSource.clip = damageClip;
            audioSource.Play();
            livesCounter.LoseHitPoint();
            ScreenShakeController.instance.StartShake(0.2f, 0.2f);
        }
    }




}
