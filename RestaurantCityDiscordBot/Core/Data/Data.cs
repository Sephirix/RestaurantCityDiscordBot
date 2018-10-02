using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RestaurantCityDiscordBot.Resources.Database;
using System.Linq;
using Discord;
using Discord.WebSocket;
namespace RestaurantCityDiscordBot.Core.Data
{
    public class Data
    {
        public static  string ingredients(ulong userId)
        {
            using (var DbContext = new SqliteDbContext())
            {
                
                if (DbContext.Trades.Where(x => x.UserId == userId).Count() < 1)
                {
                    return "empty";
                }
                else
                {
                    var trade = DbContext.Trades.Where(x => x.UserId == userId).FirstOrDefault();
                    return $"**Looking For**: {trade.Need} \n \n **For Trade**: {trade.Have} "; 
                }

                
                
            }
        }

        public static async Task deleteIngredients(ulong userId)
        {
            using (var DbContext = new SqliteDbContext())
            {
                try
                {
                    if (DbContext.Trades.Where(x => x.UserId == userId).Count() < 1)
                    {
                        return;
                    }
                    Trade trade = DbContext.Trades.Where(x => x.UserId == userId).FirstOrDefault();
                    DbContext.Trades.Remove(trade);
                    await DbContext.SaveChangesAsync();
                }
                catch(Exception ex) { Console.WriteLine(ex.ToString()); }
              
            }
        }

        public static async Task addIngredients(ulong userId, string ingredients,string type)
        {
            try {
                using (var DbContext = new SqliteDbContext())
                {
                    if (DbContext.Trades.Where(x => x.UserId == userId).Count() < 1)
                    {
                        DbContext.Trades.Add(new Trade
                        {
                            UserId = userId,
                            Have = (type == "h") ? ingredients : "",
                            Need = (type == "n") ? ingredients : "",
                        });
                    }
                    else
                    {
                        Trade trade = DbContext.Trades.Where(x => x.UserId == userId).FirstOrDefault();
                        if (type == "h")
                        {
                            trade.Have = ingredients;
                        }
                        else if (type == "n")
                        {
                            trade.Need = ingredients;
                        }
                        DbContext.Trades.Update(trade);
                    }
                    await DbContext.SaveChangesAsync();
                }
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
            }
            
        }

       
    }
}
