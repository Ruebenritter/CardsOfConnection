using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagerGame : MonoBehaviour
{
    public GameObject PauseScreen;
    public bool gamePaused;

    public AudioSource clickSound;

    void Start()
    {
        PauseScreen.SetActive(false);
        gamePaused = false;
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(!gamePaused){
                clickSound.Play();
                PauseScreen.SetActive(true);
                gamePaused = true;
            }
            else{
                clickSound.Play();
                PauseScreen.SetActive(false);
                gamePaused = false;
            }
                
        }
    }

    public void ContinueGame(){
        clickSound.Play();
        PauseScreen.SetActive(false);
        gamePaused = false;
    }
}
