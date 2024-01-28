using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System.Threading;
using System;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    public Opponent currentDate;
    public GameObject promptPrefab;

    public GameObject replyPrefab;
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
    public GameObject reactionPrefab;
    public Opponent opponent;

    public AudioSource laugh;
    public AudioSource huh_x;

   void Awake(){
     progressBars = GameObject.Find("ProgressBars").GetComponent<ProgressBars>();
   }

    void Start()
    {
        InitGame();
        StartNextRound();
    }
   
    void Update()
    {
        
    }

    void FixedUpdate(){
        if(currentDate != null && currentDate.AttractionFailed()){
            Debug.Log("Game Over");
            SceneManager.LoadScene("GameOver");
        }

        if(currentDate != null && currentDate.AttractionSucceeded()){
            Debug.Log("You Win");
            SceneManager.LoadScene("WinningScreen");
        }
        if(progressBars.TimerHasRunOut()){
            SceneManager.LoadScene("GameOver");
        }
        if(promptIndex >= currentDialogScript.prompts.Count){
            Debug.Log("End of dialog");
            SceneManager.LoadScene("GameOver");
        }


    }

    private void InitGame(){
//Get opponent selection
       
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
    public async Task HandleCardClick(AnswerCard card)
    {
        // hide thought bubble
        thoughtBubble.GetComponent<ThoughtBubble>().MoveToRestingPos();
        card.SetSelectable(false);
        // dropdown all other cards
        var handCards = handAnchor.GetComponentsInChildren<AnswerCard>();
        foreach (var handCard in handCards)
        {
            if (handCard != card)
            {
                handCard.DropDown();
            }
        }
        // progressBars.AddAttractoin((float)(card.baddieValue * 0.1));
        currentDate.attractionLevel += (float)(card.attractionChange);
        currentDate.AddToEmotion(card.attractionChange);

        if (card.attractionChange == 2)
        {
            // play positive reaction coroutine
            var positiveReactionTask = PositiveReaction();
            await positiveReactionTask;
        }
        else if (card.attractionChange == -2)
        {
            // play negative reaction coroutine
            var negativeReactionTask = NegativeReaction();
            await negativeReactionTask;
        }

        // hide promptGo
        promptGO.SetActive(false);

        var reply = Instantiate(replyPrefab, promptAnchor.transform);
        // move forward in view to z = -3 so it's placed over the background
        reply.transform.position = new Vector3(reply.transform.position.x + 1, reply.transform.position.y + 1, -3);
        var writer = reply.GetComponent<ReplyWriter>();
        var replyModel = GetReplyForScore(card.attractionChange);
        Debug.Log(replyModel.content);
        var success = await writer.WriteReply(replyModel);
        Destroy(reply);

        promptGO.SetActive(true);
        promptIndex++;
        StartNextRound();
    }

    private ReplyModel GetReplyForScore(int score){
        var replies = currentDialogScript.prompts[promptIndex].reactions;
        foreach (var reply in replies){
            if (reply.score == score){
                return reply;
            }
        }
        return null;
    }

    private async Task PositiveReaction()
    {
        laugh.Play();
        var reaction = Instantiate(reactionPrefab, transform);
        var reactionClass = reaction.GetComponent<Reaction>();
        reactionClass.SetPositiveReaction();
        await Task.Delay(2000);
        Destroy(reaction);
    }

    private async Task NegativeReaction(){
        huh_x.Play();
        var reaction = Instantiate(reactionPrefab, transform);
        var reactionClass = reaction.GetComponent<Reaction>();
        reactionClass.SetNegativeReaction();
        await Task.Delay(2000);
        Destroy(reaction);
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
