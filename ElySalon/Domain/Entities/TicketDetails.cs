using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElySalon.Domain.Entities
{
    internal class TicketDetails
    {
        private int ticketDetailsId {  get; set; }
        private string ticketId { get; set; }
        private int articleId { get; set; }
        private int quantity { get; set; }
        private decimal amount {  get; set; }
    }
}
