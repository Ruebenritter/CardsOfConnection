using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawButton : MonoBehaviour, IPointerClickHandler
{
    Vector3 startPos = new Vector3(0, -4, -3);
    Vector3 hiddenPos = new Vector3(0, -10, -3);
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        MoveToActivePos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick (PointerEventData eventData)
    {
        if(gameManager != null){
            gameManager.StartGame();
        }
        MoveToInactivePos();
    }

    private void MoveToActivePos(){
        //teleport down outside screenview
        transform.position = hiddenPos;
        //tween to active position
        LeanTween.move(gameObject, startPos, 2.0f).setEase(LeanTweenType.easeOutQuart);
    }

    private void MoveToInactivePos(){
        //tween drop fast to inactive position
        LeanTween.move(gameObject, new Vector3(0, -10, 0), 0.5f).setEase(LeanTweenType.easeInQuart);
    }
}
