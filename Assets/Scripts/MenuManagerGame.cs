using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagerGame : MonoBehaviour
{
    public GameObject PauseScreen;
    public bool gamePaused;

    void Start()
    {
        PauseScreen.SetActive(false);
        gamePaused = false;
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(!gamePaused){
                PauseScreen.SetActive(true);
                gamePaused = true;
            }
            else{
                PauseScreen.SetActive(false);
                gamePaused = false;
            }
                
        }
    }

    public void ContinueGame(){
        PauseScreen.SetActive(false);
        gamePaused = false;
    }
}
