using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitOnClick : MonoBehaviour {

    public Button yes, no;
    public GameObject current, next;

	// Use this for initialization
	void Start () {
        yes.onClick.AddListener(Quit);
        no.onClick.AddListener(Back);
	}

    void Back() {
        current.SetActive(false);
        next.SetActive(true);
    }

    void Quit() {
        Application.Quit();
    }
}
