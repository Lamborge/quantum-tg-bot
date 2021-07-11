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

        static string[] sound_file_id_stalker = {
            "CQACAgIAAxkBAAEDD_1g2rVgmO_5djk_99E0qxyeEFsLkAACOw8AAsTP0Upxnw72aGofhB4E",
            "CQACAgIAAxkBAAEDD_5g2rVo9VM0rIF54K9HAAH7UyoI-JgAAjwPAALEz9FKk1JeRvJP5qEeBA",
            "CQACAgIAAxkBAAEDD_9g2rVrRU1dEvmdhSj-8wkrYE7x1wACPQ8AAsTP0Urxn_-05KmVah4E",
            "CQACAgIAAxkBAAEDEAABYNq1biwdWjzSA1VrNfukSU7QIxkAAj4PAALEz9FKfjmhXLojrR4eBA",
            "CQACAgIAAxkBAAEDEAFg2rVxmitO1b78ttxq3vBf4BZtzgACPw8AAsTP0UoZEOSe-3Tnvh4E",
            "CQACAgIAAxkBAAEDEAJg2rV0_Dtf3zlDbp1O4s6GRAmPwQACQA8AAsTP0Urw86HHV1W5pR4E",
            "CQACAgIAAxkBAAEDEANg2rV3zH0GrtR9BWoeMLSR8_jGcwACQQ8AAsTP0Uq0V6RYB2xbVB4E",
            "CQACAgIAAxkBAAEDEARg2rV69A5CUJuujmSiuZduob9T2gACQg8AAsTP0UqtrxZgOn_klR4E"
            };

        static string[] gachi_sound = {
            "CQACAgIAAxkBAAEDHf1g4t-KN3cjmOiLD1ATQiCMXrufSQACqhAAAnfSGUtiy-i21-8_th4E",
            "CQACAgIAAxkBAAEDHf5g4t-OtP-oClmxCji1cdp6I5vCQAACqxAAAnfSGUvJLvRTShVINR4E",
            "CQACAgIAAxkBAAEDHf9g4t-RgkfkdS7fkflFJFyfUaliLgACrBAAAnfSGUvQHkn_OLTxfx4E",
            "CQACAgIAAxkBAAEDHgABYOLflDZIUOASevokwfTYsm-VaV8AAq0QAAJ30hlLvhYBsBi_VDoeBA",
            "CQACAgIAAxkBAAEDHgFg4t-X2KbcqtRD_BcaMlELl8yw7QACrhAAAnfSGUs8npqSGBI4RB4E",
            "CQACAgIAAxkBAAEDHgJg4t-aDI_OIbdbDWlTPUcjpnac0gACrxAAAnfSGUud2GbZxz7QJh4E",
            "CQACAgIAAxkBAAEDHgNg4t-diJVz0c8ytrRxCLepWNUO1wACsBAAAnfSGUsz6o-WkqvDEB4E",
            "CQACAgIAAxkBAAEDHgRg4t-g3jau-kzRYlD2Yier7o94SwACsRAAAnfSGUv44zeiztoSNx4E",
            "CQACAgIAAxkBAAEDHgVg4t-jLb5sjvzauZQ12S6brwvK6AACshAAAnfSGUv1vPmIyX9dEx4E",
            "CQACAgIAAxkBAAEDHgZg4t-lq_j-8cwh_CkhjGEMbpK2rAACsxAAAnfSGUs8RQQB85KDVB4E",
            "CQACAgIAAxkBAAEDHgdg4t-p_cZA3E1pIhOZUGCtr61smgACtBAAAnfSGUvLSyprlSISYh4E"
            };

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
                string log = $"Пришло сообщение с текстом: {msg.Text}\nОтправитель: {msg.From.Username}({msg.Chat.Id}) \nВремя: {DateTime.Now}\nЧат: {msg.Chat.Title}";

                Console.WriteLine(log);
                await client.SendTextMessageAsync(
                            chatId: 662959105,
                            text: log
                        );

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
                        var video = await client.SendVideoAsync(
                            chatId: msg.Chat.Id,
                            replyToMessageId: msg.MessageId,
                            video: "BAACAgIAAxkBAAEDECNg2rf5KM5oeea1_TEg45LSBo55DQACRg8AAsTP0UoJUTgBIQ7GqR4E",
                            supportsStreaming: true
                        );
                    break;

                    case "/reverse@quantumlamborge_bot":
                    case "/reverse":
                        try
                        {
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
                                string res = "";
                                for (var i = 1; i < args.Length; i++)
                                {
                                    res+=args[i] + " ";
                                }
                                var reverse = await client.SendTextMessageAsync(
                                msg.Chat.Id,
                                text: ReverseText(res),
                                replyToMessageId: msg.MessageId     
                                );
                            }
                            
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
                                text: $"*ID:* {msg.Chat.Id}\n*Invite link:* {msg.Chat.InviteLink}\n*Username:* {msg.Chat.Username}\n*Title:* {msg.Chat.Title}\n*Bio:* {msg.Chat.Description}\n*Pinned message:* {msg.Chat.PinnedMessage}\n*Slow mode delay:* {msg.Chat.SlowModeDelay}\n*Location:* {msg.Chat.Location}"
                            );
                        }
                        catch 
                        {
                            ReturnError(e);
                        }
                    break;

                    case "/anekdot@quantumlamborge_bot":
                    case "/anekdot":                
                        msg = await client.SendAudioAsync(
                            chatId: e.Message.Chat,
                            audio: sound_file_id_stalker[rnd.Next(8)],
                            title: "Quantum - Анекдот",
                            caption: "Внимение анекдот!",
                            replyToMessageId: msg.MessageId                        
                        );          
                    break;

                    case "/gachi@quantumlamborge_bot":
                    case "/gachi":
                        msg = await client.SendAudioAsync(
                            chatId: e.Message.Chat,
                            audio: gachi_sound[rnd.Next(gachi_sound.Length)],
                            title: "Gachi - Sound",
                            replyToMessageId: msg.MessageId                        
                        );
                    break;

                    case "/doom@quantumlamborge_bot":
                    case "/doom":
                        msg = await client.SendAudioAsync(
                        chatId: e.Message.Chat,
                        audio: "CQACAgIAAxkBAAEDECBg2rYpaPYRsgo59qVK7K8XOaPIyQACRA8AAsTP0Up8PPGJpGwgEh4E",
                        title: "Rip and Tear - Until it is Done",
                        replyToMessageId: msg.MessageId                        
                        );
                    break;

                    

                    case "/randomfile@quantumlamborge_bot":
                    case "/randomfile":

                        using (FileStream fstream = new FileStream($"./file/file.txt", FileMode.OpenOrCreate))
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

                        using (var stream = System.IO.File.OpenRead($"./file/file.txt")) {
                            msg = await client.SendDocumentAsync(
                            chatId: e.Message.Chat,
                            document: stream,                            
                            replyToMessageId: msg.MessageId                        
                            );
                        }
                        File.Delete($"./file/file.txt");

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

                    case "/leonidussaks.sh@quantumlamborge_bot":
                    case "/leonidussaks.sh":
                        var ping = await client.SendTextMessageAsync(
                            chatId: msg.Chat.Id,
                            text: "чё",
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

                        else if (File.Exists($"./ascii/{args[1]}.txt"))
                        {
                            string textFromFile = "";
                            using (FileStream fstream = File.OpenRead($"./ascii/{args[1]}.txt"))
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

                    case "/sendmsg":
                    if (msg.Chat.Id == 662959105)
                    {
                        try
                        {
                            string res = "";
                                for (var i = 2; i < args.Length; i++)
                                {
                                    res+=args[i] + " ";
                                }

                            var send = await client.SendTextMessageAsync(
                                chatId: args[1],
                                text: $"*Отправитель:* Lamborge \n*Сообщение:* \n{res}",
                                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown
                            );
                        }
                        catch
                        {
                            var send = await client.SendTextMessageAsync(
                                chatId: msg.Chat.Id,
                                text: "loss argument",
                                replyToMessageId: msg.MessageId
                            );
                        }
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
