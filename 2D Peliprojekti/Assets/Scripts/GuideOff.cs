using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideOff : MonoBehaviour
{
    public GameObject guideOff;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GuideOffPanel()
    {
        guideOff.SetActive(false);
        Time.timeScale = 1;
    }
}
