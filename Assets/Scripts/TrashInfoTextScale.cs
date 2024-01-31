using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashInfoTextScale : MonoBehaviour
{
    public Vector3 hoverScale = new Vector3(1.0f, 1.0f, 1.0f);
    private Vector3 originalScale;
    public float hoverDuration;
    
    void Start()
    {
        originalScale = transform.localScale;
    }

    
    void Update()
    {
        if(transform.localScale == hoverScale){
            LeanTween.scale(gameObject, originalScale, hoverDuration).setEase(LeanTweenType.easeOutQuad);
            }
        else if(transform.localScale == originalScale){
            LeanTween.scale(gameObject, hoverScale, hoverDuration).setEase(LeanTweenType.easeOutQuad);
            }
    }
}
