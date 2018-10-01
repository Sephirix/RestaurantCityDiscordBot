using Discord.Commands;
using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantCityDiscordBot.Core.Commands
{
    public class HelloWorld : ModuleBase<SocketCommandContext>
    {
        [Command("hello"), Alias("helloworld", "world"), Summary("Hello world command")]
        public async Task Sjustein()
        {
            await Context.Channel.SendMessageAsync("Hello worldtestset");
        }
    }
}
