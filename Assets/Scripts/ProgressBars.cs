using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBars : MonoBehaviour
{
    public GameObject opponent;
    private bool attractionLevelfade;

    public GameObject attractionBar;
    
    private Scrollbar attractionBarLevel;
    private float attractionLevelNew;
    private float transitionSpeed;

    public float attractionLevelToBarRation = 10;
   
    void Start()
    {
        attractionLevelfade = false;
        attractionBarLevel = attractionBar.GetComponent<Scrollbar>();
        transitionSpeed = 0.01f;
    }

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            SetAttractionBar(Random.Range(0.0f,1.0f));
        }

        attractionLevelNew = opponent.GetComponent<Opponent>().GetAttractionLevel() / attractionLevelToBarRation;

        if(attractionLevelNew > attractionBarLevel.size + transitionSpeed){
            attractionBarLevel.size += transitionSpeed;
        }
        else if(attractionLevelNew < attractionBarLevel.size - transitionSpeed){
            attractionBarLevel.size -= transitionSpeed;
        }
        
    }

    public void SetAttractionBar(float attractionLevel){

            //attractionBarLevel.size = attractionLevel;
            attractionLevelNew = attractionLevel;
    }


    public void AddAttractoin(float attractionChange){
        attractionLevelNew += attractionChange;
    }
}