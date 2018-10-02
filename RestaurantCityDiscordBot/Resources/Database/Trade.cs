using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantCityDiscordBot.Resources.Database
{
    public class Trade
    {
        [Key]
        public int Id { get; set; }
        public ulong UserId { get; set; }
        public string Need { get; set; }
        public string Have { get; set; }
    }
}
