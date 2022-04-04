using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class GameStartMenu : MonoBehaviour
{
    public GameObject[] ButtonGameObjectsTanks;
    public GameObject[] Buttons;
    [SerializeField] private int _activatedButton = 0;
    public AudioClip ButtonClickSound;
    public AudioClip ButtonSelectionSound;
    public TextMeshProUGUI ErrorReport;
    public GameObject SubmitButton;
    private bool _takeNextInput = true;
    


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

        if (context.performed && _takeNextInput)
        {
            _takeNextInput = false;
            AudioManager.Instance.PlaySFX(ButtonSelectionSound);
            if (context.ReadValue<Vector2>().x < 0)
            {
                MoveLeft();
            }
            else if (context.ReadValue<Vector2>().x > 0)
            {
                MoveRight();
            }
            else if (context.ReadValue<Vector2>().y < 0)
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


    public void MoveLeft()
    {

        _activatedButton = 0;
        ShowImage();

    }

    public void MoveRight()
    {
        _activatedButton = 1;
        ShowImage();

    }

    public void MoveUp()
    {
        _activatedButton = 0;
        ShowImage();
    }

    public void MoveDown()
    {
        _activatedButton = 2;
        ShowImage();
    }



    public void ShowImage()
    {
        foreach (var button in ButtonGameObjectsTanks)
        {
            button.GetComponent<Image>().enabled = false;
        }




        ButtonGameObjectsTanks[_activatedButton].GetComponent<Image>().enabled = true;


        if (_activatedButton == 2)
        {
            _takeNextInput = true;
            return;
        }



        foreach (var button in Buttons)
        {
            button.GetComponent<Image>().color = new Color(1f, 1f, 1f, .5f);
        }

        Buttons[_activatedButton].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

        InvokeMethod();


    }

    public async void InvokeMethod()
    {
        //await UniTask.Delay(TimeSpan.FromSeconds(.5f));
        if (ButtonGameObjectsTanks[_activatedButton] != null)
            ButtonGameObjectsTanks[_activatedButton].GetComponent<Button>().onClick?.Invoke();

        _takeNextInput = true;

    }

    public void ClearErrorText()
    {
        ErrorReport.text = "";
    }
}
