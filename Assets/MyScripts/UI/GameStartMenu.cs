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
    public GameObject[] RegisterButtonsTanks;
    public GameObject[] RegisterButtons;
    public GameObject[] Buttons;
    [SerializeField] private int _activatedButton = 0;
    public AudioClip ButtonClickSound;
    public AudioClip ButtonSelectionSound;
    public TextMeshProUGUI ErrorReport;
    public GameObject SubmitButton;
    public GameObject ConfirmPassword;

    private bool _takeNextInput = true;
    [SerializeField]private bool _playerSignUp = false;

    public TMP_InputField Username;
    public TMP_InputField Password;
    public TMP_InputField ConfirmPasswordText;

    public GameObject SignUpButton;
    public GameObject SignUpTank;

    [SerializeField]private int _previousActivatedButton;
    [SerializeField] private bool _InputNotFocused = true;



    void Start()
    {
        _InputNotFocused = false;
        _activatedButton = 0;
        ShowImage();
    }


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

        if (context.performed && _takeNextInput && _InputNotFocused)
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


    public void CancelInput(InputAction.CallbackContext context)
    {
        if (!gameObject.activeInHierarchy)
            return;

        if (context.performed)
        {

            _InputNotFocused = true;
            ChangeButtonColor();
            //MoveDown();
        }

        //do something to visibly indicate that input is selected

    }



    public void MoveUp()
    {
        _previousActivatedButton = _activatedButton;
        int max;
        if (_playerSignUp)
            max = RegisterButtons.Length - 1;
        else
            max = Buttons.Length - 1;
        _activatedButton = Mathf.Clamp(--_activatedButton, 0, max);
        ShowImage();
    }

    public void MoveDown()
    {

        _previousActivatedButton = _activatedButton;
        int max;
        if (_playerSignUp)
            max = RegisterButtons.Length - 1;
        else
            max = Buttons.Length - 1;
        _activatedButton = Mathf.Clamp(++_activatedButton, 0, max);

        ShowImage();
    }





    public void ChangeButtonColor()
    {
        if (!_playerSignUp)
        {
            if(_activatedButton<2)
                Buttons[_activatedButton].GetComponent<Image>().color = new Color(1f, 1f, 1f, .7f);
        }

        else if (_playerSignUp)
        {
            if(_activatedButton <3)
                RegisterButtons[_activatedButton].GetComponent<Image>().color = new Color(1f, 1f, 1f, .7f);
        }
    }


    public void ShowImage()
    {

        if (!_playerSignUp)
        {
            foreach (var button in ButtonGameObjectsTanks)
            {
                button.GetComponent<Image>().enabled = false;
            }
            ButtonGameObjectsTanks[_activatedButton].GetComponent<Image>().enabled = true;


            foreach (var button in Buttons)
            {
                button.GetComponent<Image>().color = new Color(1f, 1f, 1f, .4f);
            }

            Buttons[_activatedButton].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);


            if (_activatedButton == 0 || _activatedButton == 1)
            {
                InvokeMethod();
                DisableInputFieldSelect();
            }
            else
            {
                DisableInputFieldSelect();
            }

        }
        else if (_playerSignUp)
        {
            foreach (var button in RegisterButtonsTanks)
            {
                button.GetComponent<Image>().enabled = false;
            }
            RegisterButtonsTanks[_activatedButton].GetComponent<Image>().enabled = true;


            foreach (var button in RegisterButtons)
            {
                button.GetComponent<Image>().color = new Color(1f, 1f, 1f, .4f);
            }

            RegisterButtons[_activatedButton].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);


            if (_activatedButton == 0 || _activatedButton == 1 || _activatedButton == 2)
            {
                InvokeMethodSecond();
            }
            else
            {
                DisableInputFieldSelect();
            }


        }

    }

    public async void InvokeMethod()
    {
        /*if (_activatedButton == 1)
        {
            _takeNextInput = false;
        }*/
        //await UniTask.Delay(TimeSpan.FromSeconds(.5f));
        if (ButtonGameObjectsTanks[_activatedButton] != null)
            ButtonGameObjectsTanks[_activatedButton].GetComponent<Button>().onClick?.Invoke();

        

    }

    public async void InvokeMethodSecond()
    {
        //await UniTask.Delay(TimeSpan.FromSeconds(.5f));
        if (RegisterButtonsTanks[_activatedButton] != null)
            RegisterButtonsTanks[_activatedButton].GetComponent<Button>().onClick?.Invoke();

    }

    public void ClearErrorText()
    {
        Username.text = "";
        Password.text = "";
        ErrorReport.text = "";
        ConfirmPasswordText.text = "";

    }

    public void ReplaceLogin()
    {

        foreach (var tank in ButtonGameObjectsTanks)
        {
            tank.SetActive(false);
        }

        /*for (int i = ButtonGameObjectsTanks.Length- 1; i > 1 ; i--)
        {
            ButtonGameObjectsTanks[i].SetActive(false);
        }*/



        foreach (var button in Buttons)
        {
            button.SetActive(false);
        }

        /*for (int i = Buttons.Length - 1; i > 1; i--)
        {
            Buttons[i].SetActive(false);
        }*/



        foreach (var button in RegisterButtons)
        {
            button.SetActive(true);
        }


        foreach (var button in RegisterButtonsTanks)
        {
            button.SetActive(true);
        }

        ConfirmPassword.SetActive(true);
        /*SignUpButton.SetActive(true);
        SignUpTank.SetActive(true);*/

        _playerSignUp = true;
        _activatedButton = 0;
        ShowImage();
    }

    public void Reset()
    {


        ConfirmPassword.SetActive(false);

        foreach (var button in RegisterButtons)
        {
            button.SetActive(false);
        }


        foreach (var button in RegisterButtonsTanks)
        {
            button.SetActive(false);
        }

        /*SignUpButton.SetActive(false);
        SignUpTank.SetActive(false);*/

        foreach (var button in Buttons)
        {
            button.SetActive(true);
        }

        foreach (var tank in ButtonGameObjectsTanks)
        {
            tank.SetActive(true);
        }


        /*for (int i = ButtonGameObjectsTanks.Length - 1; i > 1; i--)
        {
            ButtonGameObjectsTanks[i].SetActive(true);
        }


        for (int i = Buttons.Length - 1; i > 1; i--)
        {
            Buttons[i].SetActive(true);
        }
        */



        _playerSignUp = false;
        _activatedButton = 0;
        ShowImage();
        ButtonGameObjectsTanks[_activatedButton].SetActive(true);
        //_takeNextInput = true;
        PlayFabController.Instance.SetLoginEvent(true);
    }


    public void InputFieldSelect()
    {
        _InputNotFocused = false;
        Debug.Log("Input Field Selected");
        if(!_playerSignUp)
            Buttons[_activatedButton].GetComponent<TMP_InputField>()?.ActivateInputField();
        else
            RegisterButtons[_activatedButton].GetComponent<TMP_InputField>()?.ActivateInputField();


    }

    public void DisableInputFieldSelect()
    {
        if(!_playerSignUp)
            Buttons[_previousActivatedButton].GetComponent<TMP_InputField>()?.DeactivateInputField();
        else
            RegisterButtons[_previousActivatedButton].GetComponent<TMP_InputField>()?.DeactivateInputField();


    }

    public void SetUpDownBool(bool state)
    {
        _InputNotFocused = state;
    }
}
