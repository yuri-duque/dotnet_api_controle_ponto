namespace Service.Identity
{
    public class TokenConfigurations
    {
        public int Id { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }
}
