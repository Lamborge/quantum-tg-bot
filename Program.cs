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
        private static string token{get; set;}
        private static TelegramBotClient client;
        private static Random rnd = new Random();
        static string[] categories_ascii = {"doom","jorjy","leonid","popug","distro"};

        static void Main(string[] args)
        {

            if (File.Exists($"./.token"))
            {
                // чтение из файла
                using (FileStream fstream = File.OpenRead($"./.token"))
                {
                    // преобразуем строку в байты
                    byte[] array = new byte[fstream.Length];
                    // считываем данные
                    fstream.Read(array, 0, array.Length);
                    // декодируем байты в строку
                    string textFromFile = System.Text.Encoding.Default.GetString(array);
                    token = textFromFile;
                }
            }
            else
            {
                Console.WriteLine("Введите токен бота: ");
                string local_token = Console.ReadLine();
                // запись в файл
                using (FileStream fstream = new FileStream($"./.token", FileMode.OpenOrCreate))
                {
                    // преобразуем строку в байты
                    byte[] array = System.Text.Encoding.Default.GetBytes(local_token);
                    // запись массива байтов в файл
                    fstream.Write(array, 0, array.Length);
                    Console.WriteLine("Токен записан в файл");
                }

                token = local_token;
            }

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

                    /*case "/shmil@quantumlamborge_bot":
                    case "/shmil":
                    using (var stream = System.IO.File.OpenRead($"./temp/video/video.mp4")) {
                        var video = await client.SendVideoAsync(
                            chatId: msg.Chat.Id,
                            replyToMessageId: msg.MessageId,
                            video: stream,
                            supportsStreaming: true
                        );}
                    break;*/

                    case "/shmil@quantumlamborge_bot":
                    case "/shmil":
                    using (var stream = System.IO.File.OpenRead($"./video/shmil.mp4")){
                        var video = await client.SendVideoAsync(
                            chatId: msg.Chat.Id,
                            replyToMessageId: msg.MessageId,
                            video: stream,
                            supportsStreaming: true
                        );}
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
                    using (var stream = System.IO.File.OpenRead($"./sound/stalker/{rnd.Next(8)}.mp3")){           
                        var anekdot = await client.SendAudioAsync(
                            chatId: e.Message.Chat,
                            audio: stream,
                            title: "Quantum - Анекдот",
                            caption: "Внимение анекдот!",
                            replyToMessageId: msg.MessageId                        
                        );          
                    break;}

                    case "/gachi@quantumlamborge_bot":
                    case "/gachi":
                    using (var stream = System.IO.File.OpenRead($"./sound/gachi/{rnd.Next(11)}.mp3")){  
                        var gachi = await client.SendAudioAsync(
                            chatId: e.Message.Chat,
                            audio: stream,
                            title: "GachiRemix - Quantum",
                            replyToMessageId: msg.MessageId                        
                        );
                    break;}

                    case "/doom@quantumlamborge_bot":
                    case "/doom":
                    using (var stream = System.IO.File.OpenRead("./sound/doom/ripandtear.mp3")){  
                        var doom = await client.SendAudioAsync(
                        chatId: e.Message.Chat,
                        audio: stream,
                        title: "Rip and Tear - Until it is Done",
                        replyToMessageId: msg.MessageId                        
                        );
                    break;}

                    

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
                            var randomfile = await client.SendDocumentAsync(
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

                    case "/leonidussaks@quantumlamborge_bot":
                    case "/leonidussaks":
                        var leonid = await client.SendTextMessageAsync(
                            chatId: msg.Chat.Id,
                            text: "чё",
                            replyToMessageId: msg.MessageId
                        );
                    break;

                    case "/gentoo@quantumlamborge_bot":
                    case "/gentoo":
                        var gentoo = await client.SendTextMessageAsync(
                            chatId: msg.Chat.Id,
                            text: "install gentoo lol its best distro itw",
                            replyToMessageId: msg.MessageId
                        );
                    break;


                    
                    case "/asciipic@quantumlamborge_bot":
                    case "/asciipic":
                        if (args.Length == 1)
                        {
                            var asciipic = await client.SendTextMessageAsync(
                                    chatId: msg.Chat.Id,
                                    text: "*Не указан рисунок*\n"+
                                    "Используйте аргумент --list что бы посмотреть список категорий",
                                    replyToMessageId: msg.MessageId,
                                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown
                                );
                        }

                        else if(args.Length == 2)
                        {
                            char[] check = args[1].ToCharArray();
                            foreach (var item in check)
                            {
                                if (item == '.')
                                {
                                    var asciipic = await client.SendTextMessageAsync(
                                    chatId: msg.Chat.Id,
                                    text: "Хорошая попытка",
                                    replyToMessageId: msg.MessageId,
                                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown
                                );
                                }
                            }
                        }

                        else if(args.Length >= 2 && args[1] == "--list")
                        {
                            string res = "";

                            foreach (var item in categories_ascii)
                            {
                                res += item + "/\n";
                            }

                            var asciipic = await client.SendTextMessageAsync(
                                chatId: msg.Chat.Id,
                                text: res        
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
                                    text: "Такого рисунка нет \n"+
                                    "Используйте аргумент --list что бы посмотреть список категорий",
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
