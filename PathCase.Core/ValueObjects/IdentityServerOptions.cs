namespace PathCase.Core.ValueObjects
{
    public class IdentityServerOptions
    {
        public string Address { get; set; } 
        public string ClientId { get; set; } 
        public string Scope { get; set; } 
        public string Secret { get; set; } 
    }
}