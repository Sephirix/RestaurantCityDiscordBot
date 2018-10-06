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
        //public static  string ingredients(ulong userId)
        //{
        //    using (var DbContext = new SqliteDbContext())
        //    {
                
        //        if (DbContext.Trades.Where(x => x.UserId == userId).Count() < 1)
        //        {
        //            return "empty";
        //        }
        //        else
        //        {
        //            var trade = DbContext.Trades.Where(x => x.UserId == userId).FirstOrDefault();
        //            return"";
        //        }

                
                
        //    }
        //}

        public static object ingredients2(ulong userId)
        {
            using (var DbContext = new SqliteDbContext())
            {
                try {
                    if (DbContext.Trades.Where(x => x.UserId == userId).Count() < 1)
                    {
                        
                        return "";
                    }
                    else
                    {
                        var trade = DbContext.Trades.Where(x => x.UserId == userId).FirstOrDefault();
                        return trade;
                    }
                } catch(Exception ex) {
                    Console.WriteLine(ex.ToString());
                    return "";

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

                    trade.Have = "";
                    trade.Need = "";
                    DbContext.Trades.Update(trade);
                    await DbContext.SaveChangesAsync();
                }
                catch(Exception ex) { Console.WriteLine(ex.ToString()); }
              
            }
        }

        public static async Task remove(ulong userId,string ingredient,string type)
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
                    if (type == "h")
                    {
                        trade.Have = trade.Have.ToString().Replace(ingredient, $"~~{ingredient}~~");
                        DbContext.Trades.Update(trade);
                    }
                    else if(type =="n")
                    { 
                    trade.Need = trade.Need.ToString().Replace(ingredient, $"~~{ingredient}~~");
                        DbContext.Trades.Update(trade);
                    }
                    
                    await DbContext.SaveChangesAsync();
                }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }

            }
        }

        public static async Task addIngredients(ulong userId, string ingredients,string type)
        {
            try {
                using (var DbContext = new SqliteDbContext())
                {
                    if (DbContext.Trades.Where(x => x.UserId == userId).Count() < 1)
                    {
                        
                        return  ;
                    }
                    else
                    {
                        Trade trade = DbContext.Trades.Where(x => x.UserId == userId).FirstOrDefault();
                        if (type == "h")
                        {
                            trade.Have = trade.Have + "," + ingredients ;
                        }
                        else if (type == "n")
                        {
                            trade.Need = trade.Need +","+ingredients ;
                        }
                        DbContext.Trades.Update(trade);
                    }
                    await DbContext.SaveChangesAsync();
                }
            } catch(Exception ex) {
                Console.WriteLine(ex.ToString());
            }
            
        }
        public static async Task updateInviteLink(ulong userId,string ign,string link)
        {
            try
            {
                using (var DbContext = new SqliteDbContext())
                {
                    if (DbContext.Trades.Where(x => x.UserId == userId).Count() < 1)
                    {
                        DbContext.Trades.Add(new Trade
                        {
                            UserId = userId,
                            Have = "",
                            Need = "",
                            inGameName = ign,
                            inviteLink = link
                        });
                    }
                    else
                    {
                        Trade trade = DbContext.Trades.Where(x => x.UserId == userId).FirstOrDefault();

                        trade.inviteLink =  link;



                        DbContext.Trades.Update(trade);
                    }
                    await DbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        public static async Task updateIGN(ulong userId, string ign,string link)
        {
            try
            {
                using (var DbContext = new SqliteDbContext())
                {
                    if (DbContext.Trades.Where(x => x.UserId == userId).Count() < 1)
                    {
                        DbContext.Trades.Add(new Trade
                        {
                            UserId = userId,
                            Have = "",
                            Need = "",
                            inGameName = ign,
                            inviteLink = link
                        });
                    }
                    else
                    {
                        Trade trade = DbContext.Trades.Where(x => x.UserId == userId).FirstOrDefault();

                        trade.inGameName  = ign;



                        DbContext.Trades.Update(trade);
                    }
                    await DbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public static async Task updateIngredients(ulong userId, string need,string have, string ign, string link)
        {
            try
            {
                using (var DbContext = new SqliteDbContext())
                {
                    if (DbContext.Trades.Where(x => x.UserId == userId).Count() < 1)
                    {
                        DbContext.Trades.Add(new Trade
                        {
                            UserId = userId,
                            Have = (have == "") ? "" :have,
                            Need = (need == "") ? "" :need,
                            inGameName = ign,
                            inviteLink = link
                        });
                    }
                    else if (need == "")
                    {
                        Trade trade = DbContext.Trades.Where(x => x.UserId == userId).FirstOrDefault();
                        trade.Have = have;
                        DbContext.Trades.Update(trade);
                    }
                    else if (have == "")
                    {
                        Trade trade = DbContext.Trades.Where(x => x.UserId == userId).FirstOrDefault();
                        trade.Need = need;
                        DbContext.Trades.Update(trade);
                    }
                    else
                    {
                        Trade trade = DbContext.Trades.Where(x => x.UserId == userId).FirstOrDefault();
                       
                            trade.Have = have;
                       
                      
                            trade.Need = need;
                      
                        DbContext.Trades.Update(trade);
                    }
                    await DbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }


    }
}
