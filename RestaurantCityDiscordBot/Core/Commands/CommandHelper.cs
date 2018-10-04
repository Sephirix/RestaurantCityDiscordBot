//using System;
//using System.Collections.Generic;
//using System.Text;
//using Discord;
//using Discord.Commands;
//using System.Threading.Tasks;
//using Discord.WebSocket;
//using RestaurantCityDiscordBot.Core.Data;
//using RestaurantCityDiscordBot.Resources.Database;
//using System.Linq;

//namespace RestaurantCityDiscordBot.Core.Commands
//{
//    class CommandHelper: ModuleBase<SocketCommandContext>
//    {
//[Group("Need"), Summary("Makes a list of ingredients user needs.")]
//class forTrade : ModuleBase<SocketCommandContext>
//{
//[Command(""), Alias("me"), Summary("Makes a list of ingredients user needs.")]
//public async Task addNeededIngredients(string ingredients = "", string link = "")
//{
//    Console.WriteLine("addNeededIngredients fired");
//    if (Data.Data.ingredients2(Context.User.Id).ToString() == "")
//    {
//        await Context.Channel.SendMessageAsync("You haven't make a list yet.");
//        Console.WriteLine("Empty");
//        return;
//    }
//    else if (ingredients == "")
//    {
//        await Context.Channel.SendMessageAsync("Please try again and specify the ingredent(s) you need.");
//        return;

//    }

//    await Data.Data.addIngredients(Context.User.Id, ingredients, "n");
//    await Context.Channel.SendMessageAsync("Ingredients added");

//}



//            [Command("update"), Summary("Makes a list of ingredients user needs.")]
//            public async Task updateIngredients(string ingredients = "", string ign = "", string link = "")
//            {
//                Console.WriteLine("addNeededIngredients fired");
//                if (ingredients == "")
//                {
//                    await Context.Channel.SendMessageAsync("Please try again and specify the ingredent(s) you need.");
//                    return;

//                }
//                else if (ign == "")
//                {
//                    await Context.Channel.SendMessageAsync("Please try again and specify your in game name.");
//                    return;
//                }
//                await Data.Data.updateIngredients(Context.User.Id, ingredients, "n", ign, link);
//                await Context.Channel.SendMessageAsync("Ingredients added");

//            }

//            [Command("ingredients"), Summary("Ingredients user has")]
//            public async Task ingredientsList(IUser user = null)
//            {
//                if (user == null)
//                {
//                    if (Data.Data.ingredients2(Context.User.Id).ToString() == "")
//                    {
//                        await Context.Channel.SendMessageAsync("You haven't make a list yet.");
//                        Console.WriteLine("Empty");
//                        return;
//                    }
//                    var trade = (Trade)Data.Data.ingredients2(Context.User.Id);
//                    var needList = (trade.Need.ToString() != "") ? trade.Need.ToString().Replace(",", "\n") : "empty";
//                    var haveList = (trade.Have.ToString() != "") ? trade.Have.ToString().Replace(",", "\n") : "empty";
//                    EmbedBuilder embed = new EmbedBuilder();
//                    embed.WithColor(96, 156, 255);
//                    embed.WithTitle($"This is your Ingredients List \n \n");
//                    embed.AddInlineField("Looking For:", needList);
//                    embed.AddInlineField("Has:", haveList);
//                    embed.WithDescription((trade.inviteLink == "") ? "You didn't provide any invite link" : $"\n This is your invite [Link]({trade.inviteLink})");

//                    await Context.Channel.SendMessageAsync("", false, embed.Build());

//                }
//                else
//                {
//                    if (Data.Data.ingredients2(user.Id).ToString() == "")
//                    {
//                        await Context.Channel.SendMessageAsync("You haven't make a list yet.");
//                        Console.WriteLine("Empty");
//                        return;
//                    }
//                    var trade = (Trade)Data.Data.ingredients2(user.Id);
//                    var needList = (trade.Need.ToString() != "") ? trade.Need.ToString().Replace(",", "\n") : "empty";
//                    var haveList = (trade.Have.ToString() != "") ? trade.Have.ToString().Replace(",", "\n") : "empty";
//                    EmbedBuilder embed = new EmbedBuilder();
//                    embed.WithColor(96, 156, 255);
//                    embed.WithTitle($"**{user.Username}** Ingredients List \n \n");
//                    embed.AddInlineField("Looking For:", needList);
//                    embed.AddInlineField("Has:", haveList);
//                    embed.WithDescription((trade.inviteLink == "") ? "You didn't provide any invite link" : $"\n This is your invite [Link]({trade.inviteLink})");
//                    await Context.Channel.SendMessageAsync("", false, embed.Build());

//                }


//            }

//        }

//    }
//}
