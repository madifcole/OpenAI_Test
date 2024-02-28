using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI; 

public class ChatGPTManager : MonoBehaviour
{

    private OpenAIApi openAI = new OpenAIApi(); //alternative to directly adding ssh key in project
    //causing an error because the name was updated on Git package's API?
    private List<ChatMessage> messages = new List<ChatMessage>(); //list of GPT Messages


    public async void AskChatGPT(string newText)
    {   

        //creating new messages
        ChatMessage newMessage = new ChatMessage();
        newMessage.Content = newText;
        newMessage.Role = "user";

        messages.Add(newMessage); //adding the message to the List to send to ChatGPT

        //making request to OpenAI with messages
        CreateChatCompletionRequest request = new CreateChatCompletionRequest();
        request.Messages = messages;
        request.Model = "gpt-3.5-turbo";

        var response = await openAI.CreateChatCompletionRequest(request); //"await" = waiting to get GPT response b4 continuing 

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
