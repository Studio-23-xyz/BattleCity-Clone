using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameUtils;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TutorialButtonClick : MonoBehaviour
{

    [SerializeField] private GameObject _button;
    [SerializeField] private GameObject _text;

    void Start()
    {
        BlinkingEffect();
    }


    public void SubmitOption(InputAction.CallbackContext context)
    {
        if (!gameObject.activeInHierarchy)
            return;

        if (context.performed)
            InvokeMethod();

    }


    public void InvokeMethod()
    {

        if (_button != null)
            _button.GetComponent<Button>().onClick?.Invoke();
    }


    public async void BlinkingEffect()
    {
        bool state = true;
        while (true)
        {
            if(_text == null)
                break;
            _text.SetActive(state);
            state = !state;
            if(state)
                await UniTask.Delay(TimeSpan.FromSeconds(.5f));
            else
                await UniTask.Delay(TimeSpan.FromSeconds(1f));
        }
    }
}
