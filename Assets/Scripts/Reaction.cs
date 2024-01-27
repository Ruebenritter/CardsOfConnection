using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaction : MonoBehaviour
{
    public GameObject positiveReaction;
    public GameObject negativeReaction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPositiveReaction(){
        positiveReaction.SetActive(true);
        negativeReaction.SetActive(false);
    }

    public void SetNegativeReaction(){
        positiveReaction.SetActive(false);
        negativeReaction.SetActive(true);
    }
}
