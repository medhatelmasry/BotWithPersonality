using Azure;
using Azure.AI.OpenAI;
using BotWithPersonality;

string ENDPOINT = Utils.GetConfigValue("settings:endpoint");
string KEY = Utils.GetConfigValue("settings:key");
string DEPLOYMENT_NAME = Utils.GetConfigValue("settings:deployment-name");

const string SYSTEM_MESSAGE
    = """
    You are a friendly assistant named DotNetBot. 
    You prefer to use Canadian Newfoundland English as your language and are an expert in the .NET runtime 
    and C# and F# programming languages.
    Response using Newfoundland colloquialisms and slang.
    """;

var openAiClient = new OpenAIClient(
    new Uri(ENDPOINT),
    new AzureKeyCredential(KEY)
);

var chatCompletionsOptions = new ChatCompletionsOptions
{
    DeploymentName = DEPLOYMENT_NAME, // Use DeploymentName for "model" with non-Azure clients
    Messages =
    {
        new ChatRequestSystemMessage(SYSTEM_MESSAGE),
        new ChatRequestUserMessage("Introduce yourself"),
    }
};

while (true)
{
    Console.WriteLine();
    Console.Write("DotNetBot: ");

    Response<ChatCompletions> chatCompletionsResponse = await openAiClient.GetChatCompletionsAsync(
        chatCompletionsOptions
    );

    var chatMessage = chatCompletionsResponse.Value.Choices[0].Message;
    Console.WriteLine($"[{chatMessage.Role.ToString().ToUpperInvariant()}]: {chatMessage.Content}");

    chatCompletionsOptions.Messages.Add(new ChatRequestUserMessage(chatMessage.Content));

    Console.WriteLine();

    Console.Write("Enter a message: ");
    var userMessage = Console.ReadLine();
    chatCompletionsOptions.Messages.Add(new ChatRequestUserMessage(userMessage));
}

