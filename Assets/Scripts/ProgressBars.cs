using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBars : MonoBehaviour
{
    private bool attractionLevelfade;
    private bool stressLevelfade;

    public GameObject stressBar;
    public GameObject attractionBar;
    
    private Scrollbar stressBarLevel;
    private Scrollbar attractionBarLevel;

    private float stressLevelNew;
    private float attractionLevelNew;
    private float transitionSpeed;
   
    void Start()
    {
        attractionLevelfade = false;
        stressLevelfade = false;
        stressBarLevel = stressBar.GetComponent<Scrollbar>();
        attractionBarLevel = attractionBar.GetComponent<Scrollbar>();
        
        SetAttractionBar(0.1f);
        SetStressBar(0.1f);

        transitionSpeed = 0.01f;

    }

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            SetAttractionBar(Random.Range(0.0f,1.0f));
            SetStressBar(Random.Range(0.0f,1.0f));
        }

        if(attractionLevelNew > attractionBarLevel.size + transitionSpeed){
            attractionBarLevel.size += transitionSpeed;
        }
        else if(attractionLevelNew < attractionBarLevel.size - transitionSpeed){
            attractionBarLevel.size -= transitionSpeed;
        }

        if(stressLevelNew > stressBarLevel.size + transitionSpeed){
            stressBarLevel.size += transitionSpeed;
        }
        else if(stressLevelNew < stressBarLevel.size - transitionSpeed){
            stressBarLevel.size -= transitionSpeed;
        }
        
    }

    public void SetAttractionBar(float attractionLevel){

            //attractionBarLevel.size = attractionLevel;
            attractionLevelNew = attractionLevel;
    }

    public void SetStressBar(float stressLevel){
        //stressBarLevel.size = stressLevel;
        stressLevelNew = stressLevel;
        
    }
}
