using System;
using System.Reflection;
using System.Threading.Tasks;
using System.IO;

#region
using Discord;
using Discord.Commands;
using Discord.WebSocket;
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
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());

            client.Ready += Client_Ready;
            client.Log += Client_Log;

            string Token = "";
            using (var Stream = new FileStream((Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)).Replace(@"bin\Debug\netcoreapp2.1",@"Data\Token.txt"), FileMode.Open, FileAccess.Read))
            using(var ReadToken = new StreamReader(Stream))
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
        }

        private async Task Client_MessageReceived(SocketMessage MessageParam)
        {
            var Message = MessageParam as SocketUserMessage;
            var Context = new SocketCommandContext(client, Message);

            if(Context.Message == null || Context.Message.Content == "") return;
            if (Context.User.IsBot) return;

            int ArgPos = 0;
            if (!(Message.HasStringPrefix("a+", ref ArgPos) || Message.HasMentionPrefix(client.CurrentUser, ref ArgPos))) return;

            var Result = await commands.ExecuteAsync(Context, ArgPos);
            if (!Result.IsSuccess)
                Console.WriteLine($"[{DateTime.Now} at Commands] Something went wrong with the executing a command. " +
                    $"Text: {Context.Message.Content} | Error: {Result.ErrorReason}");
        }

        private async Task Client_Log(LogMessage log)
        {
            Console.WriteLine($"{DateTime.Now} at {log.Source} {log.Message}");
        }

       
    }
}
