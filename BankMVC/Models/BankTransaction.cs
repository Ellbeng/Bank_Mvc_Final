using BankMVC.Models;

namespace BankMVC.Models
{
    public class BankTransaction
    {
        
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PaymentType { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }
        public virtual UserModel Users { get; set; }
    }
}
