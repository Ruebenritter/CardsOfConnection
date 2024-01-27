using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System.Threading;
using System;

public class GameManager : MonoBehaviour
{
    public Opponent currentDate;
    public GameObject promptPrefab;
    private GameObject promptGO;
    public GameObject answerPrefab;
    public ProgressBars progressBars;

    public GameObject thoughtBubble;

    //Anchors for ease of UI element positioning
    public Transform promptAnchor;
    public Transform handAnchor;
    public Transform opponentAnchor;
    
    private int promptIndex = 0;
    private DialogModel currentDialogScript;

    public Opponent opponent;

   
    void Start()
    {
        InitGame();
        StartNextRound();
    }
   
    void Update()
    {
        
    }

    private void InitGame(){
//Get opponent selection
        progressBars = GameObject.Find("ProgressBars").GetComponent<ProgressBars>();
        currentDialogScript = LoadDialog(currentDate.dateDialog);
        thoughtBubble.GetComponent<ThoughtBubble>().MoveToRestingPos();
    }

    private void StartNextRound(){
        if (promptIndex >= currentDialogScript.prompts.Count){
            Debug.Log("End of dialog");
            return;
        }
        DisplayPrompt(currentDialogScript.prompts[promptIndex]);

        //Select 5 replies
        var answers = currentDialogScript.prompts[promptIndex].answers;
        DisplayHand(answers);
    }

    private void DisplayPrompt(PromptModel prompt){
        if (promptGO != null){
            Destroy(promptGO);
        } //thats bad practice but we are in a hurry
        promptGO = Instantiate(promptPrefab, promptAnchor.transform);
        var promptGOContent = promptGO.GetComponent<TextMeshPro>();
        promptGOContent.text = prompt.content;
    }

    private void DisplayHand(List<AnswerModel> answers){
       // get card children from handAnchor
       var handCards = handAnchor.GetComponentsInChildren<AnswerCard>();
       //set card content: later we can expect up to ten answers and randomly assign 5 but no doubles
         //shuffle the card pool
         var shuffledAnswers = ShuffleCardPool(answers);

         for (int i = 0; i < handCards.Length; i++){
                //handCards[i].ResetPosition();
                handCards[i].PlaceOnStack();

                
                if (i >= 3){
                    //handCards[i].FlipToBack();
                    handCards[i].StartCoroutine(handCards[i].DrawHiddenFromStack());
                    handCards[i].SetContent(shuffledAnswers[i].content, shuffledAnswers[i].score);
                } else {
                    //handCards[i].FlipToReveal();
                    handCards[i].SetContent(shuffledAnswers[i].content, shuffledAnswers[i].score);
                    handCards[i].StartCoroutine(handCards[i].DrawFromStack());
                    
                }
         }
         //Show thought bubble
            thoughtBubble.GetComponent<ThoughtBubble>().MoveToActivePos();
    }

 

    public DialogModel LoadDialog(TextAsset dialog){
        string json = dialog.text;
        return JsonUtility.FromJson<DialogModel>(json);
    }

    //User input advances round
    public void HandleCardClick(AnswerCard card){
        //hide though bubble
        thoughtBubble.GetComponent<ThoughtBubble>().MoveToRestingPos();
        card.SetSelectable(false);
        // dropdown all other cards
        var handCards = handAnchor.GetComponentsInChildren<AnswerCard>();
        foreach (var handCard in handCards){
            if (handCard != card){
                handCard.DropDown();
            }
        }
        progressBars.AddAttractoin((float)(card.baddieValue * 0.1));
        opponent.addToEmotion(card.baddieValue);
        //wait for reaction animation to finish
        StartCoroutine(WaitForReaction(card));
    }

    IEnumerator WaitForReaction(AnswerCard card){
        yield return new WaitForSeconds(3.0f);
        //display reaction
        promptIndex++;
        StartNextRound();
    }

    public void TrashCards(){
        //drop down the first 3 cards
        var handCards = handAnchor.GetComponentsInChildren<AnswerCard>();
        for (int i = 0; i < 3; i++){
            handCards[i].DropDown();
        }
        //reveal the last two
        for (int i = 3; i < handCards.Length; i++){
            handCards[i].FlipToReveal();
        }
    }

    private List<AnswerModel> ShuffleCardPool(List<AnswerModel> cardPool){
        System.Random random = new();

        int n = cardPool.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            var value = cardPool[k];
            cardPool[k] = cardPool[n];
            cardPool[n] = value;
        }

        return cardPool;
    }
}
