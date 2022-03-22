using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonSelector : MonoBehaviour
{
    public GameObject[] ButtonGameObjects;
    private int _activatedButton = 0;
    public AudioClip ButtonClickSound;
    public AudioClip ButtonSelectionSound;


    public void SelectOption(InputAction.CallbackContext context)
    {

        if (!gameObject.activeInHierarchy)
            return;

        Debug.Log(("Naviagtion Button CLicked"));

        if (context.performed)
        {
            AudioManager.Instance.PlaySFX(ButtonSelectionSound);
            if (context.ReadValue<Vector2>().y < 0)
            {
                MoveDown();
            }
            else if (context.ReadValue<Vector2>().y > 0)
            {
                MoveUp();
            }

        }
        
    }


    public void SubmitOption(InputAction.CallbackContext context)
    {
        if (!gameObject.activeInHierarchy)
            return;

        if (context.performed)
            InvokeMethod();
        

    }


    public void MoveUp()
    {
        ButtonGameObjects[1].GetComponent<Image>().enabled = false;
        ButtonGameObjects[0].GetComponent<Image>().enabled = true;
        _activatedButton = 0;
    }

    public void MoveDown()
    {

        ButtonGameObjects[1].GetComponent<Image>().enabled = true;
        ButtonGameObjects[0].GetComponent<Image>().enabled = false;
        _activatedButton = 1;
    }

    public void InvokeMethod()
    {
        AudioManager.Instance.PlaySFX(ButtonClickSound);
        ButtonGameObjects[_activatedButton].GetComponent<Button>().onClick?.Invoke();
    }
    
}
