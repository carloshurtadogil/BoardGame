using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Addodatabase : MonoBehaviour {

    public InputField email;
    public Text error;
    public GameObject current, next, finalprompt;
    public Button go, exit;

    private string text;

    void Start()
    {
        go.onClick.AddListener(NextPane);
        exit.onClick.AddListener(QuitGame);
    }

    void NextPane() 
    {
        text = email.text.Trim();
        //otherObject.Addsave(text);
        if (ValidLength(text))
        {
            current.SetActive(false);
            UpdateErrorMessage("");
            DataManager.UserName = text;
            next.SetActive(true);
        }
        else
        {
            UpdateErrorMessage("*Username must be more than 3 characters in length");
        }

        email.text = "";

    }

    void QuitGame() 
    {
        DataManager.UserName = "";
        current.SetActive(false);
        finalprompt.SetActive(true);
    }

    public void UpdateErrorMessage(string str)
    {
        error.text = str;
    }

    public bool ValidLength(string str)
    {
        return str.Length >= 3;
    }


}
