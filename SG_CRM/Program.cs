using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using SG_Third_Iteration;

class Program
{
    static ITelegramBotClient botClient;

    static async Task Main()
    {
        botClient = new TelegramBotClient("6751527239:AAHbAnK9VSq_05nlVLFInitWb0fdWdWxJDI");

        using var cts = new CancellationTokenSource();

        // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
        ReceiverOptions receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { } // receive all update types
        };
        botClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken: cts.Token);

        var me = await botClient.GetMeAsync();
        Console.WriteLine($"Hello, I am {me.FirstName}");
        Console.ReadKey();

        // Send cancellation request to stop bot
        cts.Cancel();
    }

    static IParticipantContainer participantContainer = new PlayerMabager();


    static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Type != UpdateType.Message || update.Message.Type != MessageType.Text)
            return;

        var message = update.Message;
        Console.WriteLine($"Received a text message from {message.Chat.Id}.");

        string response = message.Text.Trim() switch
        {
            "/start_rlgl" => new RedLightGreenLight().StartGame(),
            "/start_tugofwar" => new TugOfWar().StartGame(),
            "/help" => "Type /start_rlgl to play Red Light, Green Light or /start_tugofwar to play Tug of War.",
            _ => "Unknown command. Type /help for available commands."
        };

        var messageParts = message.Text.Split(' ');
        switch (messageParts[0])
        {
            case "/addparticipant":
                if (messageParts.Length >= 2)
                {
                    var participantName = string.Join(" ", messageParts.Skip(1));
                    Random rand = new Random();
                    var participantAge = rand.Next(18, 90);
                    participantContainer.AddParticipant(new Participant(participantName, participantAge));
                    response = $"Participant {participantName} added.";
                }
                else
                {
                    response = "Usage: /addparticipant [name]";
                }
                break;

            case "/removeparticipant":
                if (messageParts.Length >= 2)
                {
                    var participantName = string.Join(" ", messageParts.Skip(1));
                    var participant = participantContainer.GetParticipantByName(participantName);
                    if (participant != null)
                    {
                        participantContainer.RemoveParticipant(participant);
                        response = $"Participant {participantName} removed.";
                    }
                    else
                    {
                        response = $"Participant {participantName} not found.";
                    }
                }
                else
                {
                    response = "Usage: /removeparticipant [name]";
                }
                break;
            case "/start_tugofwar":
                new TugOfWar().StartGame();
                break;
            case "/start_rlgl":
                new RedLightGreenLight().StartGame();
                break;
            case "/help":
                response = "Here are the commands you can use:\n" +
                       "/start - Welcome message\n" +
                       "/addparticipant [name] - Add a new participant\n" +
                       "/removeparticipant [name] - Remove an existing participant\n" +
                       "/help - Show this help information\n" +
                       "\n" +
                       "Just type a command and I'll do the rest!";
                break;
                break;
            default:
                response = "Unknown command. Type /help for available commands.";
                break;
        }

        await botClient.SendTextMessageAsync(
            chatId: message.Chat,
            text: response,
            cancellationToken: cancellationToken);
    }



    static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };
        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }

}
