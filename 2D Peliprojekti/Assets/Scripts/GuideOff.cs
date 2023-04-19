using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideOff : MonoBehaviour
{
    public GameObject guidePanel;

    // Start is called before the first frame update
    void Start()
    {
        guidePanel.SetActive(true);
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GuideOffPanel()
    {
        guidePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
