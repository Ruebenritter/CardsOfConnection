using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashButton : MonoBehaviour, IPointerClickHandler
{
    private GameManager gameManager;
public GameObject openBin;
public GameObject closedBin;

    public AudioSource clickSound;

    void Awake(){

    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
