using BankMVC.DTO;
using BankMVC.Models;
using Dapper;
using MVCWithDapper.DBContext;
using System.Data;

namespace BankMVC.Services
{
    public interface ICardService
    {
        public UserModel CheckCardUser(CardDto userModel);
        public string GenerateBankFormFillLink(TransactionDto transaction);
    }
    public class CardService : ICardService
    {
        public readonly DapperContext? _context;
        public CardService(DapperContext context)
        {
            _context = context;

        }

        public UserModel CheckCardUser(CardDto userModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CardNumber", userModel.CardNumber);
            parameters.Add("@ExpirationMonth", userModel.ExpirationMonth);
            parameters.Add("@ExpirationYear", userModel.ExpirationYear);
            parameters.Add("@CVV", userModel.CVV);


            using (IDbConnection dbConnection = _context.Connection)
            {
                dbConnection.Open();
                var result = dbConnection.Query<UserModel>("CheckCard", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

                dbConnection.Close();
                return result;

            }
        }

        public string GenerateBankFormFillLink(TransactionDto transaction)
        {
            // Assuming you have the necessary logic to generate the bank form fill link
            // based on the transaction data, such as constructing the URL with query parameters

            // Example:
            string bankFormFillLink = "https://localhost:44365/Card/CardInformation?amount=" + transaction.Amount.ToString() + "&currency=" + transaction.Currency.ToString()+
                "&id="+transaction.Id.ToString()+ "&paymentType="+transaction.PaymentType.ToString()+ "&createDAte="+transaction.CreateDate.ToString()+
                "&status="+transaction.Status.ToString()+ "&callbackUrl="+transaction.CallbackUrl;
           // string bankFormFillLink = "https://localhost:44365/Card/CardInformation";
            return bankFormFillLink;
        }
    }
}
