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

    public TMP_Text TrashInfoText;
    private Color TrashInfoTextColorTransp;
    private Color TrashInfoTextColorNormal;

    public AudioSource clickSound;



    void Awake(){

    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        TrashInfoText = TrashInfoText.GetComponent<TextMeshPro>();
        TrashInfoText.overrideColorTags = true;
        TrashInfoTextColorNormal = new Color (0.019f,0.080f,0.065f,0.990f);
        TrashInfoTextColorTransp = new Color (0.919f,0.980f,0.965f,0.000f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TrashInfoText.color = TrashInfoTextColorNormal;
        Debug.Log("TrashButton entered!");
        //LeanTween.value(gameObject, updateValueExampleCallback, fadeoutcolor, color, 1f).setEase(LeanTweenType.easeOutElastic).setDelay(2f);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TrashInfoText.color = TrashInfoTextColorTransp;
        Debug.Log("TrashButton leaving!");
    }

    public void OnPointerClick (PointerEventData eventData) {
        clickSound.Play();
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