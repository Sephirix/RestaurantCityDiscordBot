using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using Discord.WebSocket;
using RestaurantCityDiscordBot.Core.Data;
using RestaurantCityDiscordBot.Resources.Database;

namespace RestaurantCityDiscordBot.Core.Commands
{
    public class TradeCommands: ModuleBase<SocketCommandContext>
    {
       
        [Command("Need"), Alias("need","LF>"), Summary("Makes a list of ingredients user needs.")]
        public async Task addNeededIngredients( string ingredients ="")
        {
            if (ingredients == "") { await Context.Channel.SendMessageAsync("Please try again and specify the ingredent(s) you need. "); return; }
            await Data.Data.addIngredients(Context.User.Id, ingredients, "n");
            await Context.Channel.SendMessageAsync("Ingredients added");

        }
       

    

        [Command("Have"), Alias("have","H>"), Summary("Makes a list of ingredients user has.")]
        public async Task addIngredients( string ingredients = "")
        {
            if (ingredients == "") { await Context.Channel.SendMessageAsync("Please try again and specify the ingredent(s) you have."); return; }
            await Data.Data.addIngredients(Context.User.Id, ingredients, "h");
            await Context.Channel.SendMessageAsync("Ingredients added");
        }



        [Command("ingredients"), Summary("Ingredients user has")]
        public async Task ingredientsList(IUser user = null)
        {
            if (user == null) { await Context.Channel.SendMessageAsync(Data.Data.ingredients(Context.User.Id)); }
            else
            {
                await Context.Channel.SendMessageAsync(Data.Data.ingredients(user.Id));
            }
            

        }

        [Command("reset"), Summary("Reset ingredients list")]
        public async Task deleteList(IUser user = null)
        {
            
            
                await Data.Data.deleteIngredients(Context.User.Id);
             
            
            await Context.Channel.SendMessageAsync(Data.Data.ingredients(Context.User.Id));


        }



    }
}
