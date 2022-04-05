using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeGame : MonoBehaviour
{

    public GameObject[] SpawnerAnimation;
    void Update()
    {
        if (GameUtils.Game.Instance.IsGamePaused)
        {
            PauseAnimation();
        }
        else if (!GameUtils.Game.Instance.IsGamePaused)
        {
            ResumeAnimation();
        }
    }
    public void Print()
    {
        Debug.Log("Resume Button Called");
    }

    public void PauseAnimation()
    {
        foreach (var anim in SpawnerAnimation)
        {
            if (anim.activeInHierarchy)
            {
                anim.GetComponent<Animator>().speed = 0;
            }
        }
    }

    public void ResumeAnimation()
    {
        foreach (var anim in SpawnerAnimation)
        {
            if (anim.activeInHierarchy)
            {
                anim.GetComponent<Animator>().speed = 1;
            }
        }
    }
}
