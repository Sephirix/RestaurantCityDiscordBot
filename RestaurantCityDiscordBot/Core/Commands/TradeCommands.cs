using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using Discord.WebSocket;
using RestaurantCityDiscordBot.Core.Data;
using RestaurantCityDiscordBot.Resources.Database;
using System.Linq;
namespace RestaurantCityDiscordBot.Core.Commands
{
    public class TradeCommands : ModuleBase<SocketCommandContext>
    {

        public async Task welcome()
        {
            await Context.Channel.SendMessageAsync("I'm up guys!");
        }

        [Command("+need"), Alias("+Need"), Summary("Makes a list of ingredients user needs.")]
        public async Task addNeededIngredients(string ingredients = "")
        {
            Console.WriteLine("addNeededIngredients fired");
            if (Data.Data.ingredients2(Context.User.Id).ToString() == "")
            {
                await Context.Channel.SendMessageAsync("You haven't make a list yet.");
                Console.WriteLine("Empty");
                return;
            }
            else if (ingredients == "")
            {
                await Context.Channel.SendMessageAsync("Please try again and specify the ingredent(s) you need.");
                return;

            }

            await Data.Data.addIngredients(Context.User.Id, ingredients, "n");
            await Context.Channel.SendMessageAsync("Ingredients added");
            await ingredientsList();

        }

        [Command("-need"), Alias("-Need"), Summary("Makes a list of ingredients user needs.")]
        public async Task removeNeed(string ingredients = "")
        {
            
            if (Data.Data.ingredients2(Context.User.Id).ToString() == "")
            {
                await Context.Channel.SendMessageAsync("You haven't make a list yet.");
                Console.WriteLine("Empty");
                return;
            }
            else if (ingredients == "")
            {
                await Context.Channel.SendMessageAsync("Please try again and specify the ingredent you wish to remove.");
                return;

            }

            await Data.Data.remove(Context.User.Id, ingredients, "n");
            await Context.Channel.SendMessageAsync("Ingredients removed");
            await ingredientsList();

        }

        [Command("-have"), Alias("-Have"), Summary("Makes a list of ingredients user needs.")]
        public async Task removeHave(string ingredients = "")
        {

            if (Data.Data.ingredients2(Context.User.Id).ToString() == "")
            {
                await Context.Channel.SendMessageAsync("You haven't make a list yet.");
                Console.WriteLine("Empty");
                return;
            }
            else if (ingredients == "")
            {
                await Context.Channel.SendMessageAsync("Please try again and specify the ingredent you wish to remove.");
                return;

            }

            await Data.Data.remove(Context.User.Id, ingredients, "h");
            await Context.Channel.SendMessageAsync("Ingredients removed.");
            await ingredientsList();

        }

        [Command("+have"), Alias("+Have"), Summary("Makes a list of ingredients user needs.")]
        public async Task addIngredients(string ingredients = "", string link = "")
        {
            Console.WriteLine("addNeededIngredients fired");
            if (Data.Data.ingredients2(Context.User.Id).ToString() == "")
            {
                await Context.Channel.SendMessageAsync("You haven't make a list yet.");
                Console.WriteLine("Empty");
                return;
            }
            else if (ingredients == "")
            {
                await Context.Channel.SendMessageAsync("Please try again and specify the ingredent(s) you need.");
                return;

            }

            await Data.Data.addIngredients(Context.User.Id, ingredients, "h");
            await Context.Channel.SendMessageAsync("Ingredients added");
            await ingredientsList();

        }


        [Command("update"), Alias("Update","Create","create"), Summary("Makes a list of ingredients user needs.")]
        public async Task updateIngredients(string need = "",string have= "",string ign="", string link = "")
        {
            Console.WriteLine("addNeededIngredients fired");
         
            if (need == "" )
            {
                await Context.Channel.SendMessageAsync("Please try again and specify the ingredent(s) you need.");
                return;

            }
            else if(have == "")
            {
                await Context.Channel.SendMessageAsync("Please try again and specify the ingredent(s) you have.");
                return;
            }
           

            await Data.Data.updateIngredients(Context.User.Id, need,have,ign,link);
            await Context.Channel.SendMessageAsync("Ingredients added");
            await ingredientsList();

        }


        [Command("ingredients"), Summary("Ingredients user has")]
        public async Task ingredientsList(IUser user = null)
        {
            if (user == null)
            {
                if (Data.Data.ingredients2(Context.User.Id).ToString() == "")
                {
                    await Context.Channel.SendMessageAsync("You haven't make a list yet.");
                    Console.WriteLine("Empty");
                    return;
                }
                var trade = (Trade)Data.Data.ingredients2(Context.User.Id);
                var needList = (trade.Need.ToString() != "") ? trade.Need.ToString().Replace(",", "\n") : "empty";
                var haveList = (trade.Have.ToString() != "") ? trade.Have.ToString().Replace(",", "\n") : "empty";
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithColor(96, 156, 255);
                embed.WithTitle($"This is your Ingredients List \n \n");
                embed.AddField("In-Game-Name: ",$"{trade.inGameName}");
                embed.AddField("Invite Link: ", $"Click this [Link]({trade.inviteLink})");
                embed.AddInlineField("Looking For:", needList);
                embed.AddInlineField("Has:", haveList);
                

                await Context.Channel.SendMessageAsync("", false, embed.Build());

            }
            else
            {
                if (Data.Data.ingredients2(user.Id).ToString() == "")
                {
                    await Context.Channel.SendMessageAsync("You haven't make a list yet.");
                    Console.WriteLine("Empty");
                    return;
                }
                var trade = (Trade)Data.Data.ingredients2(user.Id);
                var needList = (trade.Need.ToString() != "") ? trade.Need.ToString().Replace(",", "\n") : "empty";
                var haveList = (trade.Have.ToString() != "") ? trade.Have.ToString().Replace(",", "\n") : "empty";
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithColor(96, 156, 255);
                embed.AddField("In-Game-Name: ", $"{trade.inGameName}");
                embed.AddField("Invite Link: ", $"Click this [Link]({trade.inviteLink})");
                embed.AddInlineField("Looking For:", needList);
                embed.AddInlineField("Has:", haveList);
                await Context.Channel.SendMessageAsync("", false, embed.Build());

            }


        }

        [Command("reset"), Summary("Reset ingredients list")]
        public async Task deleteList(IUser user = null)
        {

            if (Data.Data.ingredients2(Context.User.Id).ToString() == "")
            {
                await Context.Channel.SendMessageAsync("You haven't make a list yet.");
                Console.WriteLine("Empty");
                return;
            }
            await Data.Data.deleteIngredients(Context.User.Id);


            await ingredientsList();


        }



    }
}
