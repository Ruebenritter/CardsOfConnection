using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TrashButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private GameManager gameManager;
    public GameObject openBin;
    public GameObject closedBin;

    //public TMP_Text TrashInfoText;
    //private Color TrashInfoTextColorTransp;
    //private Color TrashInfoTextColorNormal;
    //public GameObject Backgr;

    public GameObject TrashInfoText;
    public GameObject thoughBubble;

    public AudioSource clickSound;
    public AudioSource trashBinSound;

    private bool thoughBubbleOnEnterActive;



    void Awake(){

    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        /*
        TrashInfoText = TrashInfoText.GetComponent<TextMeshPro>();
        TrashInfoText.overrideColorTags = true;
        TrashInfoTextColorNormal = new Color (0.619f,0.713f,0.917f,0.990f);
        TrashInfoTextColorTransp = new Color (0.919f,0.980f,0.965f,0.000f);
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        thoughBubbleOnEnterActive = thoughBubble.activeSelf;
        thoughBubble.SetActive(false);
        TrashInfoText.SetActive(true);
        /*
        TrashInfoText.color = TrashInfoTextColorNormal;
        Backgr.SetActive(true);
        Debug.Log("TrashButton entered!");
        //LeanTween.value(gameObject, updateValueExampleCallback, fadeoutcolor, color, 1f).setEase(LeanTweenType.easeOutElastic).setDelay(2f);
        */
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TrashInfoText.SetActive(false);
        thoughBubble.SetActive(thoughBubbleOnEnterActive);
        /*
        TrashInfoText.color = TrashInfoTextColorTransp;
        Backgr.SetActive(false);
        Debug.Log("TrashButton leaving!");
        */
    }

    public void OnPointerClick (PointerEventData eventData) {
        trashBinSound.volume = 0.3f;
        trashBinSound.Play();
        TrashInfoText.SetActive(false);
        thoughBubble.SetActive(thoughBubbleOnEnterActive);
        Debug.Log("TrashButton clicked");
        StartCoroutine(BinOpening());
        gameManager.TrashCards();
    }

    private IEnumerator BinOpening(){
        openBin.SetActive(true);
        closedBin.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        openBin.SetActive(false);
        closedBin.SetActive(true);
    }

}