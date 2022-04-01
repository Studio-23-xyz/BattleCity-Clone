using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelActivision : MonoBehaviour
{

    public GameObject LoginPanel;
    public GameObject StartingPanel;
    public GameObject SignUpPanel;
    public TextMeshProUGUI ButtonText;
    public GameObject ErrorReport;


    void Start()
    {

        

        if (PanelData.Instance.ShowLoginPanel)
            LoginPanel.SetActive(true);
        else
            StartingPanel.SetActive(true);

        PanelData.Instance.ShowLoginPanel = false;

    }

    // Update is called once per frame
    public void LogInOptionSelected(string loginText)
    {
        LoginPanel.SetActive(false);

        ButtonText.text = loginText;
        SignUpPanel.SetActive(true);
    }

    public void BackToLoginOptionPage()
    {
        SignUpPanel.SetActive(false);
        ErrorReport.SetActive(true);
        LoginPanel.SetActive(true);
    }


}
