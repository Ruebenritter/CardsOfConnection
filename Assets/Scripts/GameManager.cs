using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System.Threading;

public class GameManager : MonoBehaviour
{
    public Opponent currentDate;
    public GameObject promptPrefab;
    public GameObject answerPrefab;

    //Anchors for ease of UI element positioning
    public Transform promptAnchor;
    public Transform handAnchor;
    public Transform opponentAnchor;
    
    private int promptIndex = 0;
    private DialogModel currentDialogScript;

   
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
        currentDialogScript = LoadDialog(currentDate.dateDialog);
    }

    private void StartNextRound(){

        DisplayPrompt(currentDialogScript.prompts[promptIndex]);

        //Select 5 replies
        var answers = currentDialogScript.prompts[promptIndex].answers;
        DisplayHand(answers);
    }

    private void DisplayPrompt(PromptModel prompt){
        var promptGO = Instantiate(promptPrefab, promptAnchor.transform);
        var promptGOContent = promptGO.GetComponent<TextMeshPro>();
        promptGOContent.text = prompt.content;
    }

    private void DisplayHand(List<AnswerModel> answers){
       // get card children from handAnchor
       var handCards = handAnchor.GetComponentsInChildren<AnswerCard>();

       //set card content
         for (int i = 0; i < handCards.Length; i++){
            if(i>= 3){
                handCards[i].FlipToBack();
            }
            handCards[i].SetContent(answers[i].content, answers[i].score);
         }
    }

    public DialogModel LoadDialog(TextAsset dialog){
        string json = dialog.text;
        return JsonUtility.FromJson<DialogModel>(json);
    }

    public void HandleCardClick(AnswerCard card){
        // print card text and score 
        Debug.Log(card.text + " " + card.baddieValue);
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
}
