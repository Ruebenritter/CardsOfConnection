using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : MonoBehaviour
{

    //public Sprite basePortrait;
    private Animator animator;

    public TextAsset dateDialog;
    public float attractionLevel;

    private List<string> emotions = new List<string> {"very bad", "bad", "neutral", "good", "very good"};
    public List<Prompt> dialogueSequence = new List<Prompt> {};
    private float currentEmotion;

    public ProgressBars progressBars;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        attractionLevel = 5.0f;
        progressBars.SetAttractionBar(attractionLevel/10);
        setOpponentEmotion(attractionLevel);
    }

    

    
    
    
        /*
        if(Input.GetKeyDown(KeyCode.Return)){
            animator.SetFloat("CurrentEmotion",currentEmotion);
            if(currentEmotion<9.0f){
                currentEmotion += 2;
            }
            else{
                currentEmotion = 0.1f;
            }
        }   
        */
    

        
    public void addToEmotion(float delta){
        animator.SetFloat("CurrentEmotion",attractionLevel+delta);
    }

    public void setOpponentEmotion(float emotionValue){
        animator.SetFloat("CurrentEmotion",attractionLevel);
    }
}
