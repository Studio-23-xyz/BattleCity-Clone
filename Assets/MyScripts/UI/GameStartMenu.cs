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
    public GameObject ConfirmPassword;

    private bool _takeNextInput = true;
    private bool _playerSignUp = false;

    public TMP_InputField Username;
    public TMP_InputField Password;

    public GameObject SignUpButton;
    public GameObject SignUpTank;
    


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

        if (context.performed && !_playerSignUp)
            InvokeMethod();
        else if(context.performed && _playerSignUp)
            InvokeMethodSecond();

    }



    public void MoveUp()
    {
        _activatedButton = 0;
        ShowImage();
    }

    public void MoveDown()
    {
        _activatedButton = 1;
        ShowImage();
    }



    public void ShowImage()
    {
        foreach (var button in ButtonGameObjectsTanks)
        {
            button.GetComponent<Image>().enabled = false;
        }




        ButtonGameObjectsTanks[_activatedButton].GetComponent<Image>().enabled = true;




        /*foreach (var button in Buttons)
        {
            button.GetComponent<Image>().color = new Color(1f, 1f, 1f, .5f);
        }

        Buttons[_activatedButton].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);*/

        


    }

    public async void InvokeMethod()
    {
        if (_activatedButton == 1)
        {
            _takeNextInput = false;
        }
        //await UniTask.Delay(TimeSpan.FromSeconds(.5f));
        if (ButtonGameObjectsTanks[_activatedButton] != null)
            ButtonGameObjectsTanks[_activatedButton].GetComponent<Button>().onClick?.Invoke();

        

    }

    public async void InvokeMethodSecond()
    {
        //await UniTask.Delay(TimeSpan.FromSeconds(.5f));
        SignUpButton.GetComponent<Button>().onClick?.Invoke();



    }

    public void ClearErrorText()
    {
        Username.text = "";
        Password.text = "";
        ErrorReport.text = "";
    }

    public void ReplaceLogin()
    {

        foreach (var tank in ButtonGameObjectsTanks)
        {
            tank.SetActive(false);
        }

        foreach (var button in Buttons)
        {
            button.SetActive(false);
        }


        ConfirmPassword.SetActive(true);
        SignUpButton.SetActive(true);
        SignUpTank.SetActive(true);

        _playerSignUp = true;
    }

    public void Reset()
    {


        ConfirmPassword.SetActive(false);
        SignUpButton.SetActive(false);
        SignUpTank.SetActive(false);

        foreach (var button in Buttons)
        {
            button.SetActive(true);
        }

        foreach (var tank in ButtonGameObjectsTanks)
        {
            tank.SetActive(true);
        }

        _playerSignUp = false;
        _activatedButton = 0;
        ShowImage();
        ButtonGameObjectsTanks[_activatedButton].SetActive(true);
        _takeNextInput = true;
        PlayFabController.Instance.SetLoginEvent(true);
    }
}
