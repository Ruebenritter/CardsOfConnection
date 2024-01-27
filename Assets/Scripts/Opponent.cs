using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : MonoBehaviour
{

    public Sprite basePortrait;
    private Animator animator;
    public float attractionLevel;
    public float stressLevel;

    public TextAsset dateDialog;


    private List<string> emotions = new List<string> {"very bad", "bad", "neutral", "good", "very good"};
    public List<Prompt> dialogueSequence = new List<Prompt> {};
    
    void Start()
    {
        animator = GetComponent<Animator>();
        attractionLevel = 0.0f;
        stressLevel = 0.0f;

        
    }

    
    void Update()
    {

        //animator.SetBool("something",grounded);
        
    }
}
