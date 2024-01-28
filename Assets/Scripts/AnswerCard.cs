using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class AnswerCard : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool selectable; //disallow card selection when trashed or flipped over
    public string text;
    public int attractionChange;

    private GameObject stackAnchor;

    private Vector3 hoverScale;
    private float hoverDuration = 0.3f;
 
    private bool isInHand = false;

    public float drawAnimDurationInSeconds = 1.4f;

    //Transfrom variables
    private Vector3 hoverPosition;
    private Vector3 originalScale;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    
    private Vector3 placeInHandAnchor;
    private GameManager gameManager;

    public AudioSource bubbleclickSound;


void Awake(){
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        bubbleclickSound = GetComponent<AudioSource>();
}   
    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
        hoverScale = originalScale * 1.2f;
        selectable = false;
        isInHand = false;
    }   

    // Update is called once per frame
    void Update()
    {

    }

    public void SetContent(string text, int baddieValue){
        this.text = text;
        this.attractionChange = baddieValue;
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

    public async void OnPointerClick (PointerEventData eventData) {
        if (selectable){
            bubbleclickSound.Play();
            await gameManager.HandleCardClick(this);
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
        LeanTween.move(gameObject, placeInHandAnchor, hoverDuration).setEase(LeanTweenType.easeOutQuad);
        LeanTween.scale(gameObject, originalScale, hoverDuration).setEase(LeanTweenType.easeOutQuad);
    }

    public void ResetPosition(){
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        transform.localScale = originalScale;
        print("reset position: " + originalPosition);
    }

    public void SetSelectable(bool selectable){
        this.selectable = selectable;
    }

    public void SetPlaceInHand(Vector3 pos){
        placeInHandAnchor = pos;
        print("set place in hand anchor: " + placeInHandAnchor);
        //Set hover position sligthly above and in front of place in hand
        hoverPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z - 0.5f);
    }

    public IEnumerator DrawFromStack(){
        //Animate flip and flying from stack anchor to start position
        print("Draw moving from " + transform.position + " to " + placeInHandAnchor);
        LeanTween.move(gameObject, placeInHandAnchor, drawAnimDurationInSeconds).setEase(LeanTweenType.easeOutCubic);
        LeanTween.rotateY(gameObject, 0, drawAnimDurationInSeconds).setEase(LeanTweenType.easeOutElastic);

        yield return new WaitForSeconds(drawAnimDurationInSeconds);
        selectable = true;
        isInHand = true;
    }

    public IEnumerator DrawHiddenFromStack(){
        var animationDuration = 0.9f;
        //Animate flip and flying from stack anchor to start position
        LeanTween.move(gameObject, placeInHandAnchor, animationDuration).setEase(LeanTweenType.easeOutSine);

        yield return new WaitForSeconds(animationDuration);
        selectable = false;
        isInHand = true;
    }

    public void PlaceOnStack(UnityEngine.Vector3 stackPosition){
        LeanTween.move(gameObject, stackPosition, 0.0f);
        LeanTween.rotateY(gameObject, 180, 0.0f);
        isInHand = false;
        selectable = false;
    }
}
