# Telegram Bot Template

This extension allows you to quickly create telegram bots.

For the application to work, you need to add the Telegram token to the appsettings.json:

```json
{
  "Token": "your_token"
}
```

## How to install template

To install a template for your device via dotnet:

1. Go to the directory with the desired template

   ```
   cd Template.Net.Microservice/src    
   ```

Perform the installation

```
dotnet new --install .
```

You can also reinstall the template if it was installed earlier

```
dotnet new install --force .
```

To install a template for your device via dotnet:

1. Go to File => New Solution... => More Templates => Install Template...
2. Select folder with .sln file (MicroserviceExtension/src/MicroserviceTemplate)

## Definitions

Allows you to conveniently add services. To create your own definition:

1. Create a folder in "Definitions" folder
2. Create a .cs file in a new folder
3. Inherit the "Definition" class and override the "ConfigureServiceAsync" method
4. Inside the method, add the necessary services

It should look something like this:

```C#
public class MyDefinition : ApplicationDefinition
{
    public override void ConfigureServiceAstnc(IDefinitionServiceContext context)
    {
        Object myService = new();
        context.ServiceCollection.AddSingleton(myService);
    }
}
```

## Handlers

Allows processing messages by "UpdateType"

To create your own Handler:

1. Create .cs file in "Handlers" folder
2. Add the "UpdateHandler" attribute and specify the type of update
3. Implement the "IApplicationUpdateHandler" interface

It should look something like this:

```C#
[UpdateHandler(UpdateType.Message)]
public class MessageHandler : IApplicationUpdateHandler
{
    public async Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var message = update.Message;
        if(message is not null)
        {
            await botClient.SendTextMessageAsync(update.Message.Chat.Id, message);
        }
    }
}
```

## Commands

Allows you to create your own commands executed by the user

To create your own Handler:
1. Create a folder in "Commands" folder
2. Create a .cs file in a new folder
3. Add the "Command" attribute and specify the command name
4. Implement the "ICommandHandler" interface

It should look something like this:

```C#
[Command("/mycommand")]
public class MyCommand : ICommandHandler
{
    public async Task HandleAsync(CommandContext context)
    {
        await botClient.SendTextMessageAsync(conste.Update.Message.Chat.Id, "my command executed!");
    }
}
```