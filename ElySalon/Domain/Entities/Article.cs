using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElySalon.Domain.Entities
{
    internal class Article
    {
        private int articleId { get; set; }
        private string articleName { get; set; }
        private int articleType { get; set; }
        private decimal priceCost { get; set; }
        private decimal priceBuy {  get; set; }
        private int stock {  get; set; }
        private string description { get; set; }

        
    }
}
