using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void QuitApp(){
        Application.Quit();
    }

    void Update(){
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
