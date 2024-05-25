namespace tokesServiceWebApi.DTOs
{
    public class ClientEditDto
    {

        public int IdClient { get; set; }
        public string? NameClient { get; set; }

        public string? EmailClient { get; set; }

        public string? PhoneClient { get; set; }

        public string? AddressClient { get; set; }

        public string? TechnicalComments { get; set; }
        public string? LastModification { get; set; }

    }
}
