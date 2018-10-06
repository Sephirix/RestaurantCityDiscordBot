using System;
using System.Reflection;
using System.Threading.Tasks;
using System.IO;

#region
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using RestaurantCityDiscordBot.Core.Commands;
#endregion

namespace RestaurantCityDiscordBot
{
    class Program
    {
        private DiscordSocketClient client;
        private CommandService commands;
        

        static void Main(string[] args) =>
            new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug,
                
            });

            commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });

            client.MessageReceived += Client_MessageReceived;
            client.Ready += Client_Ready;
            client.Log += Client_Log;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());


            string Token ="";
            string TokenP= @".\Data\Token.txt";
            Console.WriteLine("This is the directory"+TokenP);
          
            using(var ReadToken = new StreamReader(TokenP))
            {
                Token = ReadToken.ReadToEnd();
            }
                await client.LoginAsync(TokenType.Bot, Token);
                await client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task Client_Ready()
        {
            await client.SetGameAsync("AllRound Bot - Tutorial", "", StreamType.NotStreaming);
            Console.WriteLine("I'm up");
            
            if (client.GetChannel(497203012536631322) is IMessageChannel channel)
                await channel.SendMessageAsync("Hi, I'm up!");
            

        
        }

     

        private async Task Client_MessageReceived(SocketMessage MessageParam)
        {
            var Message = MessageParam as SocketUserMessage;
            var Context = new SocketCommandContext(client, Message);
        
            if(Context.Message == null || Context.Message.Content == "") return;
            if (Context.User.IsBot) return;


            int ArgPos = 0;
           

               
                if (!(Message.HasStringPrefix("$$", ref ArgPos) || Message.HasMentionPrefix(client.CurrentUser, ref ArgPos)))
                {

                    if (Message.Channel.Id == 495567892667170849)
                    {
                        await Message.DeleteAsync();
                    }
                    return;


                }

            if (Message.Channel.Id != 495567892667170849) return;
            var Result = await commands.ExecuteAsync(Context, ArgPos);
            if (!Result.IsSuccess)
            {
                Console.WriteLine($"[{DateTime.Now} at Commands] Something went wrong with the executing a command. " +
                    $"Text: {Context.Message.Content} | Error: {Result.ErrorReason}");
                
            }
        }

        private async Task Client_Log(LogMessage log)
        {
            Console.WriteLine($"{DateTime.Now} at {log.Source} {log.Message}");
        }

       
    }
}
