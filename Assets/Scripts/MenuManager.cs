using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public SceneChanger sceneChanger;
    public GameObject StartScreen;
    public GameObject CreditsScreen;
    public GameObject LoseScreen;
    public GameObject WinScreen;
    
    void Start()
    {
        

        StartScreen.SetActive(true);
        CreditsScreen.SetActive(false);
        LoseScreen.SetActive(false);
        WinScreen.SetActive(false);
    }

    
    void Update()
    {
        
        
    }

    public void OpenCredits(){
        StartScreen.SetActive(false);
        CreditsScreen.SetActive(true);
    }

    public void CloseCredits(){
        StartScreen.SetActive(true);
        CreditsScreen.SetActive(false);
    }


    public void QuitGame(){
        Application.Quit();
    }
}
