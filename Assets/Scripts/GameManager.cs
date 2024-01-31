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
    public Transform stackAnchor;
    
    private int promptIndex = 0;
    private DialogModel currentDialogScript;
    public GameObject reactionPrefab;
    public Opponent opponent;

    private AnswerCard[] handCards;

    public AudioSource laugh;
    public AudioSource huh_x;

    private bool gameisEnding = false;

   void Awake(){
     progressBars = GameObject.Find("ProgressBars").GetComponent<ProgressBars>();
     
   }

    void Start()
    {
        InitGame();
        
    }
   
    void Update()
    {
        
    }

    void FixedUpdate(){
        if(currentDate != null && currentDate.AttractionFailed()){
           if (!gameisEnding) StartCoroutine(EndGame());
        }

        if(currentDate != null && currentDate.AttractionSucceeded()){
           if (!gameisEnding) StartCoroutine(WinGame());
        }
        if(progressBars.TimerHasRunOut()){
            if (!gameisEnding) StartCoroutine(EndGame());
        }
        if(promptIndex >= currentDialogScript.prompts.Count){
            if(!gameisEnding) StartCoroutine(EndGame());
        }


    }

    private void InitGame(){
        //Load dialog
        currentDialogScript = LoadDialog(currentDate.dateDialog);
        //Set elements to start positions
        //thoughtBubble.GetComponent<ThoughtBubble>().MoveToRestingPos();
        thoughtBubble.SetActive(false);

        //Hide cards until game start
       
    }

    private IEnumerator EndGame(){
        gameisEnding = true;
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("GameOver");
        
    }

    private IEnumerator WinGame(){
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("WinningScreen");
        gameisEnding = true;
    }

    public void StartGame(){
        StartNextRound();
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
        //Shuffle cards
        answers = ShuffleCardPool(answers);

         //Show thought bubble
        //thoughtBubble.GetComponent<ThoughtBubble>().MoveToActivePos();
        thoughtBubble.SetActive(true);

        //Get cards from hand
        handCards = handAnchor.GetComponent<HandManager>().GetCards();
        // Show cards
        handAnchor.GetComponent<HandManager>().ShowCards();
        //Move cards to stack position
        foreach (var card in handCards)
        {
            card.GetComponent<AnswerCard>().PlaceOnStack(stackAnchor.transform.position);
        }

        //Set card content
        for (int i = 0; i < handCards.Length; i++)
        {
            handCards[i].SetContent(answers[i].content, answers[i].score);
        }

        //Draw cards from stack: 3 open, 2 hidden
        for (int i = 0; i < handCards.Length; i++)
        {
           if (i < 3)
            {
                handCards[i].StartCoroutine(handCards[i].DrawFromStack());
            }
            else
            {
                handCards[i].StartCoroutine(handCards[i].DrawHiddenFromStack());
            }
        }

    }

 
    public DialogModel LoadDialog(TextAsset dialog){
        string json = dialog.text;
        return JsonUtility.FromJson<DialogModel>(json);
    }

    //User input advances round
    public async Task HandleCardClick(AnswerCard card)
    {
        // hide thought bubble
        //thoughtBubble.GetComponent<ThoughtBubble>().MoveToRestingPos();
        thoughtBubble.SetActive(false);
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
            //original code:
            //var positiveReactionTask = PositiveReaction();
            //await positiveReactionTask;
            
            var pos_react = StartCoroutine(PositiveReaction());
            
        }
        else if (card.attractionChange == -2)
        {
            // play negative reaction coroutine
            //original code:
            //var negativeReactionTask = NegativeReaction();
            //await negativeReactionTask;

            var neg_react =StartCoroutine(NegativeReaction());
            
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

    
    private IEnumerator PositiveReaction(){
        laugh.Play();
        var reaction = Instantiate(reactionPrefab, transform);
        var reactionClass = reaction.GetComponent<Reaction>();
        reactionClass.SetPositiveReaction();
        yield return new WaitForSeconds(1.5f);
        Destroy(reaction);
        
    }

    private IEnumerator NegativeReaction(){
        huh_x.Play();
        var reaction = Instantiate(reactionPrefab, transform);
        var reactionClass = reaction.GetComponent<Reaction>();
        reactionClass.SetNegativeReaction();
        yield return new WaitForSeconds(1.5f);
        Destroy(reaction);
        
    }
    


    /*
    //Original functions:
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
    */
    



    public void TrashCards(){
       // call the drop down animation on the first 3 cards and flip the last two
        //drop down the first 3 cards
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
