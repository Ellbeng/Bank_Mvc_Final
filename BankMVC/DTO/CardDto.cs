namespace BankMVC.DTO
{
    public class CardDto
    {
        public string CardNumber { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public string CVV { get; set; }
    }
}
