using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

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

    static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        // Only process Message updates
        if (update.Type != UpdateType.Message)
            return;
        // Only process text messages
        var message = update.Message;
        if (message?.Type != MessageType.Text)
            return;

        Console.WriteLine($"Received a text message from {message.Chat.Id}.");

        await botClient.SendTextMessageAsync(
            chatId: message.Chat,
            text: "You said:\n" + message.Text,
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
