using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBars : MonoBehaviour
{
    public GameObject opponent;

    public GameObject attractionBar;
    public GameObject stressBar;
    
    private Scrollbar attractionBarLevel;
    private Scrollbar stressBarLevel;
    private float attractionLevelNew;
    private float transitionSpeed;

    public AudioSource timerunsout;



    public float attractionLevelToBarRation = 10;
   
    void Start()
    {
        attractionBarLevel = attractionBar.GetComponent<Scrollbar>();
        stressBarLevel = stressBar.GetComponent<Scrollbar>();
        transitionSpeed = 0.01f;
        stressBarLevel.size = 1.0f;
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

        stressBarLevel.size -= Time.deltaTime * 0.0025f;
        if(stressBarLevel.size < 0.15f){
            if(!timerunsout.isPlaying){
                timerunsout.Play();
            }
        }
        
    }

    public bool TimerHasRunOut(){
        return stressBarLevel.size <= 0.01f;
    }

    public void SetAttractionBar(float attractionLevel){

            //attractionBarLevel.size = attractionLevel;
            attractionLevelNew = attractionLevel;
    }


    public void AddAttractoin(float attractionChange){
        attractionLevelNew += attractionChange;
    }
}