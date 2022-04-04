using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SignInBackButton : MonoBehaviour
{
    public Button BackButton;
    public void SelectBackButton(InputAction.CallbackContext context)
    {
        if (!gameObject.activeInHierarchy)
            return;
        BackButton.GetComponent<Button>().onClick?.Invoke();
    }
}
