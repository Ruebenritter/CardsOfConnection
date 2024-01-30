using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public AudioSource clickSound;

    void Start(){
        if(SceneManager.GetActiveScene().name != "WelcomeScreen" & SceneManager.GetActiveScene().name != "GameOver" & SceneManager.GetActiveScene().name != "WinningScreen"){
            clickSound.Play();
        }
        
    }

    public void ChangeScene(string sceneName){
        //clickSound.Play();
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame(){
        Application.Quit();
    }
    
}
