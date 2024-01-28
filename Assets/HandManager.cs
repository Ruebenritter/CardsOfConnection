using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{

    public GameObject[] handCards;

    public float cardSpacing = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnCards();
        HideCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnCards(){
        for(int i = 0; i < handCards.Length; i++){
            GameObject card = handCards[i];
            Vector3 cardPos = new Vector3(transform.position.x + (i * cardSpacing), transform.position.y, -2f);
            GameObject newCard = Instantiate(card, cardPos, Quaternion.identity);
            newCard.transform.SetParent(transform);
            newCard.GetComponent<AnswerCard>().SetPlaceInHand(cardPos);
        }
    }



    public void HideCards(){
        foreach(Transform child in transform){
            child.gameObject.SetActive(false);
        }
    }

    public void ShowCards(){
        foreach(Transform child in transform){
            child.gameObject.SetActive(true);
        }
    }

    public AnswerCard[] GetCards(){
        AnswerCard[] cards = new AnswerCard[transform.childCount];
        int i = 0;
        foreach(Transform child in transform){
            cards[i] = child.gameObject.GetComponent<AnswerCard>();
            i++;
        }
        return cards;
    }
}
