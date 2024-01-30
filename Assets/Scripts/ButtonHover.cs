using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public Vector3 hoverScale = new Vector3(1.0f, 1.0f, 1.0f);
    private Vector3 originalScale;
    private float hoverDuration = 0.4f;

    
    void Start()
    {
        originalScale = transform.localScale;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        LeanTween.scale(gameObject, hoverScale, hoverDuration).setEase(LeanTweenType.easeOutQuad);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, originalScale, hoverDuration).setEase(LeanTweenType.easeOutQuad);
    }

    public void OnPointerClick (PointerEventData eventData) {
        LeanTween.scale(gameObject, originalScale, hoverDuration).setEase(LeanTweenType.easeOutQuad);
    }
}
