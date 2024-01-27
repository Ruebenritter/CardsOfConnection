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
        StartCoroutine(waiter());
        

    }

    IEnumerator waiter(){
        yield return new WaitForSeconds(3);
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
        var answers = currentDialogScript.prompts[promptIndex].answerList;
    }

    private void DisplayPrompt(PromptModel prompt){
        var promptGO = Instantiate(promptPrefab, promptAnchor.transform);
        var promptGOContent = promptGO.GetComponent<TextMeshPro>();
        promptGOContent.text = prompt.content;
    }

    public DialogModel LoadDialog(TextAsset dialog){
        string json = dialog.text;
        return JsonUtility.FromJson<DialogModel>(json);
    }
}
