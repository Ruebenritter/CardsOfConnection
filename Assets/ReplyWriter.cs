using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ReplyWriter : MonoBehaviour
{
    private TextMeshPro textField;
    private string textToWrite;
    private float timeBetweenCharacters = 0.05f;
    private int characterIndex;

    private bool isWriting;
    private float timer;
    public float maxTimeToWaitInSeconds = 10.0f;

    public float uptimeForReply = 4.0f;

    private TaskCompletionSource<bool> tcs;
    // Start is called before the first frame update

    void Awake(){
        //get TextMeshPro component from parent gameobject
        textField = GetComponent<TextMeshPro>();
        if (textField == null){
            Debug.LogError("TextMeshPro component not found");
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isWriting){
            timer -= Time.deltaTime;
            if (timer <= 0f){
                timer += timeBetweenCharacters;
                characterIndex++;
                textField.text = textToWrite.Substring(0, characterIndex);
                if (characterIndex >= textToWrite.Length){
                    isWriting = false;
                }
            }
        }
    }

    public Task<bool> WriteReply(ReplyModel reply){
        if(reply == null){
            Debug.LogError("Reply is null");
        }
        
        characterIndex = 0;
        textField.text = "";
        textToWrite = reply.content;


        isWriting = true;
        tcs = new TaskCompletionSource<bool>();
        StartCoroutine(waiter());
        return tcs.Task;
        
    }

    private IEnumerator waiter(){
        float waitedTime = 0;
        while(isWriting){
            yield return new WaitForSeconds(0.1f);
            waitedTime += 0.1f;
            if (waitedTime > maxTimeToWaitInSeconds){
                tcs.SetResult(false);
                yield break;
            }
        }
        yield return new WaitForSeconds(uptimeForReply);
        tcs.SetResult(true);
        isWriting = false;
    }
}
