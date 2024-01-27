using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThoughtBubble : MonoBehaviour
{
    private Vector3 activePosition;
    // Start is called before the first frame update
    void Start()
    {
        activePosition = transform.position;
        MoveToRestingPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToRestingPos(){
        //move outside view
        transform.position = new Vector3(5, -20, 0);
    }

    public void MoveToActivePos(){
        //scale down, teleport in, animate growth
        LeanTween.scale(gameObject, new Vector3(0.2f, 0.2f, 0.2f), 0.0f);
        transform.position = activePosition;
        LeanTween.scale(gameObject, new Vector3(1.1f, 1.1f, 1.1f), 1.0f);
        LeanTween.scale(gameObject, new Vector3(1.0f, 1.0f, 1.0f), 0.1f).setDelay(1.0f);
    }
}
