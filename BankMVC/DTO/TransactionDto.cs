using BankMVC.Models;

namespace BankMVC.DTO
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string PaymentType { get; set; }
        public string Currency { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }
        public string CallbackUrl { get; set; }


    }
}
