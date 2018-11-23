using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace MyCodeDiscordBot
{
    class Program
    {
        private DiscordSocketClient client;
        private CommandService Commands;
        static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });


            Commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });

            client.MessageReceived += client_MessageReceived;
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly());

            client.Ready += client_ready;
            client.Log += client_log;

            await client.LoginAsync(TokenType.Bot, "NTE1MjIzNTA4NDUyNzY5ODEy.DtkdZQ.GOvTfQjUEm2ImcoItmXQ5LfKOnQ");
            await client.StartAsync();

            await Task.Delay(-1);

        }

        private async Task client_log(LogMessage Message)
        {
            Console.WriteLine($"{DateTime.Now} at {Message.Source}] {Message.Message}");
        }

        private async Task client_ready()
        {
            await client.SetGameAsync("Yukino - Tutorial!", "", StreamType.NotStreaming);
        }

        private async Task client_MessageReceived(SocketMessage MessageParam)
        {
            //Config commands
            var Message = MessageParam as SocketUserMessage;
            var Context = new SocketCommandContext(client, Message);

            if (Context.Message == null || Context.Message.Content == "") return;
            if (Context.User.IsBot) return;

            int ArgPos = 0;
            if (!(Message.HasStringPrefix("a!", ref ArgPos) || Message.HasMentionPrefix(client.CurrentUser, ref ArgPos))) return;

            var Result = await Commands.ExecuteAsync(Context, ArgPos);
            if (!Result.IsSuccess)
                Console.WriteLine($"{DateTime.Now} at  Commands] Something went wrong with executing a command. Text: {Context.Message.Content} | Error: {Result.ErrorReason}");
        }
    }
}
