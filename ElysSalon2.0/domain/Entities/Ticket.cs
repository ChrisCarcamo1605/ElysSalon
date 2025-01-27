namespace ElysSalon2._0.domain.Entities
{
    internal class Ticket
    {
        private string ticketId { get; set; }
        public string ticketDetails { get; set; }
        private DateTime emissionDateTime { get; set; }
        private string issuer { get; set; }
        private decimal totalOutTaxes { get; set; }
        private decimal totalWithTaxes { get; set; }
        private decimal totalAmount { get; set; }

    }



}

