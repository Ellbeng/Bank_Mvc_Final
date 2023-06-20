using Dapper;
using BankMVC.Models;
using MVCWithDapper.DBContext;
using System.Data;
using BankMVC.DTO;

namespace BankMVC.Services
{
    public interface ITransactionService
    {
        public void InsertTransaction(BankTransaction transaction);
    }
    public class TransactionService:ITransactionService
    {
        public readonly DapperContext? _context;
        public TransactionService(DapperContext context)
        {
            _context = context;


        }


        public void InsertTransaction(BankTransaction transaction)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", transaction.UserId);
            parameters.Add("PaymentType", transaction.PaymentType);
            parameters.Add("Amount", transaction.Amount);
            parameters.Add("Currency", transaction.Currency);
            parameters.Add("CreateDate", transaction.CreateDate);
            parameters.Add("Status", transaction.Status);

            using (IDbConnection dbConnection = _context.Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("InsertBankTransactions", parameters, commandType: CommandType.StoredProcedure);


                dbConnection.Close();

            }
        }





    }
}
