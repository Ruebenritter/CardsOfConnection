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

    public AudioSource clickSound;
    
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

    public void StartGame(){
        clickSound.Play();
        
        sceneChanger.ChangeScene("GameLoop");
    }

    public void OpenCredits(){
        clickSound.Play();
        StartScreen.SetActive(false);
        CreditsScreen.SetActive(true);
    }

    public void CloseCredits(){
        clickSound.Play();
        StartScreen.SetActive(true);
        CreditsScreen.SetActive(false);
    }


    public void QuitGame(){
        clickSound.Play();
        Application.Quit();
    }
}
