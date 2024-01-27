using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : MonoBehaviour
{

    //public Sprite basePortrait;
    private Animator animator;
    public float attractionLevel;
    public float stressLevel;

    public TextAsset dateDialog;


    private List<string> emotions = new List<string> {"very bad", "bad", "neutral", "good", "very good"};
    public List<Prompt> dialogueSequence = new List<Prompt> {};
    private float currentEmotion;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        attractionLevel = 0.0f;
        stressLevel = 0.0f;
        currentEmotion = 0.1f;

        
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)){
            animator.SetFloat("CurrentEmotion",currentEmotion);
            if(currentEmotion<9.0f){
                currentEmotion += 2;
            }
            else{
                currentEmotion = 0.1f;
            }
        }

        
    }
}
