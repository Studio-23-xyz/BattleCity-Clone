using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEvent : MonoBehaviour
{
    public GameObject Tutorial;
    public GameObject Menu;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowTutorial()
    {

        Tutorial.SetActive(true);
        Menu.SetActive(false);
    }

    public void HideTutorial()
    {
        Tutorial.SetActive(false);
    }
}
