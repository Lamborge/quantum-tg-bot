using System;
using System.IO;
using System.Drawing;
using Telegram.Bot;
using Telegram.Bot.Args;
using System.Reflection;
using System.Diagnostics;
using Telegram.Bot.Types.InputFiles;
using Microsoft.Extensions.Configuration;

namespace bot_tg_sharp
{
    class Program
    {

        private static string token{get; set;} = "1832829208:AAFkL4CwCprJoWWPEby8P1MODu8shUKuqbE";
        private static TelegramBotClient client;
        private static Random rnd = new Random();

        static void Main(string[] args)
        {
            client = new TelegramBotClient(token);
            client.StartReceiving();
            client.OnMessage += OnMessageHandler;
            Console.ReadLine();
            client.StopReceiving();
        }

        private static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            var msg = e.Message;
            if(msg.Text != null){
            char[] chr = msg.Text.ToCharArray();
            if (chr[0] == '/' /*msg.Text != null*/)
            {
                Console.WriteLine($"Пришло сообщение с текстом: {msg.Text}\nОтправитель: {msg.From.Username} \nВремя: {DateTime.Now}\nЧат: {msg.Chat.Title}");
                string[] args = msg.Text.ToLower().Split(' ');
                
                switch (args[0])
                {
                    case "/start@quantumlamborge_bot":
                    case "/start":
                        await client.SendTextMessageAsync(
                            msg.Chat.Id,
                            text: "Ahoj!"
                        );
                    break;

                    case "/sendshmil@quantumlamborge_bot":
                    case "/sendshmil":
                        var stick = await client.SendStickerAsync(
                            chatId: msg.Chat.Id,
                            sticker: "CAACAgIAAxkBAAEBfcJg1ylpOjwRGIb9_3uCM9b7ZGjIhgAC4QADAfSDJ2TlfRjq7pOzIAQ",
                            replyToMessageId: msg.MessageId
                        );
                    break;

                    case "/shmil@quantumlamborge_bot":
                    case "/shmil":
                        string mypath= $"{Directory.GetCurrentDirectory()}/video/shmil.mp4";
                        using (var fileStream = new FileStream(mypath, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            var video = await client.SendVideoAsync(
                                chatId: msg.Chat.Id,
                                replyToMessageId: msg.MessageId,
                                video: new InputOnlineFile(fileStream),
                                supportsStreaming: true

                            );
                        }
                    break;

                    case "/reverse@quantumlamborge_bot":
                    case "/reverse":
                        try
                        {
                            var reverse = await client.SendTextMessageAsync(
                                msg.Chat.Id,
                                text: ReverseText(msg.ReplyToMessage.Text),
                                replyToMessageId: msg.MessageId     
                            );
                        }
                        catch
                        {
                            ReturnError(e);
                        }
                    break;

                    case "/getuserinfo@quantumlamborge_bot":
                    case "/getuserinfo":
                    try
                    {
                        var getuserinfo = await client.SendTextMessageAsync(
                            msg.Chat.Id,
                            text: $"ID: *{msg.ReplyToMessage.From.Id}*\nBot: *{msg.ReplyToMessage.From.IsBot}*\nFirst name: *{msg.ReplyToMessage.From.FirstName}*\nLast name: *{msg.ReplyToMessage.From.LastName}*\nUsername: *{msg.ReplyToMessage.From.Username}*",
                            replyToMessageId: msg.MessageId,
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown
                        );
                    }
                    catch
                    {
                        ReturnError(e);
                    }                        
                    break;

                    case "/getchatinfo@quantumlamborge_bot":
                    case "/getchatinfo":
                        try
                        {
                            var getchatinfo = await client.SendTextMessageAsync(
                                msg.Chat.Id,
                                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                                replyToMessageId: msg.MessageId,
                                text: $"*ID:* {msg.ReplyToMessage.Chat.Id}\n*Invite link:* {msg.ReplyToMessage.Chat.InviteLink}\n*Username:* {msg.ReplyToMessage.Chat.Username}\n*Title:* {msg.ReplyToMessage.Chat.Title}\n*Bio:* {msg.ReplyToMessage.Chat.Description}\n*Pinned message:* {msg.ReplyToMessage.Chat.PinnedMessage}\n*Slow mode delay:* {msg.ReplyToMessage.Chat.SlowModeDelay}\n*Location:* {msg.ReplyToMessage.Chat.Location}"
                            );
                        }
                        catch 
                        {
                            ReturnError(e);
                        }
                    break;

                    case "/anekdot@quantumlamborge_bot":
                    case "/anekdot":
                        using (var stream = System.IO.File.OpenRead($"{Directory.GetCurrentDirectory()}/audio/stalker/{rnd.Next(9)}.mp3")) {
                        msg = await client.SendAudioAsync(
                        chatId: e.Message.Chat,
                        audio: stream,
                        title: "Quantum - Анекдот",
                        caption: "Внимение анекдот!",
                        replyToMessageId: msg.MessageId                        
                        );
                        }
                    break;

                    case "/doom@quantumlamborge_bot":
                    case "/doom":
                        using (var stream = System.IO.File.OpenRead($"{Directory.GetCurrentDirectory()}/audio/doom/doom_eternal.mp3")) {
                        msg = await client.SendAudioAsync(
                        chatId: e.Message.Chat,
                        audio: stream,
                        title: "Rip and Tear - Until it is Done",
                        replyToMessageId: msg.MessageId                        
                        );
                        }
                    break;

                    case "/randomfile@quantumlamborge_bot":
                    case "/randomfile":

                        using (FileStream fstream = new FileStream($"{Directory.GetCurrentDirectory()}/file/file.txt", FileMode.OpenOrCreate))
                        {
                            // преобразуем строку в байты
                            byte[] byte_text = new byte[512];
                            for (var i = 0; i < byte_text.Length; i++)
                            {
                                byte_text[i] = Convert.ToByte(rnd.Next(256));
                            }
                            // запись массива байтов в файл
                            fstream.Write(byte_text, 0, byte_text.Length);
                        }

                        using (var stream = System.IO.File.OpenRead($"{Directory.GetCurrentDirectory()}/file/file.txt")) {
                            msg = await client.SendDocumentAsync(
                            chatId: e.Message.Chat,
                            document: stream,                            
                            replyToMessageId: msg.MessageId                        
                            );
                        }
                        File.Delete($"{Directory.GetCurrentDirectory()}/file/file.txt");

                    break;    

                    case "/ping@quantumlamborge_bot":
                    case "/ping":
                        var ping = await client.SendTextMessageAsync(
                            chatId: msg.Chat.Id,
                            text: "*Pong!*",
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                            replyToMessageId: msg.MessageId
                        );
                    break; 

                    case "/asciipic@quantumlamborge_bot":
                    case "/asciipic":
                        if (args.Length == 1)
                        {
                            var asciipic = await client.SendTextMessageAsync(
                                    chatId: msg.Chat.Id,
                                    text: "*Не указан рисунок*",
                                    replyToMessageId: msg.MessageId,
                                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown
                                );
                        }

                        else if (File.Exists($"{Directory.GetCurrentDirectory()}/ascii/{args[1]}.txt"))
                        {
                            string textFromFile = "";
                            using (FileStream fstream = File.OpenRead($"{Directory.GetCurrentDirectory()}/ascii/{args[1]}.txt"))
                            {
                                // преобразуем строку в байты
                                byte[] array = new byte[fstream.Length];
                                // считываем данные
                                fstream.Read(array, 0, array.Length);
                                // декодируем байты в строку
                                textFromFile = System.Text.Encoding.Default.GetString(array);
                                
                            }
                            var asciipic = await client.SendTextMessageAsync(
                                    chatId: msg.Chat.Id,
                                    text: $"<code>{textFromFile}</code>",
                                    replyToMessageId: msg.MessageId,
                                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Html
                                );
                        }
                        
                        else
                        {
                            var asciipic = await client.SendTextMessageAsync(
                                    chatId: msg.Chat.Id,
                                    text: "Такого рисунка нет",
                                    replyToMessageId: msg.MessageId
                                );
                        }
                        
                    break;


                }
                
            }}
        }

        static string ReverseText(string text){
            char[] chr1 = text.ToCharArray();
            char[] chr2 = new char[chr1.Length];
            int j = 0;
            for (var i = chr1.Length-1; i >= 0; i--)
            {                
                chr2[j] = chr1[i];
                j++;
            }

            return String.Join("", chr2);
        }

        static void ReturnError(MessageEventArgs e){
            var msg = e.Message;
            var returnerror = client.SendTextMessageAsync(
            msg.Chat.Id,
            text: "Что-то пошло не так.\n(Возможно нужно написать сообщение в виде ответа)",
            replyToMessageId: msg.MessageId
                            );
        }

        static string RunBash(string cmd){
            System.Diagnostics.Process proc = new System.Diagnostics.Process(); 
            proc.StartInfo.FileName = "/bin/bash";
            proc.StartInfo.Arguments = cmd; 
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true; 
            proc.Start();
            
            return proc.StandardOutput.ReadToEnd();;
        }

    }
}
