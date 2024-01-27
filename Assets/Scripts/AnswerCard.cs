using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class AnswerCard : MonoBehaviour, IPointerClickHandler
{
    private bool selectable; //disallow card selection when trashed or flipped over
    public string text;
    public int baddieValue;
    

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        selectable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetContent(string text, int baddieValue){
        this.text = text;
        this.baddieValue = baddieValue;
        var textMesh = GetComponentInChildren<TextMeshPro>();
        textMesh.text = text;
    }

    public void FlipToBack(){
        transform.Rotate(0, 180, 0);
        selectable = false;
    }

    public void FlipToReveal(){
        // animate flip
        LeanTween.rotateY(gameObject, 0, 0.5f).setEase(LeanTweenType.easeInOutQuart);
        selectable = true;
    }

    public void DropDown(){
        // animate drop
        LeanTween.moveLocalY(gameObject, -40.0f, 0.5f).setEase(LeanTweenType.easeInOutQuart);
        selectable = false;
    }

    public void OnPointerClick (PointerEventData eventData) {
        if (selectable){
            gameManager.HandleCardClick(this);
        }
    }
}
