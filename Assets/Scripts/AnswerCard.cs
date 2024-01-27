using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class AnswerCard : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool selectable; //disallow card selection when trashed or flipped over
    public string text;
    public int baddieValue;

    //private Transform stackAnchor;
    private Vector3 originalPosition;
    private Vector3 hoverPosition;
    private Vector3 originalScale;
    private Vector3 hoverScale = new Vector3(1.7f, 1.7f, 1.0f);
    private float hoverDuration = 0.3f;
    private Vector3 startPos;
    private Quaternion startRot;
    private bool isInHand = false;
    
    public GameObject stackAnchor;

    private GameManager gameManager;

    public AudioSource bubbleclickSound;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //stackAnchor = GameObject.Find("StackAnchor").transform;
        
        startPos = transform.position;
        startRot = transform.rotation;
        selectable = true;

        // Store original position and scale for resetting
        originalPosition = transform.position;
        originalScale = transform.localScale;

        // Calculate hover position slightly above the original position
        hoverPosition = originalPosition + new Vector3(0f, 0.2f, -1f);
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
        LeanTween.moveLocalY(gameObject, 1.0f, 0.2f).setEase(LeanTweenType.easeInOutQuart);
        LeanTween.moveLocalY(gameObject, -20.0f, 0.5f).setEase(LeanTweenType.easeInOutQuart);
        selectable = false;
    }

    public void OnPointerClick (PointerEventData eventData) {
        if (selectable){
            bubbleclickSound.Play();
            gameManager.HandleCardClick(this);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!isInHand){
            return;
        }
        // Play hover animation
        LeanTween.move(gameObject, hoverPosition, hoverDuration).setEase(LeanTweenType.easeOutQuad);
        LeanTween.scale(gameObject, hoverScale, hoverDuration).setEase(LeanTweenType.easeOutQuad);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!isInHand){
            return;
        }
        // Play exit animation
        LeanTween.move(gameObject, originalPosition, hoverDuration).setEase(LeanTweenType.easeOutQuad);
        LeanTween.scale(gameObject, originalScale, hoverDuration).setEase(LeanTweenType.easeOutQuad);
    }

    public void ResetPosition(){
        transform.position = startPos;
        transform.rotation = startRot;
        selectable = true;
    }

    public void SetSelectable(bool selectable){
        this.selectable = selectable;
    }

    public IEnumerator DrawFromStack(){
        var animationDuration = 2.0f;
        //Animate flip and flying from stack anchor to start position
        
        
        LeanTween.move(gameObject, startPos, animationDuration).setEase(LeanTweenType.easeInOutQuart);
        LeanTween.rotateY(gameObject, 0, animationDuration).setEase(LeanTweenType.easeInOutQuart);

        yield return new WaitForSeconds(animationDuration);
        selectable = true;
        isInHand = true;
    }

    public IEnumerator DrawHiddenFromStack(){
        var animationDuration = 2.2f;
        //Animate flip and flying from stack anchor to start position
        LeanTween.move(gameObject, startPos, animationDuration).setEase(LeanTweenType.easeInOutQuart);

        yield return new WaitForSeconds(animationDuration);
        selectable = true;
        isInHand = true;
    }

    public void PlaceOnStack(){
        transform.position = stackAnchor.transform.position;
        LeanTween.rotateY(gameObject, 180, 0.0f);
        isInHand = false;
    }
}
