namespace BankMVC.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public decimal Balance { get; set; }
        public string CVV { get; set; }
    }
}
