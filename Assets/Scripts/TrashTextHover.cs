using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TrashTextHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Color TextColorTransp;
    private Color TextColorNormal;
    private TextMeshProUGUI text;


    
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        var color = text.color;

        TextColorNormal = new Color (0.019f,0.080f,0.065f,0.990f);
        TextColorTransp = new Color (0.019f,0.080f,0.065f,0.000f);
        
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = TextColorNormal;
        //LeanTween.value(gameObject, updateValueExampleCallback, fadeoutcolor, color, 1f).setEase(LeanTweenType.easeOutElastic).setDelay(2f);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = TextColorTransp;
    }

}
