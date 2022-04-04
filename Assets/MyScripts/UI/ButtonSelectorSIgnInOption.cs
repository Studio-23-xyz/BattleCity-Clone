using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonSelectorSIgnInOption : MonoBehaviour
{
    public GameObject[] ButtonGameObjects;
    [SerializeField] private int _activatedButton = 0;
    public AudioClip ButtonClickSound;
    public AudioClip ButtonSelectionSound;


    public void ResetActivateButton()
    {
        _activatedButton = 0;
        ShowImage();
    }

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
           


            ShowImage();
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

        _activatedButton = 0;

    }

    public void MoveDown()
    {
        _activatedButton = 1;
   
    }

    

    public void ShowImage()
    {
        foreach (var button in ButtonGameObjects)
        {
            button.GetComponent<Image>().enabled = false;
        }

        ButtonGameObjects[_activatedButton].GetComponent<Image>().enabled = true;
    }

    public async void InvokeMethod()
    {
        //AudioManager.Instance.PlaySFX(ButtonClickSound);
        await UniTask.Delay(TimeSpan.FromSeconds(.5f));
        if (ButtonGameObjects[_activatedButton] != null)
            ButtonGameObjects[_activatedButton].GetComponent<Button>().onClick?.Invoke();
    }

}
