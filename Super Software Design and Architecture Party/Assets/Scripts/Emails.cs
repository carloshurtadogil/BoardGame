using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emails : MonoBehaviour {

    public string[] saved = new string[50];
    int a = 0;

    public void Addsave(string x)
    {
        saved[a] = x;
        a++;
    }


    public string getsave(int i)
    {
        return saved[i];
    }
}
