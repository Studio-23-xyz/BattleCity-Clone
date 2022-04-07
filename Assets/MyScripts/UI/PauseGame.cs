using System.Collections;
using System.Collections.Generic;
using GameUtils;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour
{

    public GameObject Pause;
    private bool _state;

    public void EnableDisablePause(InputAction.CallbackContext context)
    {
        

        if (context.performed && !GameUtils.Game.Instance.isLevelDone)
        {
            Debug.Log("Pause button clicked");
            Game.Instance.ChangeState();
        }

    }

    public void PauseGamePanel()
    {
        if(!GameUtils.Game.Instance.isLevelDone)
            Pause.SetActive(!Pause.activeInHierarchy);
    }
}
