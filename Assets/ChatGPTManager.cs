using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI; 

public class ChatGPTManager : MonoBehaviour
{

    private OpenAIApi openAI = new OpenAIApi("enter-key"); //having to hardcode. Not the best but eh
    
    //according to the api, it scans for the auth file...which it's not finding /:
    private List<ChatMessage> messages = new List<ChatMessage>(); //list of GPT Messages


    public async void AskChatGPT(string newText)
    { 
        //is the error because of type?
        
        //creating new messages
        ChatMessage newMessage = new ChatMessage();
        newMessage.Content = newText;
        newMessage.Role = "user";
        messages.Add(newMessage); //adding the message to the List to send to ChatGPT

        //making request to OpenAI with messages List
        CreateChatCompletionRequest request = new CreateChatCompletionRequest(); // saying this class doesn't exist
        request.Messages = messages;
        request.Model = "gpt-3.5-turbo";

        //contrary to Valum, this is CreateChatCompletion, not CreateChatCompletionRequest?
        var response = await openAI.CreateChatCompletion(request); //"await" = waiting to get GPT response b4 continuing 

        //checking the GPT responses
        if(response.Choices != null && response.Choices.Count > 0) //checking it exists
        {
            var chatResponse = response.Choices[0].Message; //adding first choice of response to message list
            messages.Add(chatResponse);
            
            Debug.Log(chatResponse.Content); //debugging to console
        }
    }
   
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
