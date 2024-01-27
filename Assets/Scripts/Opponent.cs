using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Opponent : MonoBehaviour
{

    //public Sprite basePortrait;
    private Animator animator;
    public float attractionLevel;
    public float attractionLevelMax;
    public float attractionLevelMin;
    public float baseAttractionLevel;
    

    public TextAsset dateDialog;

    private List<string> emotions = new List<string> {"very bad", "bad", "neutral", "good", "very good"};
    public List<Prompt> dialogueSequence = new List<Prompt> {};
    
    void Start()
    {
        animator = GetComponent<Animator>();
        attractionLevel = baseAttractionLevel;
        SetOpponentEmotion(attractionLevel);
        
    }

    public float GetAttractionLevel(){
        return attractionLevel;
    }

    void Update()
    {

    }   
    public void AddToEmotion(float delta){
        animator.SetFloat("CurrentEmotion",attractionLevel+delta);
    }

    public void SetOpponentEmotion(float emotionValue){
        animator.SetFloat("CurrentEmotion",attractionLevel);
    }

    public bool AttractionFailed(){
        if(attractionLevel <= attractionLevelMin){
            return true;
        }
        else{
            return false;
        }
    }

    public bool AttractionSucceeded(){
        if(attractionLevel >= attractionLevelMax){
            return true;
        }
        else{
            return false;
        }
    }
}
