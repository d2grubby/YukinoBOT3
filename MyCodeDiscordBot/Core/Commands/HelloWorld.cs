using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace MyCodeDiscordBot.Core.Commands
{
    public class HelloWorld : ModuleBase<SocketCommandContext>
    {
        [Command("hello"), Alias("helloworld", "world"), Summary("Hello world command")]
        public async Task Sjustein()
        {
            await Context.Channel.SendMessageAsync("Hello World");
        }

        [Command("embed"), Summary("embed command")]
        public async Task Embed([Remainder] string Input = "None")
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithAuthor("Test embed", Context.User.GetAvatarUrl());
            Embed.WithColor(40, 200, 174);
            Embed.WithFooter("The footer of the embed", Context.Guild.Owner.GetAvatarUrl());
            Embed.WithDescription("Yukinoooo.\n" +
                                "[This is favor website](gooogle.com)");
            Embed.AddInlineField("User Input:", Input);
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }
    }
}
